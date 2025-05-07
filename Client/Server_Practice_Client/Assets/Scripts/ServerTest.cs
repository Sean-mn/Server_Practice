using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.Collections;

public class ServerTest : MonoBehaviour
{
    private readonly string _serverUrl = "http://localhost:5015/api/hello";

    private void Start()
    {
        StartCoroutine(SendPlayerName("Sean"));
    }

    private IEnumerator SendPlayerName(string playerName)
    {
        var json = JsonUtility.ToJson(new PlayerRequest { PlayerName = playerName });
        var req = new UnityWebRequest(_serverUrl, "POST");
        byte[] body = Encoding.UTF8.GetBytes(json);

        req.uploadHandler = new UploadHandlerRaw(body);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"서버응답: {req.downloadHandler.text}");
        }
        else
        {
            Debug.Log($"요청실패: {req.error}");
        }
    }
}
