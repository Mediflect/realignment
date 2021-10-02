using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!App.IsInitialized)
        {
            return;
        }
        if (App.ProgressTracker.AllStationsComplete)
        {
            App.ProgressTracker.OnTriggerVictory();
        }
    }
}
