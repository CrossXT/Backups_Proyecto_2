using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro; // ✅ Importa TextMeshPro

public class LevelData : MonoBehaviour
{
    int currentIndex = 0;  // Índice para recorrer los niveles
    public List<LevelInfo> levels;  // Lista de niveles

    [System.Serializable]
    public class LevelInfo
    {
        public string nombreCancion;     // Nombre visible del nivel
        public string sceneName;         // Escena que se carga
        public Sprite portada;           // Imagen asociada
        public AudioClip previewAudio;   // Música de previsualización
        public string[] dificultades;    // Dificultades disponibles
    }

    string level;
    int dificultad;
    public GameObject panelDificultad;

    bool panelDificultadActivo = false;
    string dificultadActual = "Normal";  // Dificultad inicial

    public TMP_Text tituloTexto;  // ✅ TextMeshPro en lugar de UnityEngine.UI.Text
    public UnityEngine.UI.Image imagenPortada;
    public AudioSource audioSource;

    void Start()
    {
        level = PlayerPrefs.GetString("SelectedLevel");
        dificultad = PlayerPrefs.GetInt("SelectedDifficulty");

        if (panelDificultad != null)
            panelDificultad.SetActive(false);

        UpdateLevelDisplay();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            MoveToNextLevel();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            MoveToPreviousLevel();

        if (Input.GetKeyDown(KeyCode.Return))
            HandleEnterKey();
    }

    void MoveToNextLevel()
    {
        currentIndex++;
        if (currentIndex >= levels.Count)
            currentIndex = 0;

        UpdateLevelDisplay();
    }

    void MoveToPreviousLevel()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = levels.Count - 1;

        UpdateLevelDisplay();
    }

    void UpdateLevelDisplay()
    {
        LevelInfo level = levels[currentIndex];
        tituloTexto.text = level.nombreCancion; 
        imagenPortada.sprite = level.portada;
        audioSource.clip = level.previewAudio;
        audioSource.Play();

        Debug.Log("Nivel seleccionado: " + level.nombreCancion);
    }

    void HandleEnterKey()
    {
        if (!panelDificultadActivo)
        {
            panelDificultad.SetActive(true);
            panelDificultadActivo = true;
        }
        else
        {
            string nombreEscena = levels[currentIndex].sceneName;
            PlayerPrefs.SetString("SelectedLevel", nombreEscena);
            PlayerPrefs.SetString("SelectedDifficulty", dificultadActual);
            Debug.Log("Cargando escena: " + nombreEscena + " con dificultad: " + dificultadActual);
            SceneManager.LoadScene(nombreEscena);
        }
    }
}
