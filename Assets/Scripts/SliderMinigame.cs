using UnityEngine;
using UnityEngine.UI;

public class SliderMinigame : Minigame
{
    public Slider slider;
    public float resolveStartThreshold = 0.4f;  // needs to be less than 0.5 for the start position math to work
    public float resolveEndThreshold = 0.05f;

    private DualLoopFader hum => station.hum;
    private float target;
    private float distToTarget = 0f;

    private void OnEnable()
    {
        target = Random.value;
        float sliderStartDist = Random.Range(resolveStartThreshold, Mathf.Max(Mathf.Abs(target), Mathf.Abs(1f - target)));
        slider.normalizedValue = target > 0.5f ? target - sliderStartDist : target + sliderStartDist;
    }

    protected override void Update()
    {
        distToTarget = Mathf.Abs(target - slider.normalizedValue);
        progress = Mathf.InverseLerp(resolveStartThreshold, resolveEndThreshold, distToTarget);
        base.Update();
    }
}
