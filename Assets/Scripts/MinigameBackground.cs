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
    public Button closeButton;

    public float progress = 0f;
    private float lerpValue = 0f;

    public void SetProgress(float progress)
    {
        this.progress = progress;
    }

    private void OnDisable()
    {
        lerpValue = 0;
        screenImage.color = badColor;
        // this ensures the button is in the right hover state after closing a game
        closeButton.interactable = false;
        closeButton.interactable = true;
    }

    private void Update()
    {
        lerpValue = Mathf.MoveTowards(lerpValue, progress, Time.deltaTime * transitionSpeed);
        screenImage.color = Color.Lerp(badColor, goodColor, lerpValue);
    }
}
