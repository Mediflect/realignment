using UnityEngine;

public class MinigameStation : MonoBehaviour
{
    public MinigameType minigameType;
    public DualLoopFader hum;
    public SpriteRenderer testRenderer;
    public bool IsCompleted => isCompleted;
    
    private bool isCompleted;

    public void OnGameWon()
    {
        testRenderer.color = Color.cyan;
        isCompleted = true;
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
        if (isCompleted)
        {
            return;
        }

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
