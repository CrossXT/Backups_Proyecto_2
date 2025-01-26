using UnityEngine;
using UnityEngine.UI;  // Necesario para interactuar con la UI de Unity

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;  // Referencia al componente Text para mostrar la puntuaci�n
    private int score = 0;  // Puntuaci�n actual

    void Update()
    {
        // Actualiza el texto de la puntuaci�n en pantalla
        scoreText.text = "Puntuaci�n: " + score;
    }

    // M�todo para a�adir puntos (lo puedes llamar desde el script de PlayerInput)
    public void AddScore(int points)
    {
        score += points;
    }
}
