using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AII : MonoBehaviour
{
    [SerializeField] private TMP_InputField userInput;  // Поле для ввода вопроса
    [SerializeField] private TextMeshProUGUI responseText;  // Текст для вывода ответа ИИ
    [SerializeField] private Button sendButton;  // Кнопка для отправки вопроса

    private string apiUrl = "https://api-inference.huggingface.co/models/microsoft/DialoGPT-medium";  // Модель ChatBot Hugging Face

    private void Start()
    {
        // Обработчик клика по кнопке
        sendButton.onClick.AddListener(() => StartCoroutine(SendRequest(userInput.text)));
    }

    private IEnumerator SendRequest(string prompt)
    {
        string json = "{\"inputs\": \"" + prompt + "\"}";

        using (UnityWebRequest request = new UnityWebRequest(apiUrl, "POST"))
        {
            // Подготовка запроса
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // Ожидаем завершения запроса
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string result = request.downloadHandler.text;
                string response = ParseResponse(result);
                responseText.text = response;
            }
            else
            {
                responseText.text = "Error: " + request.error;
            }
        }
    }

    private string ParseResponse(string jsonResponse)
    {
        // Извлекаем ответ из JSON (можно настроить под структуру ответа модели)
        string response = jsonResponse.Substring(jsonResponse.IndexOf("\"generated_text\":") + 18);
        response = response.Substring(0, response.IndexOf("\""));

        return response;
    }
}
