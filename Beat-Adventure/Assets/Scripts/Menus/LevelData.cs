using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class LevelData : MonoBehaviour
{
    int currentIndex = 0;
    public List<LevelInfo> levels; // ✅ Usamos LevelInfo en lugar de LevelData

    [System.Serializable]
    public class LevelInfo
    {
        public string nombreCancion;     // Nombre visible del nivel
        public string sceneName;         // Escena que se carga
        public Sprite portada;           // Imagen asociada
        public AudioClip previewAudio;   // Música de preview
        public string[] dificultades;    // Dificultades disponibles
    }

    string level;
    int dificultad;
    public GameObject panelDificultad;

    bool panelDificultadActivo = false;
    string dificultadActual = "Normal"; // Puedes cambiarlo según lo que seleccione el usuario

    void Start()
    {
        // Cargar preferencias
        level = PlayerPrefs.GetString("SelectedLevel");
        dificultad = PlayerPrefs.GetInt("SelectedDifficulty");

        // Desactiva panel de dificultad al inicio
        if (panelDificultad != null)
            panelDificultad.SetActive(false);
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
        Debug.Log("Nivel seleccionado: " + level.nombreCancion);

        // Aquí iría la actualización de UI: título, portada, etc.
        // Ej:
        // tituloTexto.text = level.nombreCancion;
        // imagenPortada.sprite = level.portada;
    }

    void HandleEnterKey()
    {
        if (!panelDificultadActivo)
        {
            // Activar el panel de dificultad
            panelDificultad.SetActive(true);
            panelDificultadActivo = true;
        }
        else
        {
            // Cargar nivel con dificultad
            string nombreEscena = levels[currentIndex].sceneName;
            PlayerPrefs.SetString("SelectedLevel", nombreEscena);
            PlayerPrefs.SetString("SelectedDifficulty", dificultadActual);
            Debug.Log("Cargando escena: " + nombreEscena + " con dificultad: " + dificultadActual);
            SceneManager.LoadScene(nombreEscena);
        }
    }
}
