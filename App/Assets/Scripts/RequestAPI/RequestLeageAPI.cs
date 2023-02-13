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

            string str = webRequest.downloadHandler.text.Replace("[","").Replace("]","");

            LeagueEntryDTO response = JsonUtility.FromJson<LeagueEntryDTO>(str);
            //Debug.Log(response);
            for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
            {
                if (puuid == LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].Puuid)
                {
                    LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].LeagueEntryDTO = response;
                    Debug.Log(LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].LeagueEntryDTO.tier);
                }
            }

            yield return null;
        }
    }
}
