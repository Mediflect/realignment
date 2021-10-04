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

    private void Awake()
    {
        speakerText.SetText("");
        lineText.SetText("");
    }

    private IEnumerator RunDialogSequence()
    {
        Helpers.SetTextAlpha(speakerText, 1);
        Helpers.SetTextAlpha(lineText, 1);
        for (int i = 0; i < sequence.lines.Count; ++i)
        {
            DialogLine line = sequence.lines[i];
            speakerText.SetText("");
            lineText.SetText("");

            string speakerName = GetSpeakerName(line.Speaker);
            bool typeInSpeakerName = false;
            if (typeInSpeakerName)
            {
                for (int j = 0; j < speakerName.Length; ++j)
                {
                    string character = speakerName.Substring(j, 1);
                    speakerText.SetText($"{speakerText.text}{character}");
                    yield return YieldInstructionCache.WaitForSeconds(characterDelay);
                }
                speakerText.SetText($"{speakerText.text}:");
                yield return YieldInstructionCache.WaitForSeconds(shortPauseLength);
            }
            else
            {
                speakerText.SetText($"{speakerName}:");
            }

            for (int j = 0; j < line.Line.Length; ++j)
            {
                string character = line.Line.Substring(j, 1);
                string nextCharacter = j == line.Line.Length - 1 ? null : line.Line.Substring(j+1, 1);
                lineText.SetText($"{lineText.text}{character}");
                if (j == line.Line.Length - 1)
                {
                    yield return YieldInstructionCache.WaitForSeconds(endLinePause);
                }
                else if (shortPauseCharacters.Contains(character))
                {
                    yield return YieldInstructionCache.WaitForSeconds(shortPauseLength);
                }
                else if (longPauseCharacters.Contains(character) && character != nextCharacter)
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
            Helpers.SetTextAlpha(speakerText, alpha);
            Helpers.SetTextAlpha(lineText, alpha);
            fadeTimer -= Time.deltaTime;
            yield return null;
        }
        playCoroutine = null;
    }

    private string GetSpeakerName(SpeakerId speaker)
    {
        switch (speaker)
        {
            case SpeakerId.AI:
                return "AI Core #1233";
            case SpeakerId.Curator:
                return "Curator";
            case SpeakerId.Sen:
                return "Sen";
            case SpeakerId.Curie:
                return "Curie";
            default:
                return "";
        }
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
