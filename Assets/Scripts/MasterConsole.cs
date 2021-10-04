using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MasterConsole : MonoBehaviour
{
    public static bool FinaleStarted = false;

    public NarrativeDriver narrativeDriver;
    public FullScreenFade fadeOut;
    public string expositionSceneName = "Exposition";

    [Header("Station Stuff")]
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
        Debug.Log("reboot ending");
        App.CurrentExposition = ExpositionType.Reboot;
        StartFadeOut();
    }

    public void ChooseDownload()
    {
        Debug.Log("download ending");
        App.CurrentExposition = ExpositionType.Download;
        StartFadeOut();
    }

    private void StartFadeOut()
    {
        fadeOut.gameObject.SetActive(true);
        hum.FadeOut();
    }

    private void Awake()
    {
        ProgressTracker.OnAllStationsComplete += OnAllStationsComplete;
        MinigameStation.AnyStationCompleted += OnAnyStationComplete;
        fadeOut.FadeFinished += OnFadeOutFinished;
    }

    private void OnDestroy()
    {
        ProgressTracker.OnAllStationsComplete -= OnAllStationsComplete;
        MinigameStation.AnyStationCompleted -= OnAnyStationComplete;
        fadeOut.FadeFinished -= OnFadeOutFinished;
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
        // App.Hums.SetUpRandom(hum);   // don't randomize the master console
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
        FinaleStarted = true;
        container.SetActive(true);
        hum.Play();
        SetLightColors(goodColor);
        monitorRenderer.sprite = goodSprite;
        narrativeDriver.StartFinale();
    }

    private void OnAnyStationComplete()
    {
        Debug.Log("station complete: " + App.ProgressTracker.PercentStationsComplete);
        hum.SetFader(App.ProgressTracker.PercentStationsComplete);
        SetLightColors(Color.Lerp(badColor, goodColor, App.ProgressTracker.PercentStationsComplete));
    }

    private void OnFadeOutFinished()
    {
        SceneManager.LoadScene(expositionSceneName);
    }

    [ContextMenu("Skip To Finale")]
    private void DebugAllComplete()
    {
        foreach (MinigameStation station in MinigameStation.AllStations)
        {
            station.OnGameWon();
        }
    }
}
