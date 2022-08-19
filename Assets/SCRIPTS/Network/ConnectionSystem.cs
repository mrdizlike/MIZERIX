using UnityEngine;
using UnityEngine.SceneManagement;
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

    void Start()
    {
        Connect_Button.interactable = true; //Тут потом будет таймер до начала матча и бла бла бла
    }
    private void Update()
    {
        Timer_Text.text = string.Format("{0:00}:{1:00}", MinuteTimer, SecondTimer);
        SecondTimer += Time.deltaTime;

        if(SecondTimer >= 59)
        {
            SecondTimer = 0;
            MinuteTimer += 1;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
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
