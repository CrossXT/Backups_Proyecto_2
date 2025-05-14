using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CreditScroll : MonoBehaviour
{
    public RectTransform creditText;
    public float scrollSpeed = 30f;
    public Image fadeOverlay;
    public float fadeDuration = 2f;
    public string mainMenuSceneName = "Menu";
    public AudioSource musicSource;

    private bool fadeStarted = false;

    void Start()
    {
        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    void Update()
    {
        creditText.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        if (!fadeStarted && creditText.anchoredPosition.y >= 1200f)
        {
            StartCoroutine(FadeAndReturnToMenu());
            fadeStarted = true;
        }
    }

    IEnumerator FadeAndReturnToMenu()
    {
        float elapsed = 0f;
        Color color = fadeOverlay.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);
            fadeOverlay.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        SceneManager.LoadScene(mainMenuSceneName);
    }
}
