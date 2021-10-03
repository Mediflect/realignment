using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System.Collections.Generic;

public class MinigameStation : MonoBehaviour
{
    public MinigameType minigameType;
    public DualLoopFader hum;
    public SpriteRenderer monitorRenderer;
    public Sprite goodSprite;
    public Sprite badSprite;
    public Color goodColor = Color.green;
    public Color badColor = Color.red;
    public List<Light2D> monitorLights;

    public bool IsCompleted => isCompleted;
    
    private bool isCompleted;

    public void OnGameWon()
    {
        monitorRenderer.sprite = goodSprite;
        SetLightColors(goodColor);
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
        monitorRenderer.sprite = badSprite;
        SetLightColors(badColor);
        App.OnInitialized -= Initialize;
    }

    private void SetLightColors(Color color)
    {
        for (int i = 0; i < monitorLights.Count; ++i)
        {
            monitorLights[i].color = color;
        }
    }
}
