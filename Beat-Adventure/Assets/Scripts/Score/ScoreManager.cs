using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0; // Puntuaci�n actual
    public TMPro.TextMeshProUGUI scoreText; // Para mostrar el puntaje en la UI

    void Start()
    {
        // Asegurarse de que el puntaje se muestre al inicio
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    // M�todo para agregar puntajes
    public void AddScore(int points)
    {
        score += points;

        // Actualiza el puntaje en la UI
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    // M�todo para obtener la puntuaci�n actual
    public int GetScore()
    {
        return score;
    }
}
