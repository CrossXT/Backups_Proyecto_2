using UnityEngine;

public class HoldNote : MonoBehaviour
{
    public float duration = 1f; // Duración en segundos
    public float speed = 5f;    // Velocidad de caída

    private Transform body;
    private Transform tail;

    void Start()
    {
        body = transform.Find("Body"); // Encuentra el cuerpo
        tail = transform.Find("Tail"); // Encuentra la cola

        AdjustSize(); // Ajusta el tamaño al inicio
    }

    void Update()
    {
        // Movimiento hacia abajo
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    public void SetNoteLength(float newDuration)
    {
        duration = newDuration;
        AdjustSize();
    }

    private void AdjustSize()
    {
        if (body != null)
        {
            float lengthFactor = duration * 2f; // Ajusta el factor de escala
            body.localScale = new Vector3(1, lengthFactor, 1);

            // Ajusta la posición de la cola al final del cuerpo
            if (tail != null)
            {
                tail.localPosition = new Vector3(0, -lengthFactor, 0);
            }
        }
    }
}
