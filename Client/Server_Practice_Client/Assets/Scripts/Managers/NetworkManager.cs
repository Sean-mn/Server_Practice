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

        public NetworkManager(string api = "")
        {
            if (!string.IsNullOrEmpty(api))
                _serverUrl += api;

            _isInitalized = true;
        }

        public IEnumerator PostJson<T>(T data, Action<string> onSuccess, Action<string> onFailed)
        {
            if (_isInitalized)
            {
                string json = JsonUtility.ToJson(data);
                Debug.Log($"보낼 JSON: {json}");

                byte[] body = Encoding.UTF8.GetBytes(json);
                UnityWebRequest req = new UnityWebRequest(_serverUrl, "POST");

                req.uploadHandler = new UploadHandlerRaw(body);
                req.downloadHandler = new DownloadHandlerBuffer();
                req.SetRequestHeader("Content-Type", "application/json");

                yield return req.SendWebRequest();

                if (req.result == UnityWebRequest.Result.Success)
                {
                    onSuccess?.Invoke(req.downloadHandler.text);
                }
                else
                {
                    onFailed?.Invoke($"요청 실패. 오류 코드: {req.responseCode}, 오류: {req.error}");
                }
            }
        }

        public IEnumerator GetJson<T>(Action<T> onSuccess, Action<string> onFailed)
        {
            var req = new UnityWebRequest(_serverUrl, "GET");
            req.downloadHandler = new DownloadHandlerBuffer();

            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
            {
                try
                {
                    T response = JsonUtility.FromJson<T>(req.downloadHandler.text);
                    onSuccess?.Invoke(response);
                }
                catch (Exception ex)
                {
                    onFailed?.Invoke($"예외 발생: {ex.Message}");
                }
            }
            else
            {
                onFailed?.Invoke($"요청 실패. 오류 코드: {req.responseCode}, 오류: {req.error}");
            }
        }
    }
}
