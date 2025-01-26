using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;  // Fuente de audio para la m�sica
    public AudioClip songClip;  // La canci�n que se reproducir�

    void Start()
    {
        audioSource.clip = songClip;
        audioSource.Play();  // Reproduce la canci�n desde el principio
    }

    void Update()
    {
        // Puedes usar la posici�n actual de la canci�n para generar las notas en momentos espec�ficos
        // Por ejemplo, puedes hacer que las notas aparezcan en funci�n de la posici�n de la canci�n:
        // float currentTime = audioSource.time;
    }
}
