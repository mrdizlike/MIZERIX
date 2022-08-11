using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;

public class Network : MonoBehaviourPunCallbacks, IPunObservable
{
    [Header("MainMenuPanels")]
    public GameObject ConnectStatePanel;
    public GameObject MenuPanel;
    public ProfileFeatures PF;
    public Text PlayerUsername;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    void Awake()
    {
        GetAccountInfo();
        GetAccountData();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected!");
        ConnectStatePanel.SetActive(false);
        MenuPanel.SetActive(true);
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void CreateMatch()
    {
        RoomOptions ro = new RoomOptions { MaxPlayers = 10, IsOpen = true, IsVisible = true };

        PhotonNetwork.JoinOrCreateRoom("NORMAL_5x5", ro, TypedLobby.Default);

        PhotonNetwork.LoadLevel(2);
    }

    private void GetAccountInfo()
    {
        var request = new GetAccountInfoRequest{
            
        };
        PlayFabClientAPI.GetAccountInfo(request, TakePlayFabUsername, OnError);
    }

    public void GetAccountData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataRecieved, OnError);
    }

    public void SaveAccountData()
    {
        var request = new UpdateUserDataRequest{
            Data = new Dictionary<string, string>{
                {"Avatar", PF.CurrentAvatarID.ToString()},
                {"NewRank", PF.CurrentRankID.ToString()},
                {"ProgressNewRank", PF.CurrentRankProgress.ToString()}
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    private void OnDataSend(UpdateUserDataResult result)
    {
        Debug.Log("Success Data send");
    }

    private void OnDataRecieved(GetUserDataResult result)
    {
        Debug.Log("Success Data get");
        if(result.Data != null && result.Data.ContainsKey("Avatar") && result.Data.ContainsKey("NewRank") && result.Data.ContainsKey("ProgressNewRank"))
        {
             PF.CurrentAvatarID =  int.Parse(result.Data["Avatar"].Value);
             PF.CurrentRankID = int.Parse(result.Data["NewRank"].Value);
             PF.CurrentRankProgress = float.Parse(result.Data["ProgressNewRank"].Value);
             PF.GetPlayerAvatar(int.Parse(result.Data["Avatar"].Value));
             PF.GetPlayerNewRank(int.Parse(result.Data["NewRank"].Value));
        } else
        {
            Debug.Log("Player data destructed!");
        }
    }

    private void TakePlayFabUsername(GetAccountInfoResult result)
    {
        PlayerUsername.text = result.AccountInfo.Username;
        PF.UserName.text = result.AccountInfo.Username;
    }

    private void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {

        }
        else if (stream.IsReading)
        {

        }
    }
}
