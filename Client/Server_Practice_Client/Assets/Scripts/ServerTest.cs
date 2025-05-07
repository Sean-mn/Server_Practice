using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.Collections;
using UnityEditor;

public class ServerTest : MonoBehaviour
{
    private readonly string _serverUrl = "http://localhost:5015/api/";
    private PlayerRequest _request;

    private void Start()
    {
        _request = new PlayerRequest();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SendPlayerName("Sean"));
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            _request.playerScore += 10;
            StartCoroutine(SendPlayerScore(_request.playerScore));
        }
    }

    private IEnumerator SendPlayerName(string currentPlayerName)
    {
        var json = JsonUtility.ToJson(new PlayerRequest { playerName = currentPlayerName });
        Debug.Log($"보낼 JSON: {json}");

        var req = new UnityWebRequest(_serverUrl + "hello", "POST");
        byte[] body = Encoding.UTF8.GetBytes(json);

        req.uploadHandler = new UploadHandlerRaw(body);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            var response = JsonUtility.FromJson<ServerResponse>(req.downloadHandler.text);
            Debug.Log($"서버응답: {response.message}");
        }
        else
        {
            Debug.Log($"요청실패: {req.error}");
        }
    }

    private IEnumerator SendPlayerScore(int currentPlayeScore)
    {
        var json = JsonUtility.ToJson(new PlayerRequest { playerScore = currentPlayeScore });
        Debug.Log($"보낼 JSON: {json}");

        var req = new UnityWebRequest(_serverUrl + "score", "POST");
        byte[] body = Encoding.UTF8.GetBytes(json.ToString());

        req.uploadHandler = new UploadHandlerRaw(body);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            var response = JsonUtility.FromJson<ServerResponse>(req.downloadHandler.text);
            Debug.Log($"서버 현재 점수: {response.message}");
        }
        else
        {
            Debug.Log($"요청 실패: {req.error}");
        }
    }
}
