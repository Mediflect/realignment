using UnityEngine;

public class App : MonoBehaviour
{
    private static App Instance;

    public static HumManifest Hums => Instance.hums;
    public HumManifest hums;

    private void Awake()
    {
        Instance = this;
    }
}
