using UnityEngine;

public class MinigameStation : MonoBehaviour
{
    public Minigame minigame;
    public DualLoopFader hum;

    private void OnEnable()
    {
        hum.SetFader(0);
        hum.Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        minigame.station = this;
        minigame.gameObject.SetActive(true);
    }
}
