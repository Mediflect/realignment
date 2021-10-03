using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLightMover : MonoBehaviour
{
    public Transform leftPos;
    public Transform rightPos;
    public SpriteRenderer spriteRenderer;

    public void Update()
    {
        if (transform.parent == leftPos && spriteRenderer.flipX)
        {
            transform.SetParent(rightPos, false);
        }

        if (transform.parent == rightPos && !spriteRenderer.flipX)
        {
            transform.SetParent(leftPos, false);
        }
    }
}
