using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SplashScreen : MonoBehaviour
{
    public CanvasGroup logoCanvas;
    public float fadeDuration = 2f; // Duración del fade

    void Start()
    {
        StartCoroutine(PlaySplashScreen());
    }

    IEnumerator PlaySplashScreen()
    {
        // Fade In
        yield return StartCoroutine(Fade(0, 1, fadeDuration));

        // Espera un poco con el logo visible
        yield return new WaitForSeconds(2f);

        // Fade Out
        yield return StartCoroutine(Fade(1, 0, fadeDuration));

        // Cargar la siguiente escena
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            logoCanvas.alpha = Mathf.Lerp(startAlpha, endAlpha, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        logoCanvas.alpha = endAlpha;
    }
}
