using TMPro;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public static ComboManager Instance;

    public int currentCombo = 0;
    public TextMeshProUGUI comboText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        comboText.text = $"Combo: {currentCombo}";
    }


    public void IncreaseCombo()
    {
        currentCombo++;
        Debug.Log("Combo actualizado: " + currentCombo);
        UpdateComboDisplay();
    }


    public void ResetCombo()
    {
        currentCombo = 0;
        UpdateComboDisplay();
    }

    private void UpdateComboDisplay()
    {
        if (comboText != null)
        {
            comboText.text = $"Combo: {currentCombo}";
        }
        else
        {
            Debug.LogWarning("comboText no asignado en ComboManager");
        }
    }

}
