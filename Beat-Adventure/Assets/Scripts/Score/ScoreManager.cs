using UnityEngine;
using UnityEngine.UI;  // Necesario para interactuar con la UI de Unity

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;  // Referencia al componente Text para mostrar la puntuación
    private int score = 0;  // Puntuación actual

    void Update()
    {
        // Actualiza el texto de la puntuación en pantalla
        scoreText.text = "Puntuación: " + score;
    }

    // Método para añadir puntos (lo puedes llamar desde el script de PlayerInput)
    public void AddScore(int points)
    {
        score += points;
    }
}
