using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pag_22 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI kcalAmount;
    [SerializeField] private TextMeshProUGUI proteintAmount;
    [SerializeField] private TextMeshProUGUI carbohydratesAmount;
    [SerializeField] private TextMeshProUGUI fatAmount;
    [SerializeField] private GameObject[] go;
    [SerializeField] private TextMeshProUGUI kcalHave;
    [SerializeField] private Button btn;

    private bool hasDisplayed = false;
    private int kcalNeeded = 2000;

    private void Start()
    {
        btn.onClick.AddListener(DisplayDailyNorms);

        if (PlayerPrefs.GetInt("Displayed", 0) == 1)
        {
            DisplayDailyNorms();
        }
    }

    private void DisplayDailyNorms()
    {
        kcalAmount.text = $"/ {kcalNeeded}";
        proteintAmount.text = "/ 100";
        carbohydratesAmount.text = "/ 250";
        fatAmount.text = "/ 70";
        hasDisplayed = true;
        PlayerPrefs.SetInt("Displayed", 1);
        PlayerPrefs.Save();
    }

    private void Update()
    {
        int currentKcal = int.TryParse(kcalHave.text, out int kcal) ? kcal : 0;
        int activeCount = Mathf.Clamp((currentKcal * go.Length) / kcalNeeded, 0, go.Length);

        for (int i = 0; i < go.Length; i++)
        {
            go[i].SetActive(i < activeCount);
        }
    }
}
