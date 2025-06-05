
using UnityEngine;
using UnityEngine.InputSystem;

public class Note : MonoBehaviour
{

    public bool isHit = false;
    private float hitPositionY = -4f;


    public string assignedKey;
    public int scoreValue = 100;

    private bool isInsideTarget = false;

    public AudioClip hitSoundClip;
    public GameObject hitParticlesPrefab;




    void Start()
    {
        // El ScoreManager ahora no se maneja desde aquí.
    }

    void Update()
    {
        if (transform.position.y < hitPositionY && !isHit)
        {
            Debug.Log("Nota fallada: " + assignedKey);

            if (ComboManager.Instance != null)
            {
                ComboManager.Instance.ResetCombo(); 
            }

            Destroy(gameObject);
        }

        if (isInsideTarget && TeclaPresionada())
        {
            Hit();
        }
    }

    bool TeclaPresionada()
    {
        if (string.IsNullOrEmpty(assignedKey)) return false;

        var keyboard = Keyboard.current;
        if (keyboard == null) return false;

        switch (assignedKey.ToLower())
        {
            case "d": return keyboard.dKey.wasPressedThisFrame;
            case "f": return keyboard.fKey.wasPressedThisFrame;
            case "j": return keyboard.jKey.wasPressedThisFrame;
            case "k": return keyboard.kKey.wasPressedThisFrame;
            case "space": return keyboard.spaceKey.wasPressedThisFrame;
            case "left": return keyboard.leftArrowKey.wasPressedThisFrame;
            case "right": return keyboard.rightArrowKey.wasPressedThisFrame;
            case "enter": return keyboard.enterKey.wasPressedThisFrame;
            default: return false;
        }
    }

    public void Hit()
    {
        if (!isHit)
        {
            isHit = true;
            Debug.Log("Nota acertada: " + assignedKey);

            //puntuación
            ScoreManager scoreManager = UnityEngine.Object.FindFirstObjectByType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AddScore(scoreValue);
            }

            // sonido 
            if (hitSoundClip != null)
            {
                AudioSource.PlayClipAtPoint(hitSoundClip, Camera.main.transform.position, 0.5f);
            }

            // partículas
            if (hitParticlesPrefab != null)
            {
                Instantiate(hitParticlesPrefab, transform.position, Quaternion.identity);
            }

            // combo
            if (ComboManager.Instance != null)
            {
                ComboManager.Instance.IncreaseCombo();
            }
            else
            {
                Debug.LogWarning("ComboManager.Instance es null.");
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
}
