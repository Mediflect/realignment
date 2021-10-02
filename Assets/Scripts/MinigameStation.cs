using UnityEngine;

public class MinigameStation : MonoBehaviour
{
    public Minigame minigame;
    public DualLoopFader hum;
    public SpriteRenderer testRenderer;

    public void OnGameWon()
    {
        testRenderer.color = Color.cyan;
    }

    private void OnEnable()
    {
        App.Hums.SetUpRandom(hum);
        hum.SetFader(0);
        hum.Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        minigame.station = this;
        minigame.gameObject.SetActive(true);
    }
}
