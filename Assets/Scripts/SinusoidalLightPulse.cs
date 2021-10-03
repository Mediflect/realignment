using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SinusoidalLightPulse : MonoBehaviour
{
    public Light2D lightSource;
    public float period = 2f;
    public float amplitude = 0.2f;
    public bool randomizePeriod = false;

    private float baseIntensity = 0f;
    private float timer = 0f;

    private void OnEnable()
    {
        baseIntensity = lightSource.intensity;
        if (randomizePeriod)
        {
            timer = period * Random.value;
        }
    }

    private void OnDisable()
    {
        lightSource.intensity = baseIntensity;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > period)
        {
            timer -= period;
        }
        float t = Mathf.InverseLerp(0, period, timer);
        float sinPos = Mathf.Lerp(0, Mathf.PI * 2f, t);
        float intensity = baseIntensity + Mathf.Sin(sinPos) * amplitude;
        lightSource.intensity = intensity;
    }
}
