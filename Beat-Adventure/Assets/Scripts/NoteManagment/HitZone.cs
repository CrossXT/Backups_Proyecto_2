using UnityEngine;

public class HitZone : MonoBehaviour
{
    // Puedes hacer que las notas que entran en esta zona sean "acertadas"
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
        {
            // Llama al método Hit de la nota si entra en la zona
            other.GetComponent<Note>().Hit();
        }
    }
}
