using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip songClip;
    private bool hasEnded = false;

    void Start()
    {
        audioSource.clip = songClip;
        audioSource.Play();
    }

    void Update()
    {
        if (!audioSource.isPlaying && !hasEnded && audioSource.time > 0f)
        {
            hasEnded = true;
            StartCoroutine(FadeOutAndLoad());
        }
    }

    IEnumerator FadeOutAndLoad()
    {
        
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0.01f)
        {
            audioSource.volume -= startVolume * Time.deltaTime / 1.5f; // Duración: 1.5 segundos
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;

        
        DestinyScene.nombreEscena = "LevelSelector"; // Define a dónde irá la pantalla de carga
        SceneManager.LoadScene("LoadingScene"); // Carga la escena de carga (ya existente)
    }
}
