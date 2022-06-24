using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ElectricDamage : MonoBehaviour
{
    public float Damage;
    public PhotonView PhotonID_Player;

    private void OnTriggerEnter(Collider other)
    {
        if(PhotonID_Player.tag == "LightTeam" && other.tag == "DarkTeam" && other.GetComponent<PhotonView>().ViewID != PhotonID_Player.ViewID)
        {
            other.GetComponent<Player_MAIN>().Hater = PhotonID_Player.gameObject;
            other.GetComponent<PlayerSTAT>().Debuffs.Add(BuffList.ElectricDebuff);
            other.GetComponent<PlayerSTAT>().EnableDeBuff = true;
        }

        if (PhotonID_Player.tag == "DarkTeam" && other.tag == "LightTeam" && other.GetComponent<PhotonView>().ViewID != PhotonID_Player.ViewID)
        {
            other.GetComponent<Player_MAIN>().Hater = PhotonID_Player.gameObject;
            other.GetComponent<PlayerSTAT>().Debuffs.Add(BuffList.ElectricDebuff);
            other.GetComponent<PlayerSTAT>().EnableDeBuff = true;
        }
    }
}
