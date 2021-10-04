using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFollower : MonoBehaviour
{
    private const int MaxLinePositions = 30;    // should be plenty;

    public event System.Action StoppedMoving;

    public LineRenderer lineRenderer;
    public bool reverse = false;
    public float speed = 1f;
    
    private Vector3[] cachedPositions = new Vector3[MaxLinePositions];    // 30 should be plenty
    private int numPositions;
    private int currentPosIndex = 0;
    private bool isInitialized = false;

    public void SetInitialized()
    {
        isInitialized = true;
    }

    private void OnEnable()
    {
        if (!isInitialized)
        {
            return;
        }

        numPositions = lineRenderer.GetPositions(cachedPositions);
        if (numPositions > MaxLinePositions)
        {
            Debug.LogError("uh oh too many line positions");
            numPositions = MaxLinePositions;
        }
        if (reverse)
        {
            Array.Reverse(cachedPositions, 0, numPositions);
        }
        currentPosIndex = 0;
        transform.position = new Vector3(cachedPositions[0].x ,cachedPositions[0].y, 0);
    }

    private void Update()
    {   
        if (!isInitialized)
        {
            return;
        }

        float moveDelta = speed * Time.deltaTime;
        float leftoverDist = MoveAlongLine(moveDelta);
        if (leftoverDist > 0f)
        {
            ++currentPosIndex;
            if (currentPosIndex == numPositions - 1)
            {
                // stop moving
                gameObject.SetActive(false);
                return;
            }
            // Technically this would be wacky if there are tons of super close together positions... but whatever
            MoveAlongLine(leftoverDist);
        }
    }

    private float MoveAlongLine(float moveDelta)
    {
        Vector3 nextPoint = cachedPositions[currentPosIndex + 1];
        float distToNext = Vector3.Distance(transform.position, nextPoint);
        transform.position = Vector3.MoveTowards(transform.position, nextPoint, moveDelta);
        float leftoverDist = moveDelta - distToNext;
        return leftoverDist;
    }
}
