using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class Network : MonoBehaviour
{
    public Text PlayerUsername;

    void Awake()
    {
        GetAccountInfo();
    }

    private void GetAccountInfo()
    {
        var request = new GetAccountInfoRequest{
            
        };
        PlayFabClientAPI.GetAccountInfo(request, TakePlayFabUsername, OnError);
    }

    private void TakePlayFabUsername(GetAccountInfoResult result)
    {
        PlayerUsername.text = result.AccountInfo.Username;
    }

    private void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
}
