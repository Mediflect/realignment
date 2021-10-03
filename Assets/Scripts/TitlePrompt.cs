using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class TitlePrompt : MonoBehaviour
{
    public string mainSceneName;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI promptText;
    public TextMeshProUGUI soundText;
    public float textFadeTime = 2f;
    public float pauseTime = 1f;
    public FullScreenFade fadeOut;

    private bool allowAdvance = false;
    private Coroutine transitionCoroutine;

    private void Awake()
    {
        Helpers.SetTextAlpha(titleText, 0);
        Helpers.SetTextAlpha(promptText, 0);
        Helpers.SetTextAlpha(soundText, 0);
        App.CameFromTitle = true;
    }

    private void OnEnable()
    {
        Helpers.SetTextAlpha(titleText, 0);
        Helpers.SetTextAlpha(promptText, 0);
        Helpers.SetTextAlpha(soundText, 0);
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
            Helpers.SetTextAlpha(titleText, alpha);
            titleFadeTimer -= Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(pauseTime);

        float promptFadeTimer = textFadeTime;
        while (promptFadeTimer > 0f)
        {
            float alpha = Mathf.InverseLerp(textFadeTime, 0, promptFadeTimer);
            Helpers.SetTextAlpha(promptText, alpha);
            Helpers.SetTextAlpha(soundText, alpha);
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
