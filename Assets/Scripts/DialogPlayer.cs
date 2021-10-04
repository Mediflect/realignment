using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class DialogPlayer : MonoBehaviour
{
    public bool IsPlayingSequence => playCoroutine != null;

    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI lineText;
    public Image backingImage;
    public float characterDelay = 0.1f;
    public float shortPauseLength = 0.5f;
    public string shortPauseCharacters = ",";
    public float longPauseLength = 1f;
    public string longPauseCharacters = "!?.";
    public float endLinePause = 4f;
    public float endFadeTime = 3f;

    [Header("Text Settings")]
    public Color aiColor = Color.red;
    public Color curatorColor = Color.cyan;
    public string aiFormalName = "AI Core #1233";
    public string curatorFormalName = "Curator";
    public string aiInformalName = "Sen";
    public string curatorInformalName = "Curie";

    public DialogSequence sequence;
    private Coroutine playCoroutine = null;
    private float backingAlpha;

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
        backingAlpha = backingImage.color.a;
        Helpers.SetImageAlpha(backingImage, 0);
    }

    private IEnumerator RunDialogSequence()
    {
        Helpers.SetTextAlpha(speakerText, 1);
        Helpers.SetTextAlpha(lineText, 1);
        Helpers.SetImageAlpha(backingImage, backingAlpha);
        for (int i = 0; i < sequence.lines.Count; ++i)
        {
            DialogLine line = sequence.lines[i];
            speakerText.SetText("");
            lineText.SetText("");

            if (line.Speaker == SpeakerId.AI || line.Speaker == SpeakerId.Sen)
            {
                speakerText.color = aiColor;
                lineText.color = aiColor;
            }
            else if (line.Speaker == SpeakerId.Curator || line.Speaker == SpeakerId.Curie)
            {
                speakerText.color = curatorColor;
                lineText.color = curatorColor;
            }

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
            Helpers.SetImageAlpha(backingImage, Mathf.Lerp(0, backingAlpha, alpha));
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
                return aiFormalName;
            case SpeakerId.Curator:
                return curatorFormalName;
            case SpeakerId.Sen:
                return aiInformalName;
            case SpeakerId.Curie:
                return curatorInformalName;
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
