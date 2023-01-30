using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RequestAccountAPI : MonoBehaviour
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

            AccountDto response = JsonUtility.FromJson<AccountDto>(webRequest.downloadHandler.text);
            Debug.Log(response.puuid);

            LOM.Instance.UserData.SetPuuid(response.puuid);
            LOM.Instance.LiveGameManager.RequestSummonerURL = $"https://jp1.api.riotgames.com/lol/summoner/v4/summoners/by-puuid/{LOM.Instance.UserData.Puuid}?api_key={LOM.Instance.Mainsystem.DevelopmentAPIKey}";

            LOM.Instance.LiveGameManager.IsAccountRequest = true;

            yield return null;
        }
    }
}
