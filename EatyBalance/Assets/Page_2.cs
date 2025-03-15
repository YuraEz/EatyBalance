using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Page_2 : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown age;
    [SerializeField] private TMP_Dropdown height;
    [SerializeField] private TMP_Dropdown weight;
    [SerializeField] private TextMeshProUGUI result;

    private void Start()
    {
        age.onValueChanged.AddListener(delegate { CalculateBodyFat(); });
        height.onValueChanged.AddListener(delegate { CalculateBodyFat(); });
        weight.onValueChanged.AddListener(delegate { CalculateBodyFat(); });
    }

    private void CalculateBodyFat()
    {
        int ageValue = GetDropdownValue(age);
        int heightValue = GetDropdownValue(height);
        int weightValue = GetDropdownValue(weight);

        // ѕримерна€ формула расчета жира на основе возраста, роста и веса
        float bodyFatPercentage = (weightValue * 0.3f) - (heightValue * 0.2f) + (ageValue * 0.5f);

        // ќграничение значений в адекватных пределах
        bodyFatPercentage = Mathf.Clamp(bodyFatPercentage, 5f, 40f);

        result.text = "Estimated Body Fat: " + bodyFatPercentage.ToString("F1") + "%";
    }

    private int GetDropdownValue(TMP_Dropdown dropdown)
    {
        string selectedText = dropdown.options[dropdown.value].text;
        string[] parts = selectedText.Split('-');

        if (parts.Length > 1)
        {
            if (int.TryParse(parts[1], out int value))
            {
                return value;
            }
        }
        return 0; // ¬озвращает 0, если не удалось определить число
    }
}
