using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;  // Fuente de audio para la música
    public AudioClip songClip;  // La canción que se reproducirá

    void Start()
    {
        audioSource.clip = songClip;
        audioSource.Play();  // Reproduce la canción desde el principio
    }

    void Update()
    {
        // Puedes usar la posición actual de la canción para generar las notas en momentos específicos
        // Por ejemplo, puedes hacer que las notas aparezcan en función de la posición de la canción:
        // float currentTime = audioSource.time;
    }
}
