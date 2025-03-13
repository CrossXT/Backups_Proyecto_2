using UnityEngine;
using UnityEngine.UI; // Necesario para manejar la UI
using UnityEngine.InputSystem;

public class Note : MonoBehaviour
{
    public bool isHit = false;
    private float hitPositionY = -4f;
    private ScoreManager scoreManager;

    public string assignedKey;  // Nombre de la acción (Ej: "D", "F", etc.)
    public int scoreValue = 100;

    private bool isInsideTarget = false;

    // Nuevas variables para mantener presionada la tecla

    public bool holdToHit = false; // Si es true, hay que mantener presionada la tecla
    private bool isKeyPressed = false; // Si la tecla está presionada
    private float keyPressDuration = 0f; // El tiempo durante el cual la tecla ha sido presionada
    private const float requiredDuration = 1.5f; // Duración que debe mantenerse la tecla

    // Referencia a la barra de progreso (UI)
    public Slider progressBar; // Enlazar el Slider desde el Inspector

    void Start()
    {
        scoreManager = UnityEngine.Object.FindFirstObjectByType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager no encontrado en la escena.");
        }

        // Cargar la tecla asignada desde PlayerPrefs si existe
        if (PlayerPrefs.HasKey(assignedKey))
        {
            assignedKey = PlayerPrefs.GetString(assignedKey);
        }

        if (progressBar != null)
        {
            progressBar.value = 0;  // Inicializa la barra de progreso
            progressBar.gameObject.SetActive(false);  // Asegúrate de que la barra está oculta al principio
        }
    }

    void Update()
    {
        if (transform.position.y < hitPositionY && !isHit)
        {
            Debug.Log("Nota fallada: " + assignedKey);
            Destroy(gameObject);
        }

        if (isInsideTarget)
        {
            if (holdToHit)
            {
                if (TeclaPresionada())
                {
                    // Si la tecla fue presionada por primera vez, comenzamos a contar el tiempo
                    if (!isKeyPressed)
                    {
                        isKeyPressed = true;
                        keyPressDuration = 0f;

                        // Hacer visible la barra de progreso
                        if (progressBar != null)
                        {
                            progressBar.gameObject.SetActive(true);
                        }
                    }

                    // Si la tecla sigue presionada, acumulamos el tiempo
                    keyPressDuration += Time.deltaTime;
                    progressBar.value = keyPressDuration / requiredDuration; // Llenar la barra proporcionalmente

                    // Si el tiempo acumulado es suficiente, consideramos que la nota fue mantenida correctamente
                    if (keyPressDuration >= requiredDuration)
                    {
                        Hit(); // "Hit" si la tecla fue mantenida por el tiempo suficiente
                    }
                }
                else
                {
                    // Si la tecla se ha soltado antes de completar el tiempo requerido
                    if (isKeyPressed)
                    {
                        if (keyPressDuration < requiredDuration)
                        {
                            // Fallar si la duración no es suficiente
                            Debug.Log("Tecla soltada antes de tiempo.");
                        }

                        isKeyPressed = false;
                        keyPressDuration = 0f;

                        // Ocultar la barra de progreso
                        if (progressBar != null)
                        {
                            progressBar.gameObject.SetActive(false);
                        }
                    }
                }
            }
            else
            {
                if (TeclaPresionada())
                {
                    Hit();  // Si no es necesario mantener la tecla, se registra el "hit" de inmediato
                }
            }
        }
    }

    bool TeclaPresionada()
    {
        if (string.IsNullOrEmpty(assignedKey)) return false;

        var keyboard = Keyboard.current;
        if (keyboard == null) return false; // Asegurar que el teclado está activo

        // Comprobación de teclas con un switch
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
        if (!isHit)
        {
            isHit = true;
            Debug.Log("Nota acertada: " + assignedKey);

            // Añadir puntuación
            if (scoreManager != null)
            {
                scoreManager.AddScore(scoreValue);
            }

            // Ocultar la barra de progreso al completar el "hit"
            if (progressBar != null)
            {
                progressBar.gameObject.SetActive(false);
            }

            Destroy(gameObject); // Destruir la nota
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            isInsideTarget = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            isInsideTarget = false;
        }
    }
}
