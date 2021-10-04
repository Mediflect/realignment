using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExpositionDriver : MonoBehaviour
{
    public string mainSceneName = "Main";
    public float beginDelay = 1f;
    public ExpositionDialogPlayer dialogPlayer;
    public List<DialogSequence> introSequences;
    public List<DialogSequence> rebootSequences;
    public List<DialogSequence> downloadSequences;
    public FullScreenFade fadeOut;
    public FullScreenFade fadeIn;
    public GameObject expositionContainer;
    public GameObject creditsContainer;

    private List<DialogSequence> currentSequences;
    private Coroutine expositionCoroutine;

    private void OnEnable()
    {
        switch (App.CurrentExposition)
        {
            case ExpositionType.Intro:
                currentSequences = introSequences;
                break;
            case ExpositionType.Reboot:
                currentSequences = rebootSequences;
                break;
            case ExpositionType.Download:
                currentSequences = downloadSequences;
                break;
            default:
                break;
        }

        expositionCoroutine = StartCoroutine(RunExposition());
    }

    private void Awake()
    {
        fadeOut.FadeFinished += OnFadeOutFinished;
    }

    private void OnDestroy()
    {
        fadeOut.FadeFinished -= OnFadeOutFinished;
    }

    private IEnumerator RunExposition()
    {
        yield return YieldInstructionCache.WaitForSeconds(beginDelay);
        for (int i = 0; i < currentSequences.Count; ++i)
        {
            dialogPlayer.PlaySequence(currentSequences[i]);
            while (dialogPlayer.IsPlayingSequence)
            {
                yield return null;
            }
        }

        if (App.CurrentExposition == ExpositionType.Intro)
        {
            App.CameFromTitle = true;
        }
        else
        {
            fadeOut.fadeTime *= 2f; // longer more dramatic fade
        }
        fadeOut.gameObject.SetActive(true);
        expositionCoroutine = null;
    }

    private void OnFadeOutFinished()
    {
        if (App.CurrentExposition == ExpositionType.Intro)
        {
            SceneManager.LoadScene(mainSceneName);
        }
        else
        {
            expositionContainer.SetActive(false);
            fadeOut.gameObject.SetActive(false);
            fadeIn.gameObject.SetActive(true);
            creditsContainer.SetActive(true);
        }
    }

    [ContextMenu("Debug Intro")]
    private void DebugIntro()
    {
        App.CurrentExposition = ExpositionType.Intro;
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    [ContextMenu("Debug Reboot")]
    private void DebugReboot()
    {
        App.CurrentExposition = ExpositionType.Reboot;
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    [ContextMenu("Debug Download")]
    private void DebugDownload()
    {
        App.CurrentExposition = ExpositionType.Download;
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    [ContextMenu("Skip To Credits")]
    private void DebugCredits()
    {
        StopCoroutine(expositionCoroutine);
        expositionCoroutine = null;
        App.CurrentExposition = ExpositionType.Download;
        fadeOut.fadeTime *= 2f;
        fadeOut.gameObject.SetActive(true);
    }
}

public enum ExpositionType
{
    Intro,
    Reboot,
    Download
}
