using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RequestLeageAPI : MonoBehaviour
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
                        break;
                    default:
                        break;
                }
                yield break;
            }
            if (webRequest.downloadHandler.text == "[]")
            {
                yield return null;
            }

            string pass = webRequest.downloadHandler.text.Replace("[", "{\"leagueEntryDTOs\":[").Replace("]", "]}");
            Test response = JsonUtility.FromJson<Test>(pass);

            //Debug.Log(response);
            for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
            {
                if (puuid == LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].Puuid)
                {
                    LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].LeagueEntryDTO = response;
                    //Debug.Log(LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].LeagueEntryDTO.leagueEntryDTOs.Count);
                }
            }
            LOM.Instance.LiveGameManager.RequestCount++;
            yield return null;
        }
    }
}
