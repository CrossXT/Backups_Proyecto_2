using UnityEngine;
using TMPro; // Importar TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText; // Referencia al TextMeshPro
    private int score = 0;  // Puntuaci�n actual

    void Start()
    {
        UpdateScoreUI();
    }

    // M�todo para a�adir puntos
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    // M�todo para actualizar el texto en pantalla
    private void UpdateScoreUI()
    {
        scoreText.text = score.ToString();
    }
}
