using UnityEngine;
using UnityEngine.InputSystem;

public class Note : MonoBehaviour
{
    public bool isHit = false;
    private float hitPositionY = -4f;

    private GameInputActions inputActions;
    private ScoreManager scoreManager;
    public string assignedKey;
    public int scoreValue = 100;

    private bool isInsideTarget = false;

    void Awake()
    {
        inputActions = new GameInputActions();
        inputActions.Enable();
    }

    void Start()
    {
        scoreManager = Object.FindFirstObjectByType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager no encontrado en la escena.");
        }
    }

    void Update()
    {
        if (transform.position.y < hitPositionY && !isHit)
        {
            Debug.Log("Nota fallada: " + assignedKey);
            Destroy(gameObject);
        }

        if (isInsideTarget && TeclaPresionada())
        {
            Hit();
        }
    }

    bool TeclaPresionada()
    {
        switch (assignedKey.ToLower()) // Convierte la tecla asignada a minúsculas
        {
            case "d": return Keyboard.current.dKey.wasPressedThisFrame;
            case "f": return Keyboard.current.fKey.wasPressedThisFrame;
            case "j": return Keyboard.current.jKey.wasPressedThisFrame;
            case "k": return Keyboard.current.kKey.wasPressedThisFrame;
            case "space": return Keyboard.current.spaceKey.wasPressedThisFrame;
            case "left": return Keyboard.current.leftArrowKey.wasPressedThisFrame;
            case "right": return Keyboard.current.rightArrowKey.wasPressedThisFrame;
            case "enter": return Keyboard.current.enterKey.wasPressedThisFrame;
            default: return false;
        }
    }

    public void Hit()
    {
        if (!isHit)
        {
            isHit = true;
            Debug.Log("Nota acertada: " + assignedKey);

            if (scoreManager != null)
            {
                scoreManager.AddScore(scoreValue);
            }

            Destroy(gameObject);
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

    void OnDestroy()
    {
        inputActions.Disable();
    }
}
