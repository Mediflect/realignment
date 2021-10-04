using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLine : MonoBehaviour
{
    public LineFollower followerPrefab;
    public LineRenderer lineRenderer;
    public float sendPeriod = 5f;

    private List<LineFollower> instantiatedFollowers = new List<LineFollower>();
    private float sendTimer = 0f;
    private bool followersGoReverse = false;

    public void SetReverse(bool reverse)
    {
        if (followersGoReverse == reverse)
        {
            return;
        }
        followersGoReverse = reverse;
        // reclaim all followers so none are going the wrong way
        for (int i = 0; i < instantiatedFollowers.Count; ++i)
        {
            instantiatedFollowers[i].gameObject.SetActive(false);
            sendTimer = 0f;
        }
    }

    private void OnEnable()
    {
        sendTimer = 0f;
    }

    private void Update()
    {
        sendTimer -= Time.deltaTime;
        if (sendTimer <= 0f)
        {
            LineFollower follower = GetNextFollower();
            follower.gameObject.SetActive(true);
            follower.reverse = followersGoReverse;
            sendTimer = sendPeriod;
        }
    }

    private LineFollower GetNextFollower()
    {
        LineFollower follower = null;
        for (int i = 0; i < instantiatedFollowers.Count; ++i)
        {
            if (!instantiatedFollowers[i].gameObject.activeSelf)
            {
                follower = instantiatedFollowers[i];
                break;
            }
        }
        if (follower == null)
        {
            follower = Instantiate(followerPrefab);
            follower.transform.SetParent(transform);
            follower.lineRenderer = lineRenderer;
            // this is hacky af but it fixes some weird initialization order stuff
            follower.enabled = false;
            follower.SetInitialized();
            follower.enabled = true;
            instantiatedFollowers.Add(follower);
        }
        return follower;
    }

    [ContextMenu("Reverse")]
    private void DebugReverse()
    {
        SetReverse(true);
    }

    [ContextMenu("Un-reverse")]
    private void DebugUnreverse()
    {
        SetReverse(false);
    }
}
