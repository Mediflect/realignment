using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    public MinigameStation station;
}

public enum MinigameType
{
    Slider,
    Knob, 
    Pin
}