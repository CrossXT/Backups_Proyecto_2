using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public Button restartButton;
    public Button levelSelectButton;

    private bool isPaused = false;

    void Start()
    {
        restartButton.onClick.AddListener(RestartLevel);
        levelSelectButton.onClick.AddListener(GoToLevelSelector);
        pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);

        // Pausa el juego (afecta a las físicas, movimientos, animaciones con deltaTime)
        Time.timeScale = 0f;

        // Asegura que el sistema UI esté activo
        Canvas canvas = pausePanel.GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }
    }

    void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GoToLevelSelector()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelector");
    }
}
