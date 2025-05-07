using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System;
using System.Collections;

namespace Managers.Network
{
    public class NetworkManager
    {
        private string _serverUrl = "http://localhost:5015/api/";
        private bool _isInitalized = false;

        public NetworkManager() { }
        public NetworkManager(string api)
        {
            _serverUrl += api;
            _isInitalized = true;
        }

        public IEnumerator PostJson<T>(T data, Action<string> onSuccess, Action<string> onFailed)
        {
            if (_isInitalized)
            {
                string json = JsonUtility.ToJson(data);
                Debug.Log($"º¸³¾ JSON: {json}");

                byte[] body = Encoding.UTF8.GetBytes(json);
                UnityWebRequest req = new UnityWebRequest(_serverUrl, "POST");

                req.uploadHandler = new UploadHandlerRaw(body);
                req.downloadHandler = new DownloadHandlerBuffer();
                req.SetRequestHeader("Content-Type", "application/json");

                yield return req.SendWebRequest();

                if (req.result == UnityWebRequest.Result.Success)
                {
                    var response = JsonUtility.FromJson<ServerResponse>(req.downloadHandler.text);
                    onSuccess?.Invoke(req.downloadHandler.text);
                }
                else
                {
                    onFailed?.Invoke(req.error);
                }
            }
        }
    }
}
