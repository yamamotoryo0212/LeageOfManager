using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class RequestSpectatorAPI : MonoBehaviour
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

            CurrentGameInfo response = JsonUtility.FromJson<CurrentGameInfo>(webRequest.downloadHandler.text);
            //Debug.Log(response.gameId);

            //LOM.Instance.LiveGameManager.RequestMatchIDURL = $"https://asia.api.riotgames.com/lol/match/v5/matches/by-puuid/{LOM.Instance.UserData.Puuid}/ids?start=0&count={LOM.Instance.LiveGameManager.MatchCount}&api_key={LOM.Instance.Mainsystem.DevelopmentAPIKey}";

            DelayMatch(response);

            //LOM.Instance.LiveGameManager.IsSpectatorRequest = true;
            yield return null;
        }
    }

    private async void DelayMatch(CurrentGameInfo currentGameInfo)
    {
        for (int i = 0; i < currentGameInfo.participants.Count; i++)
        {
            string str = $"https://jp1.api.riotgames.com/lol/summoner/v4/summoners/{currentGameInfo.participants[i].summonerId}?api_key={LOM.Instance.Mainsystem.DevelopmentAPIKey}";
            LOM.Instance.LiveGameManager.SetMatchMenberData(str, currentGameInfo.participants[i]);
            //Debug.Log(currentGameInfo.participants[i].summonerName + " : " + currentGameInfo.participants[i].championId);
            await Task.Delay(300);
        }
    }
}
