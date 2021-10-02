using UnityEngine;

public class MinigameStation : MonoBehaviour
{
    public MinigameType minigameType;
    public DualLoopFader hum;
    public SpriteRenderer testRenderer;

    public void OnGameWon()
    {
        testRenderer.color = Color.cyan;
    }

    private void OnEnable()
    {
        if (App.IsInitialized)
        {
            Initialize();
        }
        else
        {
            App.OnInitialized += Initialize;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Minigame minigame = App.MinigameManager.GetMinigame(minigameType);
        minigame.station = this;
        minigame.gameObject.SetActive(true);
    }

    private void Initialize()
    {
        App.Hums.SetUpRandom(hum);
        hum.SetFader(0);
        hum.Play();
        App.OnInitialized -= Initialize;
    }
}
