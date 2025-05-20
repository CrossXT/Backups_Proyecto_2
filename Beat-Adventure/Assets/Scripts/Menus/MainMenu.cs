using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Button Start;
    public Button HowToPlay;
    public Button Settings;
    public Button Credits;
    public Button Exit;

    // Start is called before the first frame update


    private void OnEnable()
    {
        Start.onClick.AddListener(OnButtonStartClick);
        HowToPlay.onClick.AddListener(OnButtonHowToPlayClick);
        Settings.onClick.AddListener(OnButtonSettingsClick);
        Credits.onClick.AddListener(OnButtonCreditsClick);
        Exit.onClick.AddListener(OnButtonExitClick);

    }

    private void OnDisable()
    {
        Start.onClick.RemoveListener(OnButtonStartClick);
        HowToPlay.onClick.RemoveListener(OnButtonHowToPlayClick);
        Settings.onClick.RemoveListener(OnButtonSettingsClick);
        Credits.onClick.RemoveListener(OnButtonCreditsClick);
        Exit.onClick.RemoveListener(OnButtonExitClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnButtonStartClick()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    void OnButtonHowToPlayClick()
    {
        SceneManager.LoadScene("HowToPlay");
    }
    void OnButtonSettingsClick()
    {
        SceneManager.LoadScene("Settings");
    }
    void OnButtonCreditsClick()
    {
        SceneManager.LoadScene("Credits");
    }
    void OnButtonExitClick()
    {
        #if UNITY_EDITOR
        
        EditorApplication.isPlaying = false;
        #endif

        Application.Quit();

    }
}
