using UnityEngine;
using System.Collections.Generic;

public class ProgressTracker : MonoBehaviour
{
    public List<MinigameStation> stations;

    public bool AllStationsComplete => allStationsComplete;
    private bool allStationsComplete = false;

    public void OnTriggerVictory()
    {
        Debug.Log("You win!");
    }

    private void Update()
    {
        allStationsComplete = true;
        for (int i = 0; i < stations.Count; ++i)
        {
            if (!stations[i].IsCompleted)
            {
                allStationsComplete = false;
            }
        }
    }
}
