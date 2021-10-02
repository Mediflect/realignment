using UnityEngine;
using TMPro;
using System.Collections;

public class DialogPlayer : MonoBehaviour
{
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI lineText;
    public float characterDelay = 0.1f;
    public float shortPauseLength = 0.5f;
    public string shortPauseCharacters = ",";
    public float longPauseLength = 1f;
    public string longPauseCharacters = "!?.";
    public float endLinePause = 4f;
    public float endFadeTime = 3f;

    public DialogSequence sequence;
    private Coroutine playCoroutine = null;

    public void PlaySequence(DialogSequence sequence)
    {
        this.sequence = sequence;
        if (playCoroutine != null)
        {
            Debug.LogError("sequence was already playing, stopping to play new one");
            StopCoroutine(playCoroutine);
        }
        playCoroutine = StartCoroutine(RunDialogSequence());
    }

    private IEnumerator RunDialogSequence()
    {
        TMPHelpers.SetTextAlpha(speakerText, 1);
        TMPHelpers.SetTextAlpha(lineText, 1);
        for (int i = 0; i < sequence.lines.Count; ++i)
        {
            DialogLine line = sequence.lines[i];
            speakerText.SetText($"{line.Speaker}:");
            lineText.SetText("");
            for (int j = 0; j < line.Line.Length; ++j)
            {
                string character = line.Line.Substring(j, 1);
                lineText.SetText($"{lineText.text}{character}");
                if (j == line.Line.Length - 1)
                {
                    yield return YieldInstructionCache.WaitForSeconds(endLinePause);
                }
                else if (shortPauseCharacters.Contains(character))
                {
                    yield return YieldInstructionCache.WaitForSeconds(shortPauseLength);
                }
                else if (longPauseCharacters.Contains(character))
                {
                    yield return YieldInstructionCache.WaitForSeconds(longPauseLength);
                }
                else
                {
                    yield return YieldInstructionCache.WaitForSeconds(characterDelay);
                }
            }
        }
        float fadeTimer = endFadeTime;
        while (fadeTimer > 0f)
        {
            float alpha = Mathf.InverseLerp(0, endFadeTime, fadeTimer);
            TMPHelpers.SetTextAlpha(speakerText, alpha);
            TMPHelpers.SetTextAlpha(lineText, alpha);
            fadeTimer -= Time.deltaTime;
            yield return null;
        }
        playCoroutine = null;
    }

    [ContextMenu("Play")]
    private void DebugPlay()
    {
        if (playCoroutine != null)
        {
            StopCoroutine(playCoroutine);
        }
        playCoroutine = StartCoroutine(RunDialogSequence());
    }
}
