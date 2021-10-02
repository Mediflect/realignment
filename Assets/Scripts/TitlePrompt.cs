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

    private void Awake()
    {
        titleText.color = new Color(titleText.color.r, titleText.color.g, titleText.color.b, 0f);
        promptText.color = new Color(promptText.color.r, promptText.color.g, promptText.color.b, 0f);
    }

    private void OnEnable()
    {
        titleText.color = new Color(titleText.color.r, titleText.color.g, titleText.color.b, 0f);
        promptText.color = new Color(promptText.color.r, promptText.color.g, promptText.color.b, 0f);
        StartCoroutine(TextFadeIn());
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(mainSceneName);
        }
    }

    private IEnumerator TextFadeIn()
    {
        float titleFadeTimer = textFadeTime;
        while(titleFadeTimer > 0f)
        {
            float alpha = Mathf.InverseLerp(textFadeTime, 0, titleFadeTimer);
            titleText.color = new Color(titleText.color.r, titleText.color.g, titleText.color.b, alpha);
            titleFadeTimer -= Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(pauseTime);

        float promptFadeTimer = textFadeTime;
        while(promptFadeTimer > 0f)
        {
            float alpha = Mathf.InverseLerp(textFadeTime, 0, promptFadeTimer);
            promptText.color = new Color(promptText.color.r, promptText.color.g, promptText.color.b, alpha);
            promptFadeTimer -= Time.deltaTime;
            yield return null;
        }
    }
}
