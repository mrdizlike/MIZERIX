using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ConnectionSystem : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject player;

    GameObject PlayerObject;

    public GameObject LightBaseProtection;
    public GameObject DarkBaseProtection;

    public Transform[] LightSpawn;
    public Transform[] DarkSpawn;
    public int CountOfPlayers;
    public float MinuteTimer;
    public float SecondTimer;

    public Button Connect_Button;
    public Text Timer_Text;

    public KillFeed KillFeed_Script;
    public GameObject ChatPanel;
    public GameObject ChatContent;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    private void Update()
    {
        Timer_Text.text = string.Format("{0:00}:{1:00}", MinuteTimer, SecondTimer);
        SecondTimer += Time.deltaTime;

        if(SecondTimer >= 59)
        {
            SecondTimer = 0;
            MinuteTimer++;
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected!");
        PhotonNetwork.AutomaticallySyncScene = true;

        RoomOptions ro = new RoomOptions { MaxPlayers = 10, IsOpen = true, IsVisible = true };

        PhotonNetwork.JoinOrCreateRoom("NORMAL_5x5", ro, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("U in game!");
        Connect_Button.interactable = true;
        base.OnJoinedRoom();
    }

    public void Spawn()
    {
        photonView.RPC("CountOfPlayer", RpcTarget.All, 0);
        SpawnPoints(CountOfPlayers);
    }
    
    void SpawnPoints(int PlayerCount)
    {
        if (CountOfPlayers % 2 == 0)
        {
            PlayerObject = PhotonNetwork.Instantiate(player.name, LightSpawn[CountOfPlayers / 2 - 1].position, LightSpawn[CountOfPlayers / 2].rotation);
            PlayerObject.GetComponent<Player_MAIN>().SpawnPoint = LightSpawn[CountOfPlayers / 2 - 1].gameObject;
            PlayerObject.tag = "LightTeam";
        }
        else
        {
            PlayerObject = PhotonNetwork.Instantiate(player.name, DarkSpawn[CountOfPlayers / 2].position, DarkSpawn[CountOfPlayers / 2].rotation);
            PlayerObject.GetComponent<Player_MAIN>().SpawnPoint = DarkSpawn[CountOfPlayers / 2].gameObject;
            PlayerObject.tag = "DarkTeam";
        }

        PlayerObject.GetComponent<Player_MAIN>().LightBaseProtection = LightBaseProtection;
        PlayerObject.GetComponent<Player_MAIN>().DarkBaseProtection = DarkBaseProtection;
        PlayerObject.GetComponent<Player_MAIN>().CS = GetComponent<ConnectionSystem>();
        PlayerObject.GetComponent<Player_MAIN>().ChatPanel = ChatPanel;
        PlayerObject.GetComponent<Player_MAIN>().ChatContent = ChatContent;
        PlayerObject.GetComponent<PlayerAnouncmentHUD>().TS = GetComponent<ThroneSystem>();
    }

   

    [PunRPC]
    public void CountOfPlayer(int ActionID)
    {
        if(ActionID == 0)
        {
            CountOfPlayers++;          
        }

        if (ActionID == 1)
        {
            CountOfPlayers--;
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(CountOfPlayers);
            stream.SendNext(MinuteTimer);
            stream.SendNext(SecondTimer);
        }
        else if (stream.IsReading)
        {
            CountOfPlayers = (int)stream.ReceiveNext();
            MinuteTimer = (float)stream.ReceiveNext();
            SecondTimer = (float)stream.ReceiveNext();
        }
    }
}
