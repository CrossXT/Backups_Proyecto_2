using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.InputSystem; // Necesario para la entrada del teclado

public class HoldNote : MonoBehaviour
{
    public float duration = 1f; // Duración en segundos
    public float speed = 5f;    // Velocidad de caída
    public string assignedKey;  // Tecla asignada a esta nota

    public AudioClip holdHitSoundClip;


    private Transform head;
    private Transform body;
    private Transform tail;

    private bool isKeyPressed = false; // Si la tecla está presionada
    private float keyPressDuration = 0f; // El tiempo durante el cual la tecla está presionada
    private const float requiredDuration = 1.5f; // Duración que debe mantenerse la tecla

    void Start()
    {
        head = transform.Find("Head"); // Encuentra el cuerpo
        body = transform.Find("Body"); // Encuentra el cuerpo
        tail = transform.Find("Tail"); // Encuentra la cola

        AdjustSize(); // Ajusta el tamaño al inicio

        // Cargar la tecla asignada desde PlayerPrefs si existe
        if (PlayerPrefs.HasKey(assignedKey))
        {
            assignedKey = PlayerPrefs.GetString(assignedKey);
        }
    }

    void Update()
    {
        // Movimiento hacia abajo
        transform.position += Vector3.down * speed * Time.deltaTime;

        // Verifica si la tecla asignada está siendo presionada
        if (TeclaPresionada())
        {
            if (!isKeyPressed)
            {
                isKeyPressed = true;
                keyPressDuration = 0f; // Resetear el tiempo de presión de tecla
            }

            keyPressDuration += Time.deltaTime;

            // Si la duración de la tecla mantenida alcanza el tiempo necesario, la nota se considera acertada
            if (keyPressDuration >= requiredDuration)
            {
                Hit();
            }
        }
        else
        {
            if (isKeyPressed)
            {
                // Si la tecla se soltó antes de tiempo
                if (keyPressDuration < requiredDuration)
                {
                    Debug.Log("Tecla soltada antes de tiempo.");
                }

                isKeyPressed = false;
                keyPressDuration = 0f;
            }
        }
    }

    bool TeclaPresionada()
    {
        if (string.IsNullOrEmpty(assignedKey)) return false;

        var keyboard = Keyboard.current;
        if (keyboard == null) return false; // Asegura que el teclado esté activo

        switch (assignedKey.ToLower())
        {
            case "d": return keyboard.dKey.isPressed;
            case "f": return keyboard.fKey.isPressed;
            case "j": return keyboard.jKey.isPressed;
            case "k": return keyboard.kKey.isPressed;
            case "space": return keyboard.spaceKey.isPressed;
            case "left": return keyboard.leftArrowKey.isPressed;
            case "right": return keyboard.rightArrowKey.isPressed;
            case "enter": return keyboard.enterKey.isPressed;
            default: return false; // Si la tecla no está en la lista, no hace nada
        }
    }

    public void Hit()
    {
        Debug.Log("Nota mantenida correctamente: " + assignedKey);

        if (holdHitSoundClip != null)
        {
            AudioSource.PlayClipAtPoint(holdHitSoundClip, Camera.main.transform.position, 0.5f);
        }

        Destroy(gameObject);
    }



    public void SetNoteLength(float newDuration)
    {
        duration = newDuration;
        AdjustSize(); // Ajustar tamaño cuando cambia la duración
    }

    private void AdjustSize()
    {
        if (body != null)
        {
            float newBodyHeight = duration * speed; // Calculamos la altura según la duración

            // Aplicamos la escala en Y sin afectar X ni Z
            body.localScale = new Vector3(1, newBodyHeight, 1);

            // Ajustamos la posición de la cola para que quede bien alineada
            if (tail != null)
            {
                tail.localPosition = new Vector3(0, -newBodyHeight / 2f, 0);
            }

            float bodyLength = Mathf.Abs(head.transform.position.y - tail.transform.position.y);

            body.transform.position = (head.transform.position + tail.transform.position) / 2f;
            body.transform.localScale = new Vector3(1f, bodyLength, 1f);

            //// Ajustamos el BoxCollider2D del cuerpo si existe
            //BoxCollider2D bodyCollider = body.GetComponent<BoxCollider2D>();
            //if (bodyCollider != null)
            //{
            //    bodyCollider.size = new Vector2(bodyCollider.size.x, newBodyHeight);
            //    bodyCollider.offset = new Vector2(bodyCollider.offset.x, -newBodyHeight / 2f);
            //}
        }
    }
}
