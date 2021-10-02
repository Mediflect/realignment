using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public Minigame sliderMinigame;
    public Minigame knobMinigame;
    public Minigame pinMinigame;

    public Minigame GetMinigame(MinigameType type)
    {
        switch (type)
        {
            case MinigameType.Slider:
                return sliderMinigame;
            case MinigameType.Knob:
                return knobMinigame;
            case MinigameType.Pin:
                return pinMinigame;
            default:
                Debug.LogError("unknown minigame");
                return null;
        }
    }
}
