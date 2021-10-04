using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Dialog Sequence")]
public class DialogSequence : ScriptableObject
{
    public List<DialogLine> lines;
}

[System.Serializable]
public struct DialogLine
{
    public SpeakerId Speaker;
    [TextArea(minLines: 2, maxLines: 10)]
    public string Line;
}

public enum SpeakerId
{
    AI,
    Curator,
    Sen,
    Curie,
}