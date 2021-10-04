using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System.Collections.Generic;

public class MasterConsole : MonoBehaviour
{
    public static event System.Action RebootChosen;
    public static event System.Action DownloadChosen;

    public GameObject container;
    public GameObject finaleObj;
    public DualLoopFader hum;
    public SpriteRenderer monitorRenderer;
    public Sprite goodSprite;
    public Sprite badSprite;
    public Color goodColor = Color.green;
    public Color badColor = Color.red;
    public List<Light2D> monitorLights;

    public void OnQuit()
    {
        finaleObj.SetActive(false);
        PlayerControls.SetMovementAllowed(true);
    }

    public void ChooseReboot()
    {
        RebootChosen?.Invoke();
        Debug.Log("reboot ending");
    }

    public void ChooseDownload()
    {
        DownloadChosen?.Invoke();
        Debug.Log("download ending");
    }

    private void Awake()
    {
        ProgressTracker.OnAllStationsComplete += OnAllStationsComplete;
        MinigameStation.AnyStationCompleted += OnAnyStationComplete;
    }

    private void OnDestroy()
    {
        ProgressTracker.OnAllStationsComplete -= OnAllStationsComplete;
        MinigameStation.AnyStationCompleted -= OnAnyStationComplete;
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
        if (!App.ProgressTracker.AllStationsComplete)
        {
            return;
        }

        finaleObj.SetActive(true);
        PlayerControls.SetMovementAllowed(false);
    }

    private void Initialize()
    {
        App.Hums.SetUpRandom(hum);
        hum.SetFader(0);
        hum.Play();
        monitorRenderer.sprite = badSprite;
        SetLightColors(badColor);
        container.SetActive(false);
        App.OnInitialized -= Initialize;
    }

    private void SetLightColors(Color color)
    {
        for (int i = 0; i < monitorLights.Count; ++i)
        {
            monitorLights[i].color = color;
        }
    }

    private void OnAllStationsComplete()
    {
        container.SetActive(true);
        hum.Play();
        SetLightColors(goodColor);
        monitorRenderer.sprite = goodSprite;
    }

    private void OnAnyStationComplete()
    {
        Debug.Log("station complete: " + App.ProgressTracker.PercentStationsComplete);
        hum.SetFader(App.ProgressTracker.PercentStationsComplete);
        SetLightColors(Color.Lerp(badColor, goodColor, App.ProgressTracker.PercentStationsComplete));
    }

    [ContextMenu("Skip To Finale")]
    private void DebugAllComplete()
    {
        foreach(MinigameStation station in MinigameStation.AllStations)
        {
            station.OnGameWon();
        }
    }
}
