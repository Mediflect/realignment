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

    private List<DialogSequence> currentSequences;

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

        StartCoroutine(RunExposition());
    }

    private void Awake()
    {
        fadeOut.FadeFinished += OnFadeFinished;
    }

    private void OnDestroy()
    {
        fadeOut.FadeFinished -= OnFadeFinished;
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
            fadeOut.gameObject.SetActive(true);
        }
    }

    private void OnFadeFinished()
    {
        if (App.CurrentExposition == ExpositionType.Intro)
        {
            SceneManager.LoadScene(mainSceneName);
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
}

public enum ExpositionType
{
    Intro,
    Reboot,
    Download
}
