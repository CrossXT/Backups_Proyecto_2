using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyBindingManager : MonoBehaviour
{
    public TMP_Text[] keyLabels; // Asigna textos UI para mostrar la tecla actual
    private string[] actionKeys = new string[] { "key1", "key2", "key3", "key4" }; // Nombres internos

    private int waitingForKeyIndex = -1;

    void Start()
    {
        for (int i = 0; i < actionKeys.Length; i++)
        {
            string savedKey = PlayerPrefs.GetString(actionKeys[i], GetDefaultKey(i));
            keyLabels[i].text = savedKey.ToUpper();
        }
    }

    public void StartRebind(int index)
    {
        waitingForKeyIndex = index;
        keyLabels[index].text = "Presiona una tecla...";
    }

    void OnGUI()
    {
        if (waitingForKeyIndex != -1 && Event.current.type == EventType.KeyDown)
        {
            string newKey = Event.current.keyCode.ToString().ToLower();
            PlayerPrefs.SetString(actionKeys[waitingForKeyIndex], newKey);
            keyLabels[waitingForKeyIndex].text = newKey.ToUpper();
            waitingForKeyIndex = -1;
        }
    }

    private string GetDefaultKey(int index)
    {
        return index switch
        {
            0 => "d",
            1 => "f",
            2 => "j",
            3 => "k",
            _ => "none"
        };
    }

}
