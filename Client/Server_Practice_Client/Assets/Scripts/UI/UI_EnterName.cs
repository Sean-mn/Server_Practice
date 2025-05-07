using Managers.Network;
using UnityEngine;
using UnityEngine.UI;

public class UI_EnterName : MonoBehaviour
{
    [SerializeField]
    private InputField _nameInput;

    private void Start()
    {
        if (_nameInput != null)
            _nameInput.characterLimit = 5;
    }

    public void EnterName()
    {
        NetworkManager manager = new NetworkManager("hello");
        string currentPlayerName = _nameInput.text;

        StartCoroutine(manager.PostJson(
            new PlayerRequest { playerName = currentPlayerName },
            onSuccess: (response) =>
            {
                Debug.Log($"���� ����: {response}");
            },
            onFailed: (error) =>
            {
                Debug.LogError($"��û ����: {error}");
            }
        ));
    }
}
