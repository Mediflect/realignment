using UnityEngine;

public class DualLoopFader : MonoBehaviour
{
    public AudioSource tension;
    public AudioSource resolution;

    public float fader = 0f;

    public bool chaseFader = false;
    public float chaseSpeed = 1f;

    private float lerpValue = 0f;


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
}
