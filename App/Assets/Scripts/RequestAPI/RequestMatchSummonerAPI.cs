using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RequestMatchSummonerAPI : MonoBehaviour
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

            SummonerDTO_MatchMember response = JsonUtility.FromJson<SummonerDTO_MatchMember>(webRequest.downloadHandler.text);

            LiveGameMenberData liveGameMenberData = new LiveGameMenberData();
            liveGameMenberData.Puuid = response.puuid;
            liveGameMenberData.SummonerID = response.id;
            liveGameMenberData.SummonerName = response.name;
            LOM.Instance.LiveGameManager.LiveGameMenberDatas.Add(liveGameMenberData);
            Debug.Log(LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count);
            yield return null;
        }
    }
}
