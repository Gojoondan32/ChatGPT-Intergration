using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ChatGPT_Handler : MonoBehaviour
{
    private const string openAIUrl = "https://api.openai.com/v1/engines/text-davinci-003/completions";
    private const string openAIKey = "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";


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateText("what are the words to happy birthday"));
    }

    public IEnumerator GenerateText(string prompt)
    {
        //string jsonRequestBody = "{\"prompt\": \"" + prompt + "\", \"max_tokens\": 50}";
        string jsonRequestBody = JsonUtility.ToJson(prompt);

        using (UnityWebRequest request = UnityWebRequest.Post(openAIUrl, jsonRequestBody))
        {
            //byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);

            var data = Encoding.UTF8.GetBytes(jsonRequestBody);

            request.uploadHandler = new UploadHandlerRaw(data);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + openAIKey);

            Debug.Log("Running");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("Error: " + request.error);
                yield break;
            }
            else
            {
                Debug.Log("Received: " + request.downloadHandler.text);
                // Process the received text here
            }
        }
    }

    private void ProcessText(string data){

    }
}
