using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class BazookaBoom : MonoBehaviour
{
    public PhotonView Photon_Player;
    public float dmg;

    void LightTeamDMG(Collider other)
    {
        if (Photon_Player.tag == "LightTeam" && other.tag == "DarkTeam")
        {
            other.GetComponent<PhotonView>().RPC("ReceiveDMG", RpcTarget.All, dmg);
            other.GetComponent<Player_MAIN>().controller.Move(transform.TransformDirection(transform.up) * 10f * Time.deltaTime); //������������ �����, ����� ��������� � ������� ����
            Photon_Player.GetComponent<GunScript>().DmgTextRelease(dmg);
            PhotonNetwork.Destroy(gameObject);

            if (other.GetComponent<PlayerSTAT>().Buffs.Contains(BuffList.TyranitBeltBuff) && !other.GetComponent<PlayerSTAT>().Debuffs.Contains(BuffList.TyranitBeltBuff))
            {
                other.GetComponent<ItemSysScript>().TyranitBeltSys();
                PhotonNetwork.Destroy(gameObject);
            }

            if (!other.GetComponent<PlayerSTAT>().Dead && other.GetComponent<PlayerSTAT>().Buffs.Contains(BuffList.SpikeBuff))
            {
                Photon_Player.GetComponent<Player_MAIN>().Hater = other.gameObject;
                Photon_Player.RPC("ReceiveDMG", RpcTarget.All, dmg / 2);
            }
        }

        if (Photon_Player.tag == "LightTeam" && other.tag == "Throne")
        {
            other.GetComponent<ThroneDamage>().TakeDamageDark(dmg);
            Photon_Player.GetComponent<GunScript>().DmgTextRelease(dmg);
            PhotonNetwork.Destroy(gameObject);
        }
    }

    void DarkTeamDMG(Collider other)
    {
        if (Photon_Player.tag == "DarkTeam" && other.tag == "LightTeam")
        {
            other.GetComponent<PhotonView>().RPC("ReceiveDMG", RpcTarget.All, dmg);
            other.GetComponent<Player_MAIN>().controller.Move(transform.TransformDirection(transform.up) * 10f * Time.deltaTime); //������������ �����, ����� ��������� � ������� ����
            Photon_Player.GetComponent<GunScript>().DmgTextRelease(dmg);
            PhotonNetwork.Destroy(gameObject);

            if (other.GetComponent<PlayerSTAT>().Buffs.Contains(BuffList.TyranitBeltBuff) && !other.GetComponent<PlayerSTAT>().Debuffs.Contains(BuffList.TyranitBeltBuff))
            {
                other.GetComponent<ItemSysScript>().TyranitBeltSys();
                PhotonNetwork.Destroy(gameObject);
            }

            if (!other.GetComponent<PlayerSTAT>().Dead && other.GetComponent<PlayerSTAT>().Buffs.Contains(BuffList.SpikeBuff))
            {
                Photon_Player.RPC("ReceiveDMG", RpcTarget.All, dmg / 2);
            }
        }

        if (Photon_Player.tag == "DarkTeam" && other.tag == "Throne")
        {
            other.GetComponent<ThroneDamage>().TakeDamageLight(dmg);
            Photon_Player.GetComponent<GunScript>().DmgTextRelease(dmg);
            PhotonNetwork.Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(GetComponent<PhotonView>().IsMine)
        {
            LightTeamDMG(other);
            DarkTeamDMG(other);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
