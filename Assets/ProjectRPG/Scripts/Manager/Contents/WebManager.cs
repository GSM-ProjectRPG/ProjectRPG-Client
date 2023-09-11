using System;
using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace ProjectRPG
{
    public class WebManager
    {
        public string BaseUrl { get; set; } = "https://localhost:5001/api";

        public void SendPostRequest<T>(string url, object obj, Action<T> res)
        {
            Managers.Instance.StartCoroutine(CoSendWebRequest(url, UnityWebRequest.kHttpVerbPOST, obj, res));
        }

        private IEnumerator CoSendWebRequest<T>(string url, string method, object obj, Action<T> res)
        {
            string sendUrl = $"{BaseUrl}/{url}";

            byte[] jsonBytes = null;
            if (obj != null)
            {
                string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                jsonBytes = Encoding.UTF8.GetBytes(jsonStr);
            }

            using (var uwr = new UnityWebRequest(sendUrl, method))
            {
                uwr.uploadHandler = new UploadHandlerRaw(jsonBytes);
                uwr.downloadHandler = new DownloadHandlerBuffer();
                uwr.SetRequestHeader("Content-Type", "application/json");
                uwr.certificateHandler = new CertificateWhore();

                yield return uwr.SendWebRequest();

                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    Debug.Log(uwr.error);
                }
                else
                {
                    T resObj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(uwr.downloadHandler.text);
                    res.Invoke(resObj);
                }
            }
        }
    }
}