using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class TitlePrompt : MonoBehaviour
{
    public string mainSceneName;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI promptText;
    public float textFadeTime = 2f;
    public float pauseTime = 1f;
    public FullScreenFade fadeOut;

    private bool allowAdvance = false;
    private Coroutine transitionCoroutine;

    private void Awake()
    {
        titleText.color = new Color(titleText.color.r, titleText.color.g, titleText.color.b, 0f);
        promptText.color = new Color(promptText.color.r, promptText.color.g, promptText.color.b, 0f);
        App.CameFromTitle = true;
    }

    private void OnEnable()
    {
        titleText.color = new Color(titleText.color.r, titleText.color.g, titleText.color.b, 0f);
        promptText.color = new Color(promptText.color.r, promptText.color.g, promptText.color.b, 0f);
        StartCoroutine(TextFadeIn());
    }

    private void OnDisable()
    {
        fadeOut.FadeFinished -= LoadGameScene;
    }

    private void Update()
    {
        if (allowAdvance && Input.anyKeyDown && !fadeOut.gameObject.activeSelf)
        {
            fadeOut.gameObject.SetActive(true);
            fadeOut.FadeFinished += LoadGameScene;
        }
    }

    private IEnumerator TextFadeIn()
    {
        float titleFadeTimer = textFadeTime;
        while (titleFadeTimer > 0f)
        {
            float alpha = Mathf.InverseLerp(textFadeTime, 0, titleFadeTimer);
            titleText.color = new Color(titleText.color.r, titleText.color.g, titleText.color.b, alpha);
            titleFadeTimer -= Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(pauseTime);

        float promptFadeTimer = textFadeTime;
        while (promptFadeTimer > 0f)
        {
            float alpha = Mathf.InverseLerp(textFadeTime, 0, promptFadeTimer);
            promptText.color = new Color(promptText.color.r, promptText.color.g, promptText.color.b, alpha);
            promptFadeTimer -= Time.deltaTime;
            yield return null;
        }
        allowAdvance = true;
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene(mainSceneName);
    }
}
