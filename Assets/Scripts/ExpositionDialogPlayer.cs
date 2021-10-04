using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

// copy pasting code cause I don't give a fuuuuuuuuck
public class ExpositionDialogPlayer : MonoBehaviour
{   
    public bool IsPlayingSequence => playCoroutine != null;

    public DialogSequence sequence;
    public TextMeshProUGUI lineText;

    [Header("Audio")]
    public AudioSource voiceSource;
    public AudioClip aiVoiceClip;
    public AudioClip curatorVoiceClip;
    public float voiceVolume = 0.5f;

    [Header("Timings")]
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
        lineText.SetText("");
    }

    private IEnumerator RunDialogSequence()
    {
        lineText.SetText("");

        for (int i = 0; i < sequence.lines.Count; ++i)
        {
            DialogLine line = sequence.lines[i];
            lineText.SetText($"{lineText.text}> ");

            if (line.Speaker == SpeakerId.AI || line.Speaker == SpeakerId.Sen)
            {
                lineText.color = aiColor;
                voiceSource.clip = aiVoiceClip;
            }
            else if (line.Speaker == SpeakerId.Curator || line.Speaker == SpeakerId.Curie)
            {
                lineText.color = curatorColor;
                voiceSource.clip = curatorVoiceClip;
            }

            voiceSource.Play();
            for (int j = 0; j < line.Line.Length; ++j)
            {
                string character = line.Line.Substring(j, 1);
                string nextCharacter = j == line.Line.Length - 1 ? null : line.Line.Substring(j + 1, 1);
                lineText.SetText($"{lineText.text}{character}");
                if (j == line.Line.Length - 1)
                {
                    voiceSource.volume = 0;
                    yield return YieldInstructionCache.WaitForSeconds(endLinePause);
                }
                else if (shortPauseCharacters.Contains(character))
                {
                    voiceSource.volume = 0;
                    yield return YieldInstructionCache.WaitForSeconds(shortPauseLength);
                }
                else if (character == "\n" || longPauseCharacters.Contains(character) && character != nextCharacter)
                {
                    voiceSource.volume = 0;
                    yield return YieldInstructionCache.WaitForSeconds(longPauseLength);
                }
                else
                {
                    voiceSource.volume = voiceVolume;
                    yield return YieldInstructionCache.WaitForSeconds(characterDelay);
                }
            }
            lineText.SetText($"{lineText.text}\n\n");
        }
        voiceSource.Stop();


        float fadeTimer = endFadeTime;
        while (fadeTimer > 0f)
        {
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
