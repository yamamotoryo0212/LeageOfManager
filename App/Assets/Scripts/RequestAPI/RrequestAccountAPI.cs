using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RrequestAccountAPI : MonoBehaviour
{
    private string _developmentAPIKey = "RGAPI-1612f1e7-09c6-40cc-a7f6-66d363fb663a";

    private string _tagLine = "JP1";
    private string _gameName = "yamamoto0212";

    private string _requestAccountURL = null;
    private void Awake()
    {
        _requestAccountURL = $"https://asia.api.riotgames.com/riot/account/v1/accounts/by-riot-id/{_gameName}/{_tagLine}?api_key={_developmentAPIKey}";
        StartCoroutine(GetRequest(_requestAccountURL));
    }

    private IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                // Error.
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
                        // ‚±‚±‚É‚Í‚±‚È‚¢.
                        break;
                }
                yield break;
            }

            AccountDto response = JsonUtility.FromJson<AccountDto>(webRequest.downloadHandler.text);
            Debug.Log(response.puuid);
            yield return null;
        }
    }
}
