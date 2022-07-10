using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DarkBossLaserWall : MonoBehaviour
{
    public GameObject DarkBoss_Prefab;
    public void OnTriggerEnter(Collider other)
    {        
        if(other.tag == "LightTeam" || other.tag == "DarkTeam" && DarkBoss_Prefab.GetComponent<PhotonView>().IsMine)
        {
            other.GetComponent<PhotonView>().RPC("ReceiveDMG", RpcTarget.All, 60f);
            other.GetComponent<Player_MAIN>().Hater = DarkBoss_Prefab;
        }
    }
}
