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
    [TextArea]
    public string Line;
}

public enum SpeakerId
{
    AI,
    Curator,
    Sen,
    Curie,
}