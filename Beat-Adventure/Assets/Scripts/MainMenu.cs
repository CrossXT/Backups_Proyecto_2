 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{

    public Button Start;
    public Button Settings;
    public Button Credits;
    public Button Exit;

    // Start is called before the first frame update


    private void OnEnable()
    {
        Start.onClick.AddListener(OnButtonStartClick);
        Settings.onClick.AddListener(OnButtonSettingsClick);
        Credits.onClick.AddListener(OnButtonSettingsClick);
        Exit.onClick.AddListener(OnButtonExitClick);

    }

    private void OnDisable()
    {
        Start.onClick.RemoveListener(OnButtonStartClick);
        Settings.onClick.RemoveListener(OnButtonStartClick);
        Credits.onClick.RemoveListener(OnButtonStartClick);
        Exit.onClick.RemoveListener(OnButtonStartClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnButtonStartClick()
    {
        SceneManager.LoadScene("LevelSelection");
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
