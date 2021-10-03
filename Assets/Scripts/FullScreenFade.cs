using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenFade : MonoBehaviour
{
    public event System.Action FadeFinished;

    public bool fadeOut = true;
    public bool onlyFromTitle = false;
    public float fadeTime = 3f;
    public Image fadeImage;

    private float progress;

    private void OnEnable()
    {
        if (onlyFromTitle && !App.CameFromTitle)
        {
            gameObject.SetActive(false);
            return;
        }
        progress = 0;
        UpdateFade();
        StartCoroutine(RunFade());
    }

    private IEnumerator RunFade()
    {
        float timer = fadeTime;
        while (timer > 0f)
        {
            progress = Mathf.InverseLerp(fadeTime, 0, timer);
            if (!fadeOut)
            {
                progress = 1f - progress;
            }
            UpdateFade();
            timer -= Time.deltaTime;
            yield return null;
        }
        FadeFinished?.Invoke();
        if (!fadeOut)
        {
            gameObject.SetActive(false);
        }
    }

    private void UpdateFade()
    {
        Helpers.SetImageAlpha(fadeImage, progress);
    }
}
