using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class KeybindManager : MonoBehaviour
{
    [System.Serializable]
    public class KeyBinding
    {
        public string actionName; // Nombre de la acción (Ej: "Jump")
        public string defaultKey; // Tecla por defecto (Ej: "Space")
        public Text keyText;      // Texto en la UI que muestra la tecla actual
        public Button changeKeyButton; // Botón para cambiar la tecla
    }

    public List<KeyBinding> keyBindings; // Lista de acciones configurables
    private string currentKeyToChange = null; // Para saber qué tecla se está cambiando

    void Start()
    {
        LoadKeyBindings();

        // Asignar eventos a los botones
        foreach (var binding in keyBindings)
        {
            binding.changeKeyButton.onClick.AddListener(() => StartKeyChange(binding));
        }
    }

    void Update()
    {
        if (currentKeyToChange != null && Input.anyKeyDown)
        {
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    SetKeyBinding(currentKeyToChange, key.ToString());
                    break;
                }
            }
        }
    }

    void StartKeyChange(KeyBinding binding)
    {
        currentKeyToChange = binding.actionName;
        binding.keyText.text = "Presiona una tecla...";
    }

    void SetKeyBinding(string action, string newKey)
    {
        PlayerPrefs.SetString(action, newKey);
        PlayerPrefs.Save();
        currentKeyToChange = null;
        LoadKeyBindings(); // Refrescar la UI
    }

    void LoadKeyBindings()
    {
        foreach (var binding in keyBindings)
        {
            string savedKey = PlayerPrefs.GetString(binding.actionName, binding.defaultKey);
            binding.keyText.text = savedKey;
        }
    }
}
