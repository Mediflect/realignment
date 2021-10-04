using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFader : MonoBehaviour
{
    public AudioSource source;
    public bool fadeIn = true;
    public float fadeTime = 1f;
    public float fadeInTarget = 0.2f;
    public bool doOnEnable = false;

    private Coroutine fadeCoroutine = null;

    public void DoFade()
    {
        if (fadeCoroutine != null)
        {
            Debug.LogError("fade was in progress, overwriting");
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null;
        }
        fadeCoroutine = StartCoroutine(RunFade());
    }

    private void OnEnable()
    {
        if (doOnEnable)
        {
            DoFade();
        }
    }

    private IEnumerator RunFade()
    {
        float playingVolume = fadeIn ? fadeInTarget : source.volume;
        float fadeTimer = fadeTime;
        while (fadeTimer > 0f)
        {
            float progress = Mathf.InverseLerp(fadeTime, 0, fadeTimer);
            float volume;
            if (fadeIn)
            {
                volume = Mathf.Lerp(0, playingVolume, progress);
            }
            else
            {
                volume = Mathf.Lerp(playingVolume, 0, progress);
            }

            source.volume = volume;
            fadeTimer -= Time.deltaTime;
            yield return null;
        }

        fadeCoroutine = null;
    }
}
