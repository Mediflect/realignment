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
    public string Speaker;
    public string Line;
}