using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RequestMatchAPI : MonoBehaviour
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

            MatchDto response = JsonUtility.FromJson<MatchDto>(webRequest.downloadHandler.text);
            for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
            {
                if (puuid == LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].Puuid)
                {
                    LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].MatchDtos.Add(response);
                    LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].IsMatchDataRequest = true;
                }
            }


            bool a = false;
            int index = 0;

            for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
            {
                if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].Puuid == puuid)
                {
                    index = i;
                    a = true;                    
                }
            }
           
            if (a)
            {
                for (int i = 0; i < response.info.participants.Count; i++)
                {
                    if (response.info.participants[i].puuid == puuid)
                    {
                        if (response.info.participants[i].win)
                        {
                            LOM.Instance.LiveGameManager.LiveGameMenberDatas[index].WinCount++;
                        }                        
                    }
                }
            }
          
            yield return null;
        }
    }
}
