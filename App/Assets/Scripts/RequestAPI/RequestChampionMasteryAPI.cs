using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RequestChampionMasteryAPI : MonoBehaviour
{
    public IEnumerator GetRequest(string uri, string puuid)
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
                        if (!(webRequest.error == "HTTP/1.1 503 Service Unavailable"))
                        {
                            LOM.Instance.LiveGameManager.ResetButton();
                        }
                        break;
                    default:
                        break;
                }
                yield break;
            }

            string str = webRequest.downloadHandler.text.Replace("[", "").Replace("]", "");
            ChampionMasteryDto response = JsonUtility.FromJson<ChampionMasteryDto>(str);

            foreach (var item in LOM.Instance.LiveGameManager.LiveGameMenberDatas)
            {
                if (item.Puuid == puuid)
                {
                    item.FavoriteChampion = response.championId;
                }
            }
            LOM.Instance.LiveGameManager.RequestCount++;
            yield return null;
        }
    }
}
