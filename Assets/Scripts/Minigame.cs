using UnityEngine;
using System.Collections;

public abstract class Minigame : MonoBehaviour
{
    public MinigameStation station;
    public float finishWaitTime = 2f;
    public float progress = 0f;
    public MinigameBackground minigameBackground;

    private bool isWinning = false;
    private Coroutine winCoroutine = null;

    protected virtual void Update()
    {
        station.hum.SetFader(progress);
        minigameBackground.SetProgress(progress);

        isWinning = progress >= 1f;
        if (isWinning && winCoroutine == null)
        {
            winCoroutine = StartCoroutine(WinCountdown());
        }
        else if (!isWinning && winCoroutine != null)
        {
            StopCoroutine(winCoroutine);
            winCoroutine = null;
        }
    }

    private IEnumerator WinCountdown()
    {
        yield return YieldInstructionCache.WaitForSeconds(finishWaitTime);
        station.OnGameWon();
        gameObject.SetActive(false);
        winCoroutine = null;
    }
}

public enum MinigameType
{
    Slider,
    Knob, 
    Pin
}