using UnityEngine;
using UnityEngine.InputSystem;

public class Note : MonoBehaviour
{
    public bool isHit = false;
    private float hitPositionY = -4f;

    private GameInputActions inputActions; // Cambiamos PlayerInput por GameInputActions
    public string assignedKey;  // Tecla asignada a esta nota (Ej: "D", "F", "J", "K")

    void Awake()
    {
        // Inicializamos correctamente el inputActions
        inputActions = new GameInputActions();
        inputActions.Enable(); // Activamos las acciones
    }

    void Update()
    {
        // Si la nota pasa la posición de fallo y no fue golpeada, se destruye
        if (transform.position.y < hitPositionY && !isHit)
        {
            Destroy(gameObject);
        }

        // Si el jugador presiona la tecla correcta, golpea la nota
        if (DetectaInput())
        {
            Hit();
        }
    }

    bool DetectaInput()
    {
        // Verificamos si la acción "Hit" fue activada y la tecla está presionada
        switch (assignedKey)
        {
            case "D":
                return inputActions.Gameplay.Hit.triggered && Keyboard.current.dKey.isPressed;
            case "F":
                return inputActions.Gameplay.Hit.triggered && Keyboard.current.fKey.isPressed;
            case "J":
                return inputActions.Gameplay.Hit.triggered && Keyboard.current.jKey.isPressed;
            case "K":
                return inputActions.Gameplay.Hit.triggered && Keyboard.current.kKey.isPressed;
            default:
                return false;
        }
    }

    public void Hit()
    {
        if (!isHit)
        {
            isHit = true;
            Debug.Log("Nota acertada: " + assignedKey);
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        inputActions.Disable(); // Deshabilitamos las acciones cuando la nota es destruida
    }
}
