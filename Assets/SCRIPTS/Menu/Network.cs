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
    public PlayerFabStat PFS;
    public Text PlayerUsername;

    [Header("SearchGameMisc")]
    public bool IsSearching;
    public float Seconds;
    public float Minutes;
    public float ReadyTimer;
    public int AcceptCount;

    public Text SearchTimerText;
    public Image TimeToAccept;
    
    public GameObject GameReadyPanel;
    public GameObject GameSearchButton;
    public GameObject GameSearchCancelButton;
    public GameObject GameAcceptButton;
    public GameObject[] PlayersReady;

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

    void Update()
    {
        if(IsSearching)
        {
            SearchTimerText.text = string.Format("{0:00}:{1:00}", Minutes, Seconds);
            Seconds += Time.deltaTime;

            if(Seconds >= 59)
            {
                Seconds = 0;
                Minutes += 1;
            }
        }

        if(GameReadyPanel.activeSelf)
        {
            ReadyTimer += Time.deltaTime;

            TimeToAccept.fillAmount -= 1 / 10f * Time.deltaTime;

            if(ReadyTimer >= 10)
            {
                foreach(GameObject PlayerIcon in PlayersReady)
                {
                  if(PlayerIcon.activeSelf)
                  {
                        PlayerIcon.SetActive(false);
                  }
                }

                Seconds = 0;
                Minutes = 0;
                ReadyTimer = 0;
                AcceptCount = 0;
                IsSearching = false;
                TimeToAccept.fillAmount = 1;
                GameSearchCancelButton.SetActive(false);
                GameSearchButton.SetActive(true);
                GameAcceptButton.GetComponent<Button>().interactable = true;
                GameSearchCancelButton.GetComponent<Button>().interactable = true;
                PhotonNetwork.LeaveRoom();
                Debug.Log("Leave from room");
                GameReadyPanel.SetActive(false);
            }

            if(PhotonNetwork.InRoom && photonView.IsMine)
            {
                  if(AcceptCount == 2 && IsSearching) //Все игроки приняли матч, начинается загрузка карты
                  {
                       IsSearching = false;
                       photonView.RPC("LoadMap", RpcTarget.All);
                  }
            }
        }
    }

    public void IsSearchingButton(bool search)
    {
        Seconds = 0;
        Minutes = 0;
        IsSearching = search;
    }

    #region ConnectionAndSearch
    public override void OnConnectedToMaster() //Присоединяемся к серверам Photon
    {
        Debug.Log("Connected!");
        ConnectStatePanel.SetActive(false);
        MenuPanel.SetActive(true);
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void CreateMatch() //Создаем игру
    {
        RoomOptions ro = new RoomOptions { 
            MaxPlayers = 10,
            IsOpen = true,
            IsVisible = true
            }; //Настройки комнаты

        PhotonNetwork.JoinOrCreateRoom("NORMAL_5x5", ro, TypedLobby.Default); //Либо создаем, либо присоединяемся к существующей комнате
        Debug.Log("Room has been created");
    }

    public void StopSearchMatch()
    {
        PhotonNetwork.LeaveRoom();
        Debug.Log("Search stopped");
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient) //Ждем соглашения всех игроков и запускаем карту
        {
            Debug.Log("All players in room! Waiting accepting");
            photonView.RPC("EnableReadyPanel", RpcTarget.All);
        }
    }

    public void AcceptMatch()
    {
        photonView.RPC("AcceptMatchRPC", RpcTarget.All);
    }

    [PunRPC]
    void LoadMap()
    {
        PhotonNetwork.LoadLevel(2);
    }

    [PunRPC]
    void EnableReadyPanel()
    {
        GameReadyPanel.SetActive(true);
        GameSearchCancelButton.GetComponent<Button>().interactable = false;
    }

    [PunRPC]
    void AcceptMatchRPC()
    {
        AcceptCount += 1;
        
        foreach(GameObject PlayerIcon in PlayersReady)
        {
            if(!PlayerIcon.activeSelf)
            {
                PlayerIcon.SetActive(true);
                break;
            }
        }
    }
    #endregion

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
            Permission = UserDataPermission.Public,
            Data = new Dictionary<string, string>{
                {"Avatar", PF.CurrentAvatarID.ToString()},
                {"NewRank", PF.CurrentRankID.ToString()},
                {"ProgressNewRank", PF.CurrentRankProgress.ToString()},
                {"WinStat", PFS.WinStat.ToString()},
                {"DefeatStat", PFS.DefeatStat.ToString()},
                {"KillsStat", PFS.KillStat.ToString()},
                {"DeathStat", PFS.DeathStat.ToString()},
                {"ItemsStat", PFS.ItemsStat.ToString()},
                {"StatBlockOneID", PFS.StatBlockOneID.ToString()},
                {"StatBlockTwoID", PFS.StatBlockTwoID.ToString()}
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
             PFS.WinStat = int.Parse(result.Data["WinStat"].Value);
             PFS.DefeatStat = int.Parse(result.Data["DefeatStat"].Value);
             PFS.KillStat = int.Parse(result.Data["KillsStat"].Value);
             PFS.DeathStat = int.Parse(result.Data["DeathStat"].Value);
             PFS.ItemsStat = int.Parse(result.Data["ItemsStat"].Value);
             PFS.StatBlockOneID = int.Parse(result.Data["StatBlockOneID"].Value);
             PFS.StatBlockTwoID = int.Parse(result.Data["StatBlockTwoID"].Value);
             PFS.ChangeStatBlockOne(int.Parse(result.Data["StatBlockOneID"].Value));
             PFS.ChangeStatBlockTwo(int.Parse(result.Data["StatBlockTwoID"].Value));
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
            GetFriends();
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

    public void SubmitFriendRequest()
    {
        AddFriend(FriendIdType.PlayFabId, FriendSearch.text);
    }
    #endregion

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
