using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Page_1 : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dishDropdown;
    [SerializeField] private TMP_Dropdown fruitDropdown;
    [SerializeField] private TMP_Dropdown vegetableDropdown;
    [SerializeField] private TMP_Dropdown saladDropdown;
    [SerializeField] private Button add;

    [Space]
    [SerializeField] private TextMeshProUGUI kcalAmount;
    [SerializeField] private TextMeshProUGUI proteinAmount;
    [SerializeField] private TextMeshProUGUI carbohydratesAmount;
    [SerializeField] private TextMeshProUGUI fatAmount;

    private const string DateKey = "LastSavedDate";
    private const string KcalKey = "SavedKcal";
    private const string ProteinKey = "SavedProtein";
    private const string CarbsKey = "SavedCarbs";
    private const string FatsKey = "SavedFats";

    private Dictionary<string, (float kcal, float protein, float carbs, float fats)> dishes = new Dictionary<string, (float, float, float, float)>
    {
        { "Omelet", (154, 11, 1, 11) },
        { "Porridge", (150, 5, 30, 2) },
        { "Soup", (100, 6, 10, 4) },
        { "Pasta", (250, 8, 40, 5) },
        { "Chicken", (165, 31, 0, 3) },
        { "Fish", (200, 20, 0, 10) },
        { "Steak", (250, 30, 0, 15) },
        { "Rice", (130, 3, 28, 0) },
        { "Sandwich", (300, 12, 35, 10) }
    };

    private Dictionary<string, (float kcal, float protein, float carbs, float fats)> fruits = new Dictionary<string, (float, float, float, float)>
    {
        { "Apple", (52, 0, 14, 0) },
        { "Banana", (89, 1, 23, 0) },
        { "Orange", (43, 1, 10, 0) },
        { "Grapes", (70, 1, 18, 0) },
        { "Strawberry", (32, 1, 7, 0) },
        { "Watermelon", (30, 1, 8, 0) },
        { "Pear", (57, 0, 15, 0) },
        { "Peach", (39, 1, 10, 0) }
    };

    private Dictionary<string, (float kcal, float protein, float carbs, float fats)> vegetables = new Dictionary<string, (float, float, float, float)>
    {
        { "Carrot", (35, 1, 8, 0) },
        { "Potato", (77, 2, 17, 0) },
        { "Cucumber", (16, 1, 3, 0) },
        { "Tomato", (18, 1, 4, 0) },
        { "Onion", (40, 1, 9, 0) },
        { "Pepper", (31, 1, 6, 0) },
        { "Cabbage", (25, 1, 6, 0) },
        { "Broccoli", (55, 4, 11, 1) }
    };

    private Dictionary<string, (float kcal, float protein, float carbs, float fats)> salads = new Dictionary<string, (float, float, float, float)>
    {
        { "Greek Salad", (120, 4, 6, 9) },
        { "Vegetable Salad", (80, 2, 10, 4) },
        { "Caesar", (180, 12, 8, 12) },
        { "Olivier", (250, 8, 15, 18) },
        { "Coleslaw", (150, 2, 12, 10) },
        { "Seaweed Salad", (90, 5, 8, 4) }
    };

    private void Start()
    {
        CheckAndResetData();
        LoadData();
        add.onClick.AddListener(AddValues);
    }

    private void CheckAndResetData()
    {
        string lastSavedDate = PlayerPrefs.GetString(DateKey, "");
        string today = DateTime.Now.ToString("yyyy-MM-dd");

        if (lastSavedDate != today)
        {
            PlayerPrefs.SetString(DateKey, today);
            PlayerPrefs.SetFloat(KcalKey, 0);
            PlayerPrefs.SetFloat(ProteinKey, 0);
            PlayerPrefs.SetFloat(CarbsKey, 0);
            PlayerPrefs.SetFloat(FatsKey, 0);
            PlayerPrefs.Save();
        }
    }

    private void LoadData()
    {
        kcalAmount.text = PlayerPrefs.GetFloat(KcalKey, 0).ToString();
        proteinAmount.text = PlayerPrefs.GetFloat(ProteinKey, 0).ToString();
        carbohydratesAmount.text = PlayerPrefs.GetFloat(CarbsKey, 0).ToString();
        fatAmount.text = PlayerPrefs.GetFloat(FatsKey, 0).ToString();
    }

    private void AddValues()
    {
        float kcal = 0, protein = 0, carbs = 0, fats = 0;

        kcal += GetFoodValues(dishDropdown, dishes).kcal;
        protein += GetFoodValues(dishDropdown, dishes).protein;
        carbs += GetFoodValues(dishDropdown, dishes).carbs;
        fats += GetFoodValues(dishDropdown, dishes).fats;

        kcal += GetFoodValues(fruitDropdown, fruits).kcal;
        protein += GetFoodValues(fruitDropdown, fruits).protein;
        carbs += GetFoodValues(fruitDropdown, fruits).carbs;
        fats += GetFoodValues(fruitDropdown, fruits).fats;

        kcal += GetFoodValues(vegetableDropdown, vegetables).kcal;
        protein += GetFoodValues(vegetableDropdown, vegetables).protein;
        carbs += GetFoodValues(vegetableDropdown, vegetables).carbs;
        fats += GetFoodValues(vegetableDropdown, vegetables).fats;

        kcal += GetFoodValues(saladDropdown, salads).kcal;
        protein += GetFoodValues(saladDropdown, salads).protein;
        carbs += GetFoodValues(saladDropdown, salads).carbs;
        fats += GetFoodValues(saladDropdown, salads).fats;

        kcalAmount.text = kcal.ToString();
        proteinAmount.text = protein.ToString();
        carbohydratesAmount.text = carbs.ToString();
        fatAmount.text = fats.ToString();

        dishDropdown.value = 0;
        dishDropdown.RefreshShownValue();

        fruitDropdown.value = 0;
        fruitDropdown.RefreshShownValue();

        vegetableDropdown.value = 0;
        vegetableDropdown.RefreshShownValue();

        saladDropdown.value = 0;
        saladDropdown.RefreshShownValue();
    }

    private (float kcal, float protein, float carbs, float fats) GetFoodValues(TMP_Dropdown dropdown, Dictionary<string, (float, float, float, float)> data)
    {
        return data.ContainsKey(dropdown.options[dropdown.value].text) ? data[dropdown.options[dropdown.value].text] : (0, 0, 0, 0);
    }
}
