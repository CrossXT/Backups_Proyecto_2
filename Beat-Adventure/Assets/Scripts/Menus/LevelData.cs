using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;


public class LevelData : MonoBehaviour
{
    int currentIndex = 0;
    int currentDificultadIndex = 0;

    public List<LevelInfo> levels;

    [System.Serializable]
    public class LevelInfo
    {
        public string nombreCancion;
        public string sceneName;
        public Sprite portada;
        public AudioClip previewAudio;
        public string[] dificultades;
    }

    public GameObject panelDificultad;
    public TMP_Text tituloTexto;
    public TMP_Text textoDificultad; 
    public UnityEngine.UI.Image imagenPortada;
    public AudioSource audioSource;

    bool panelDificultadActivo = false;

    void Start()
    {
        if (panelDificultad != null)
            panelDificultad.SetActive(false);

        UpdateLevelDisplay();
    }

    void Update()
    {
        if (!panelDificultadActivo)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                currentIndex = (currentIndex + 1) % levels.Count;
                UpdateLevelDisplay();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentIndex = (currentIndex - 1 + levels.Count) % levels.Count;
                UpdateLevelDisplay();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                panelDificultadActivo = true;
                panelDificultad.SetActive(true);
                currentDificultadIndex = 0; // Reseteamos dificultad
                UpdateDificultadDisplay();
            }
        }
        else // Si está activo el panel de dificultad
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                currentDificultadIndex = (currentDificultadIndex + 1) % levels[currentIndex].dificultades.Length;
                UpdateDificultadDisplay();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentDificultadIndex = (currentDificultadIndex - 1 + levels[currentIndex].dificultades.Length) % levels[currentIndex].dificultades.Length;
                UpdateDificultadDisplay();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                string nombreEscena = levels[currentIndex].sceneName;
                string dificultadSeleccionada = levels[currentIndex].dificultades[currentDificultadIndex];

                PlayerPrefs.SetString("SelectedLevel", nombreEscena);
                PlayerPrefs.SetString("SelectedDifficulty", dificultadSeleccionada);

                Debug.Log("Cargando escena: " + nombreEscena + " con dificultad: " + dificultadSeleccionada);

                PlayerPrefs.SetString("SceneToLoad", nombreEscena);
                SceneManager.LoadScene("Loading");

            }
        }
    }

    void UpdateLevelDisplay()
    {
        LevelInfo nivel = levels[currentIndex];
        tituloTexto.text = nivel.nombreCancion;
        imagenPortada.sprite = nivel.portada;

        if (audioSource != null && nivel.previewAudio != null)
        {
            audioSource.clip = nivel.previewAudio;
            audioSource.Play();
        }

        Debug.Log("Nivel seleccionado: " + nivel.nombreCancion);
    }

    void UpdateDificultadDisplay()
    {
        string dif = levels[currentIndex].dificultades[currentDificultadIndex];
        textoDificultad.text = "Dificultad: " + dif;
        Debug.Log("Dificultad seleccionada: " + dif);
    }
}
