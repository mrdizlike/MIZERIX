using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;

public class Network : MonoBehaviourPunCallbacks, IPunObservable
{
    [Header("MainMenuPanels")]
    public GameObject ConnectStatePanel;
    public GameObject MenuPanel;
    public Text FriendSearch;
    public GameObject FriendPrefab;
    public ProfileFeatures PF;
    public Text PlayerUsername;

    [SerializeField]
    Transform FriendScrollView;
    List<PlayFab.ClientModels.FriendInfo> _friends;
    List<PlayFab.ClientModels.FriendInfo> myFriends;
    public enum FriendIdType { PlayFabId, Username, Email, DisplayName};

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    void Awake()
    {
        GetAccountInfo(); //Получаем PlayFab информацию об аккаунте игрока
        GetAccountData(); //Получаем PlayFab кастомные данные игрока
        GetFriends(); //Получаем PLayFab друзей из списка друзей игрока
    }

    public override void OnConnectedToMaster() //Присоединяемся к серверам Photon
    {
        Debug.Log("Connected!");
        ConnectStatePanel.SetActive(false);
        MenuPanel.SetActive(true);
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void CreateMatch() //Создаем игру
    {
        RoomOptions ro = new RoomOptions { MaxPlayers = 10, IsOpen = true, IsVisible = true }; //Настройки комнаты

        PhotonNetwork.JoinOrCreateRoom("NORMAL_5x5", ro, TypedLobby.Default); //Либо создаем, либо присоединяемся к существующей комнате

        PhotonNetwork.LoadLevel(2); //Загружаем карту
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
             PF.GetPlayerAvatar(PF.ProfileUserAvatar, int.Parse(result.Data["Avatar"].Value));
             PF.GetPlayerNewRank(int.Parse(result.Data["NewRank"].Value));
        } else //Если игрок создал аккаунт, то его данные сохраняются под нулевым значением для дальнейшей работы.
        {
            SaveAccountData();
        }
    }

    private void TakePlayFabUsername(GetAccountInfoResult result) //Получаем никнейм игрока
    {
        PlayerUsername.text = result.AccountInfo.Username;
        PF.UserName.GetComponent<InputField>().text = result.AccountInfo.Username;
        PF.PlayerID.GetComponent<InputField>().text = "(ID: " + result.AccountInfo.PlayFabId + ")";
    }
    
    #region FriendsSys
    public void GetFriends() //Запрашиваем лист с друзьями игрока
    {
        PlayFabClientAPI.GetFriendsList(new GetFriendsListRequest
        {
            IncludeFacebookFriends = false,
            IncludeSteamFriends = false
        }, result => {
            _friends = result.Friends;
            DisplayFriends(_friends);
        }, OnError);
    }

    void DisplayFriends(List<PlayFab.ClientModels.FriendInfo> friendsCache) //Отображаем полученные данные
    {
        foreach(PlayFab.ClientModels.FriendInfo f in friendsCache)
        {
            bool isFound = false;
            if(myFriends != null)
            {
                foreach(PlayFab.ClientModels.FriendInfo g in myFriends)
                {
                    if(f.FriendPlayFabId == g.FriendPlayFabId)
                    {
                        isFound = true;
                    }
                }
            }

            if(!isFound)
            {

                GameObject listing = Instantiate(FriendPrefab, FriendScrollView);
                listing.GetComponent<ListingPrefab>().NT = GetComponent<Network>();
                listing.GetComponent<ListingPrefab>().PF = PF;
                listing.GetComponent<ListingPrefab>().PlayerUsername.text = f.Username;
                listing.GetComponent<ListingPrefab>().PlayerID = f.FriendPlayFabId;
                PlayFabClientAPI.GetUserData(new GetUserDataRequest
                {PlayFabId = f.FriendPlayFabId}, listing.GetComponent<ListingPrefab>().OnFriendDataRecieved, OnError);
            }        
        }

        myFriends = friendsCache;
    }

    IEnumerator WaitForFriend()
    {
        yield return new WaitForSeconds(2);
        GetFriends();
    }

    public void RunWaitFunction()
    {
        StartCoroutine(WaitForFriend());
    }

    void AddFriend(FriendIdType idType, string friendId)
    {
        var request = new AddFriendRequest();
        switch(idType)
        {
            case FriendIdType.PlayFabId:
                request.FriendPlayFabId = friendId;
                break;
            case FriendIdType.Username:
                request.FriendUsername = friendId;
                break;
            case FriendIdType.Email:
                request.FriendEmail = friendId;
                break;
            case FriendIdType.DisplayName:
                request.FriendTitleDisplayName = friendId;
                break;
        }

        PlayFabClientAPI.AddFriend(request, result => {
            Debug.Log("Friend added");
        }, OnError);
    }

    public void RemoveFriend(string friendId)
    {
        var request = new RemoveFriendRequest();
        request.FriendPlayFabId = friendId;

        PlayFabClientAPI.RemoveFriend(request, result => {
            Debug.Log("Friend deleted!");
        }, OnError);
    }

    public void InputFriendId(string IdIn)
    {
        FriendSearch.text = IdIn;
    }
    #endregion

    public void SubmitFriendRequest()
    {
        AddFriend(FriendIdType.PlayFabId, FriendSearch.text);
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
