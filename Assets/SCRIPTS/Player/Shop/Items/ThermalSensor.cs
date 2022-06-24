using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ThermalSensor : MonoBehaviour
{
    public float Damage;
    public PhotonView PhotonID_Player;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<PhotonView>().ViewID != PhotonID_Player.ViewID)
        {
            other.GetComponent<PhotonView>().RPC("ReceiveDMG", RpcTarget.All, Damage);
            PhotonID_Player.RPC("ItemEffect", RpcTarget.All, 2115, true);
            PhotonID_Player.GetComponent<GunScript>().DmgTextRelease(Damage);
        }

        if (other.tag == "Player" && other.GetComponent<PlayerSTAT>().HP_Amount <= 0)
        {
            PhotonID_Player.RPC("ReceiveEXP", RpcTarget.All, 2);
            PhotonID_Player.RPC("ItemEffect", RpcTarget.All, 2115, true);

            if (PhotonID_Player.GetComponent<PlayerSTAT>().Buffs.Contains(BuffList.InfectedSkullBuff)) //Предмет
            {
                PhotonID_Player.GetComponent<ItemSysScript>().InfectedSkull_Count++;
            }
        }

        if (other.tag == "Player" && !other.GetComponent<PlayerSTAT>().Dead && other.GetComponent<PhotonView>().ViewID != PhotonID_Player.ViewID)
        {
            PhotonID_Player.RPC("ReceiveDMG", RpcTarget.All, Damage / 2);
            PhotonID_Player.RPC("ItemEffect", RpcTarget.All, 2115, true);
        }
    }
}
