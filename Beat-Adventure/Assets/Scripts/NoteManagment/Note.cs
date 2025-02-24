using UnityEngine;

public class Note : MonoBehaviour
{
    public bool isHit = false;  // Para verificar si la nota ha sido golpeada
    private float hitPositionY = -4f;  // Posición en Y donde la nota será destruida si no se golpea

    void Update()
    {
        // Si la nota pasa la posición de "fallo", se destruye
        if (transform.position.y < hitPositionY && !isHit)
        {
            Destroy(gameObject);
        }
    }

    // Método para marcar la nota como "acertada"
    public void Hit()
    {
        if (!isHit)
        {
            isHit = true;
            Destroy(gameObject); // Destruye la nota si es golpeada
        }
    }
}
