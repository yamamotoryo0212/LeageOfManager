using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RequestMatchIDAPI : MonoBehaviour
{
    public IEnumerator GetRequest(string uri,string puuid)
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

            string str = webRequest.downloadHandler.text.Replace("[", "{\"matchIDs\":[").Replace("]", "]}");
            MatchIDDTO response = JsonUtility.FromJson<MatchIDDTO>(str);

            for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
            {
                if (puuid == LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].Puuid)
                {
                    LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].MatchIDs = response.matchIDs;
                    //Debug.Log(LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerName + " : " + LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].MatchIDs.Count);
                }
            }

            LOM.Instance.LiveGameManager.IsMatchIDRequest = true;
            Debug.Log("ƒ}ƒbƒ`IDŽæ“¾Š®—¹");
          
            yield return null;
        }
    }
}
