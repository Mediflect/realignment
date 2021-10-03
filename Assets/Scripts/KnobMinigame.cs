using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KnobMinigame : Minigame
{
    public Transform knobTransform;
    public DragHandler knobDragHandler;
    public float dragSpeed = 0.5f;
    public float resolveStartThreshold = 90f;  // degrees
    public float resolveEndThreshold = 10f;

    private DualLoopFader hum => station.hum;
    private float target;
    private float distToTarget;

    public void OnKnobDragged(PointerEventData eventData)
    {
        knobTransform.Rotate(Vector3.forward, eventData.delta.x * dragSpeed * -1f);
    }

    private void Awake()
    {
        knobDragHandler.OnDragged += OnKnobDragged;
    }

    private void OnDestroy()
    {
        knobDragHandler.OnDragged -= OnKnobDragged;
    }

    private void OnEnable()
    {
        target = Random.value * 360f;
        // Set the knob at the target
        knobTransform.rotation = Quaternion.identity;
        knobTransform.Rotate(Vector3.forward, target);
        // Rotate it some start distance away;
        float knobStartDist = Random.Range(resolveStartThreshold, 180);
        if (Random.value > 0.5f)
        {
            knobStartDist *= -1f;
        }
        knobTransform.Rotate(Vector3.forward, knobStartDist);
    }

    protected override void Update()
    {
        distToTarget = Mathf.Abs(Mathf.DeltaAngle(target, knobTransform.eulerAngles.z));
        progress = Mathf.InverseLerp(resolveStartThreshold, resolveEndThreshold, distToTarget);
        base.Update();
    }
}
