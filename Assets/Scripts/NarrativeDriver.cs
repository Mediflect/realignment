using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeDriver : MonoBehaviour
{
    public DialogPlayer dialogPlayer;
    public List<DialogSequence> sequences;
    public FullScreenFade fadeIn;

    private int currentSequenceIndex = 0;
    private Queue<DialogSequence> sequenceQueue = new Queue<DialogSequence>();

    private void Awake()
    {
        MinigameStation.AnyStationCompleted += OnStationWon;
        fadeIn.FadeFinished += OnFadeInFinished;
        if (fadeIn.onlyFromTitle && !App.CameFromTitle && !fadeIn.gameObject.activeSelf)
        {
            // fixes initialization order bug
            OnFadeInFinished();
        }
    }

    private void OnDestroy()
    {
        MinigameStation.AnyStationCompleted -= OnStationWon;
        fadeIn.FadeFinished -= OnFadeInFinished;
    }

    private void Update()
    {
        if (!dialogPlayer.IsPlayingSequence && sequenceQueue.Count > 0)
        {
            dialogPlayer.PlaySequence(sequenceQueue.Dequeue());
        }
    }

    private void OnFadeInFinished()
    {
        sequenceQueue.Enqueue(sequences[currentSequenceIndex++]);
    }

    private void OnStationWon()
    {
        if (currentSequenceIndex < sequences.Count)
        {
            sequenceQueue.Enqueue(sequences[currentSequenceIndex++]);
        }
    }
}
