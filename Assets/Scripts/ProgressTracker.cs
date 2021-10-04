using UnityEngine;
using System.Collections.Generic;

public class ProgressTracker : MonoBehaviour
{
    public static event System.Action OnAllStationsComplete;

    public List<MinigameStation> stations;

    public float PercentStationsComplete => ((float)numStationsComplete) / ((float)stations.Count);
    public bool AllStationsComplete => allStationsComplete;
    private bool allStationsComplete = false;

    private int numStationsComplete = 0;

    private void Update()
    {
        CheckStationsComplete();
    }

    public void CheckStationsComplete()
    {
        numStationsComplete = 0;
        for (int i = 0; i < stations.Count; ++i)
        {
            if (stations[i].IsCompleted)
            {
                ++numStationsComplete;
            }
        }

        if (numStationsComplete == stations.Count && !allStationsComplete)
        {
            allStationsComplete = true;
            OnAllStationsComplete?.Invoke();
        }
    }
}
