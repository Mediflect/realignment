using UnityEngine;

public class App : MonoBehaviour
{
    public static event System.Action OnInitialized;
    public static bool IsInitialized => Instance != null;
    private static App Instance;

    public static HumManifest Hums => Instance.hums;
    public HumManifest hums;

    public static MinigameManager MinigameManager => Instance.minigameManager;
    public MinigameManager minigameManager;

    private void Awake()
    {
        Instance = this;
        OnInitialized?.Invoke();
    }
}
