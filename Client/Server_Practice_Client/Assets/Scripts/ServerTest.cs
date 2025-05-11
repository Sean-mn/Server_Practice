using UnityEngine;
using System.Collections;
using Managers.Network;
using UnityEngine.Networking;

public class ServerTest : MonoBehaviour
{
    private PlayerRequest _request;

    private void Start()
    {
        StartCoroutine(GetPlayerData(1));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _request = new(1, "Sean");

            StartCoroutine(SendPlayerName(_request));
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            _request.playerScore += 10;
            StartCoroutine(SendPlayerScore(_request.playerScore));
        }
    }

    private IEnumerator GetPlayerData(int id)
    {
        string url = $"http://localhost:5015/api/player/{id}";
        UnityWebRequest req = UnityWebRequest.Get(url);

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("플레이어 정보: " + req.downloadHandler.text);
        }
        else
        {
            Debug.LogError("에러: " + req.error);
        }
    }

    private IEnumerator SendPlayerName(PlayerRequest request)
    {
        NetworkManager manager = new NetworkManager("player/register");

        yield return manager.PostJson(
            request,
            onSuccess: (response) =>
            {
                Debug.Log($"서버 응답: {response}");
            },
            onFailed: (error) =>
            {
                if (error.Contains("409"))
                {
                    Debug.LogWarning($"이미 등록된 ID입니다: {error}");
                }
                else
                {
                    Debug.LogError($"요청 실패: {error}");
                }
            }
        );
    }

    private IEnumerator SendPlayerScore(int currentPlayeScore)
    {
        NetworkManager manager = new NetworkManager("score");

        yield return manager.PostJson(
            _request,
            onSuccess: (response) =>
            {
                Debug.Log($"서버 응답: {response}");
            },
            onFailed: (error) =>
            {
                Debug.LogWarning($"요청 실패: {error}");
            }
        );
    }
}
