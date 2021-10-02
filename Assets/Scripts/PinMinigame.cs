using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PinMinigame : Minigame
{
    private static WaitForSeconds finishWait = new WaitForSeconds(2f);

    public List<PinInputNode> inputNodes;
    public int charsPerNode = 5;

    private int nodesCorrect = 0;
    private bool isWinning = false;
    private Coroutine winCoroutine = null;

    private void OnEnable()
    {
        for (int i = 0; i < inputNodes.Count; ++i)
        {
            inputNodes[i].Initialize(5);
        }
    }

    private void Update()
    {
        nodesCorrect = 0;
        for (int i = 0; i < inputNodes.Count; ++i)
        {
            if (inputNodes[i].AtTarget)
            {
                ++nodesCorrect;
            }
        }

        station.hum.SetFader(Mathf.InverseLerp(0, inputNodes.Count, nodesCorrect));

        // I'm doing it with coroutines now because fuck yeah coroutines
        isWinning = nodesCorrect == inputNodes.Count;
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
        yield return finishWait;
        station.OnGameWon();
        gameObject.SetActive(false);
        winCoroutine = null;
    }
}
