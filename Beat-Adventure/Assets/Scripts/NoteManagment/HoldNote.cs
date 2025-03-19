using UnityEngine;
using UnityEngine.InputSystem; // Necesario para la entrada del teclado

public class HoldNote : MonoBehaviour
{
    public float duration = 1f; // Duraci�n en segundos
    public float speed = 5f;    // Velocidad de ca�da
    public string assignedKey;  // Tecla asignada a esta nota

    private Transform body;
    private Transform tail;

    private bool isKeyPressed = false; // Si la tecla est� presionada
    private float keyPressDuration = 0f; // El tiempo durante el cual la tecla est� presionada
    private const float requiredDuration = 1.5f; // Duraci�n que debe mantenerse la tecla

    void Start()
    {
        body = transform.Find("Body"); // Encuentra el cuerpo
        tail = transform.Find("Tail"); // Encuentra la cola

        AdjustSize(); // Ajusta el tama�o al inicio

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

        // Verifica si la tecla asignada est� siendo presionada
        if (TeclaPresionada())
        {
            if (!isKeyPressed)
            {
                isKeyPressed = true;
                keyPressDuration = 0f; // Resetear el tiempo de presi�n de tecla
            }

            keyPressDuration += Time.deltaTime;

            // Si la duraci�n de la tecla mantenida alcanza el tiempo necesario, la nota se considera acertada
            if (keyPressDuration >= requiredDuration)
            {
                Hit();
            }
        }
        else
        {
            if (isKeyPressed)
            {
                // Si la tecla se solt� antes de tiempo
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
        if (keyboard == null) return false; // Asegura que el teclado est� activo

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
            default: return false; // Si la tecla no est� en la lista, no hace nada
        }
    }

    public void Hit()
    {
        // Aqu� puedes agregar lo que sucede cuando la nota se mantiene correctamente
        Debug.Log("Nota mantenida correctamente: " + assignedKey);
        Destroy(gameObject); // Destruir la nota despu�s de que se haya mantenido
    }

    private void AdjustSize()
    {
        if (body != null)
        {
            float newBodyHeight = duration * speed; // Calculamos la altura seg�n la duraci�n

            // Aplicamos la escala en Y sin afectar X ni Z
            body.localScale = new Vector3(1, newBodyHeight, 1);

            // Ajustamos la posici�n de la cola para que quede bien alineada
            if (tail != null)
            {
                tail.localPosition = new Vector3(0, -newBodyHeight / 2f, 0);
            }

            // Ajustamos el BoxCollider2D del cuerpo si existe
            BoxCollider2D bodyCollider = body.GetComponent<BoxCollider2D>();
            if (bodyCollider != null)
            {
                bodyCollider.size = new Vector2(bodyCollider.size.x, newBodyHeight);
                bodyCollider.offset = new Vector2(bodyCollider.offset.x, -newBodyHeight / 2f);
            }
        }
    }

}
