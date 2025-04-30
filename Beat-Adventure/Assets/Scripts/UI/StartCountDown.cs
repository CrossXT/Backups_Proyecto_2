using System.Collections;
using TMPro;
using UnityEngine;

public class StartCountdown : MonoBehaviour
{
    public TextMeshProUGUI startText;
    public float initialDelay = 0.5f; // Pequeña pausa antes del mensaje
    public float delayBetweenWords = 1f;

    void Start()
    {
        Time.timeScale = 0f; // Detiene el juego al inicio
        StartCoroutine(CountdownRoutine());
    }

    IEnumerator CountdownRoutine()
    {
        yield return new WaitForSecondsRealtime(initialDelay);

        startText.gameObject.SetActive(true);
        startText.text = "¡Preparados!";
        yield return new WaitForSecondsRealtime(delayBetweenWords);

        startText.text = "¡Listos!";
        yield return new WaitForSecondsRealtime(delayBetweenWords);

        startText.text = "¡YA!";
        yield return new WaitForSecondsRealtime(delayBetweenWords);

        startText.gameObject.SetActive(false);

        Time.timeScale = 1f; // Reanuda el juego
    }
}
