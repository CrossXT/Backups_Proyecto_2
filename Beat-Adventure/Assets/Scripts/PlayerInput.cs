using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public KeyCode keyToPress = KeyCode.Space;  // La tecla que debe presionar el jugador (puedes cambiarlo)
    private int score = 0;  // Puntuación del jugador
    private float hitWindow = 0.1f;  // Tiempo de ventana para "acertar" una nota

    void Update()
    {
        // Detecta cuando se presiona la tecla correcta
        if (Input.GetKeyDown(keyToPress))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);

            if (hit.collider != null && hit.collider.CompareTag("Note"))
            {
                float distance = Mathf.Abs(hit.collider.transform.position.y - transform.position.y);

                if (distance <= hitWindow)
                {
                    score += 100;  // Sumar puntos por un "acierto"
                    hit.collider.GetComponent<Note>().Hit();  // Marca la nota como acertada
                    Debug.Log("¡Acierto! Puntuación: " + score);
                }
                else
                {
                    Debug.Log("¡Fallaste!");
                }
            }
        }
    }
}
