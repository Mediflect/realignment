using UnityEngine;
using System.Collections;

public class DualLoopFader : MonoBehaviour
{
    public AudioSource tension;
    public AudioSource resolution;

    public float fader = 0f;

    public bool chaseFader = false;
    public float chaseSpeed = 1f;

    [Header("Fade Out")]
    public float fadeOutTime = 3f;

    private float lerpValue = 0f;
    private Coroutine fadeOutCoroutine = null;


    [ContextMenu("Play")]
    public void Play()
    {
        tension.Play();
        resolution.Play();
    }

    [ContextMenu("Stop")]
    public void Stop()
    {
        tension.Stop();
        resolution.Stop();
    }

    [ContextMenu("Fade Out")]
    public void FadeOut()
    {
        fadeOutCoroutine = StartCoroutine(RunFadeOut());
    }

    public void SetFader(float fader)
    {
        this.fader = fader;
    }

    private void Awake()
    {
        tension.loop = true;
        resolution.loop = true;
    }

    private void Update()
    {
        if (fadeOutCoroutine != null)
        {
            return;
        }

        fader = Mathf.Clamp01(fader);

        if (chaseFader)
        {
            lerpValue = Mathf.MoveTowards(lerpValue, fader, Time.deltaTime * chaseSpeed);
        }
        else
        {
            lerpValue = fader;
        }

        tension.volume = Mathf.InverseLerp(1f, 0f, lerpValue);
        resolution.volume = Mathf.InverseLerp(0f, 1f, lerpValue);
    }

    private IEnumerator RunFadeOut()
    {
        float timer = fadeOutTime;
        float tensionStart = tension.volume;
        float resolutionStart = resolution.volume;
        while (timer > 0f)
        {
            float progress = Mathf.InverseLerp(fadeOutTime, 0, timer);
            tension.volume = Mathf.Lerp(tensionStart, 0, progress);
            resolution.volume = Mathf.Lerp(resolutionStart, 0, progress);
            timer -= Time.deltaTime;
            yield return null;
        }
        fadeOutCoroutine = null;
        gameObject.SetActive(false);
    }
}
