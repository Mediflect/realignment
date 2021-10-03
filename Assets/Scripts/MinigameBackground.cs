using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameBackground : MonoBehaviour
{
    public Image screenImage;
    public Color badColor;
    public Color goodColor;
    public float transitionSpeed = 1f;

    public float progress = 0f;
    private float lerpValue = 0f;

    public void SetProgress(float progress)
    {
        this.progress = progress;
    }

    private void Update()
    {
        lerpValue = Mathf.MoveTowards(lerpValue, progress, Time.deltaTime * transitionSpeed);
        screenImage.color = Color.Lerp(badColor, goodColor, lerpValue);
    }
}
