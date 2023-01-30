using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RequestSummonerAPI : MonoBehaviour
{
    public IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.LogError("Error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError("HTTP Error: " + webRequest.error);
                        break;
                    default:
                        break;
                }
                yield break;
            }

            SummonerDTO response = JsonUtility.FromJson<SummonerDTO>(webRequest.downloadHandler.text);
            Debug.Log(response.id);
            LOM.Instance.UserData.SetSummonerId(response.id);            

            yield return null;
        }
    }
}
