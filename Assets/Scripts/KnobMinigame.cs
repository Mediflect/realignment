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
    public float victoryHoldTime = 2f;

    private DualLoopFader hum => station.hum;
    private float target;
    private float distToTarget;
    private bool isWinning;
    private float victoryTimer;

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

    private void Update()
    {
        distToTarget = Mathf.Abs(Mathf.DeltaAngle(target, knobTransform.eulerAngles.z));
        hum.SetFader(Mathf.InverseLerp(resolveStartThreshold, resolveEndThreshold, distToTarget));

        bool hitTarget = distToTarget < resolveEndThreshold;
        if (hitTarget && !isWinning)
        {
            isWinning = true;
            victoryTimer = victoryHoldTime;
        }
        else if (!hitTarget && isWinning)
        {
            isWinning = false;
        }

        if (isWinning)
        {
            victoryTimer -= Time.deltaTime;
            if (victoryTimer <= 0f)
            {
                station.OnGameWon();
                gameObject.SetActive(false);
            }
        }
    }
}
