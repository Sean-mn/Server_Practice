using Managers.Network;
using System.Collections;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private NetworkManager manager;

    private void Start()
    {
        manager = new NetworkManager("score");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(manager.GetJson<ServerResponse>(OnReceiveSuccess, OnRequestFailed));
        }
    }

    private void OnReceiveSuccess(ServerResponse response)
    {
        if (response != null)
        {
            Debug.Log($"서버 응답: {response.message}, 누적 점수: {response.totalScore}");
        }
        else
        {
            Debug.LogError("응답이 비어 있거나 잘못된 형식입니다.");
        }
    }

    private void OnRequestFailed(string errorMessage)
    {
        Debug.LogError($"요청 실패: {errorMessage}");
    }
}
