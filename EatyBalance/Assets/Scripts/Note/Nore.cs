using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Nore : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown wellb;
    [SerializeField] private TMP_InputField dish;
    [SerializeField] private TMP_InputField location;

    [SerializeField] private Button add;

    private void Start()
    {
        // Ограничиваем ввод только цифрами
       // dish.contentType = TMP_InputField.ContentType.IntegerNumber;
        location.contentType = TMP_InputField.ContentType.IntegerNumber;

        // Добавляем проверку при изменении текста
       // dish.onValidateInput += ValidateInput;
        location.onValidateInput += ValidateInput;

        add.onClick.AddListener(() =>
        {
            int noteIndex = PlayerPrefs.GetInt("noteId", 0);
            PlayerPrefs.SetString($"wellb{noteIndex}", wellb.options[wellb.value].text);
            PlayerPrefs.SetString($"dish{noteIndex}", dish.text);
            PlayerPrefs.SetString($"location{noteIndex}", location.text);

            PlayerPrefs.SetInt("noteId", noteIndex + 1);

            CVManager.Instance.ChangeScreen("s_1_1");
            CVManager.Instance.ChangeScreen("s_2_1");
            CVManager.Instance.ChangeScreen("s_4_1");
        });
    }

    private char ValidateInput(string text, int charIndex, char addedChar)
    {
        return char.IsDigit(addedChar) ? addedChar : '\0';
    }
}
