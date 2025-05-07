using UnityEngine;
using System.Collections;
using Managers.Network;

public class ServerTest : MonoBehaviour
{
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
        NetworkManager manager = new NetworkManager("hello");

        yield return manager.PostJson(
            new PlayerRequest { playerName = currentPlayerName },
            onSuccess: (response) =>
            {
                Debug.Log($"서버 응답: {response}");
            },
            onFailed: (error) =>
            {
                Debug.LogError($"요청 실패: {error}");
            }
        );
    }

    private IEnumerator SendPlayerScore(int currentPlayeScore)
    {
        NetworkManager manager = new NetworkManager("score");

        yield return manager.PostJson(
            new PlayerRequest { playerScore = currentPlayeScore },
            onSuccess: (response) =>
            {
                Debug.Log($"서버 응답: {response}");
            },
            onFailed: (error) =>
            {
                Debug.LogError($"요청 실패: {error}");
            }
            );
    }
}
