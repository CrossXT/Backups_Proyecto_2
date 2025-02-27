using UnityEngine;
using UnityEngine.UI; // Para usar los UI elements
using UnityEngine.SceneManagement; // Para cambiar de escena

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel; // Panel que se activará durante la pausa
    public Button restartButton;  // Botón para reiniciar el nivel
    public Button levelSelectButton; // Botón para volver al selector de nivel

    private bool isPaused = false;  // Controla si el juego está pausado

    void Start()
    {
        // Inicializa los botones
        restartButton.onClick.AddListener(RestartLevel);
        levelSelectButton.onClick.AddListener(GoToLevelSelector);

        // Asegúrate de que el panel de pausa esté inicialmente desactivado
        pausePanel.SetActive(false);
    }

    void Update()
    {
        // Detectar si se presiona "ESC" para pausar o reanudar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame(); // Reanudar el juego
            }
            else
            {
                PauseGame(); // Pausar el juego
            }
        }
    }

    void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true); // Muestra el panel de pausa
        Time.timeScale = 0f; // Detiene el tiempo del juego
    }

    void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false); // Oculta el panel de pausa
        Time.timeScale = 1f; // Reanuda el tiempo del juego
    }

    void RestartLevel()
    {
        Time.timeScale = 1f; // Asegúrate de reanudar el tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia la escena actual
    }

    void GoToLevelSelector()
    {
        Time.timeScale = 1f; // Asegúrate de reanudar el tiempo
        SceneManager.LoadScene("LevelSelector"); // Carga la escena del selector de nivel (ajusta el nombre de la escena)
    }
}
