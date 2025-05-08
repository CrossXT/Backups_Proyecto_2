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

    private bool fadeStarted = false;

    void Update()
    {
        // Mueve el texto hacia arriba
        creditText.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        // Cuando el texto sale completamente de la pantalla, empieza el fade
        if (!fadeStarted && creditText.anchoredPosition.y >= 1200f) // ajusta según el contenido
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
