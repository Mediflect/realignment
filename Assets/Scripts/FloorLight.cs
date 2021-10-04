using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FloorLight : MonoBehaviour
{
    public Light2D floorLight;
    public LightLine lightLine;
    public MinigameStation station;
    public Color goodColor;
    public Color badColor;

    private void Awake()
    {
        lightLine.lightsColor = badColor;
        floorLight.color = badColor;

        station.OnCompleted += OnStationCompleted;
    }

    private void OnDestroy()
    {
        station.OnCompleted -= OnStationCompleted;
    }

    private void OnStationCompleted()
    {
        lightLine.lightsColor = goodColor;
        floorLight.color = goodColor;
        lightLine.SetReverse(true);     // dunno if this is best or not yet
    }
}
