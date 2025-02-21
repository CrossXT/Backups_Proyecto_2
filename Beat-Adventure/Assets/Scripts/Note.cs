using UnityEngine;

public class Note : MonoBehaviour
{
    public float speed = 3f;  // Velocidad con la que la nota baja
    public bool isHit = false;  // Para verificar si la nota ha sido golpeada
    private float hitPositionY = -4f;  // Posición en Y donde la nota será destruida si no se golpea

    void Update()
    {
        // Movimiento hacia abajo
        transform.Translate(Vector3.down * speed * Time.deltaTime);

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
