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
            Debug.Log($"���� ����: {response.message}, ���� ����: {response.totalScore}");
        }
        else
        {
            Debug.LogError("������ ��� �ְų� �߸��� �����Դϴ�.");
        }
    }

    private void OnRequestFailed(string errorMessage)
    {
        Debug.LogError($"��û ����: {errorMessage}");
    }
}
