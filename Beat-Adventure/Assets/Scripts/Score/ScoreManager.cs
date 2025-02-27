using UnityEngine;
using TMPro; // Importar TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText; // Referencia al TextMeshPro
    private int score = 0;  // Puntuación actual

    void Start()
    {
        UpdateScoreUI();
    }

    // Método para añadir puntos
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    // Método para actualizar el texto en pantalla
    private void UpdateScoreUI()
    {
        scoreText.text = score.ToString();
    }
}
