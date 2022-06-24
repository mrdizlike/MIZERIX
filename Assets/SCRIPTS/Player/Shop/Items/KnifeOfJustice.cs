using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class KnifeOfJustice : MonoBehaviour
{
    public Rigidbody RB;
    public PhotonView Photon_Player;

    public float speed;
    public float Damage;

    private void Update()
    {
        OnKnifeFired();
    }

    private void OnKnifeFired()
    {
        Vector3 forward = RB.transform.forward;
        RB.AddForce(forward * speed, ForceMode.Impulse);

    }
    private void OnTriggerEnter(Collider other)
    {
        LightTeamDMG(other);
        DarkTeamDMG(other);
    }

    void LightTeamDMG(Collider other)
    {
        if (Photon_Player.tag == "LightTeam" && other.tag == "DarkTeam" && other.GetComponent<PhotonView>().ViewID != Photon_Player.ViewID) //!!!Заменить на командный тег
        {
            other.GetComponent<Player_MAIN>().Hater = Photon_Player.gameObject;
            other.GetComponent<PhotonView>().RPC("ReceiveDMG", RpcTarget.All, Damage);
            Photon_Player.GetComponent<GunScript>().DmgTextRelease(Damage);
            Photon_Player.GetComponent<GunScript>().SoundScript.HitCheck();
            other.GetComponent<PlayerSTAT>().Debuffs.Add(BuffList.BleedDebuff);
            other.GetComponent<PlayerSTAT>().EnableDeBuff = true;
            PhotonNetwork.Destroy(gameObject);
        }

        if (Photon_Player.tag == "LightTeam" && other.tag == "DarkTeam" && other.GetComponent<PlayerSTAT>().HP_Amount <= 0) //Убийство игрока
        {
            Photon_Player.GetComponent<GunScript>().SoundScript.DeathHitCheck();

            if (Photon_Player.GetComponent<PlayerSTAT>().Buffs.Contains(BuffList.InfectedSkullBuff)) //Предмет
            {
                Photon_Player.GetComponent<ItemSysScript>().InfectedSkull_Count++;
            }
        }
    }

    void DarkTeamDMG(Collider other)
    {
        if (Photon_Player.tag == "DarkTeam" && other.tag == "LightTeam" && other.GetComponent<PhotonView>().ViewID != Photon_Player.ViewID) //!!!Заменить на командный тег
        {
            other.GetComponent<Player_MAIN>().Hater = Photon_Player.gameObject;
            other.GetComponent<PhotonView>().RPC("ReceiveDMG", RpcTarget.All, Damage);
            Photon_Player.GetComponent<GunScript>().DmgTextRelease(Damage);
            Photon_Player.GetComponent<GunScript>().SoundScript.HitCheck();
            other.GetComponent<PlayerSTAT>().Debuffs.Add(BuffList.BleedDebuff);
            other.GetComponent<PlayerSTAT>().EnableDeBuff = true;
            PhotonNetwork.Destroy(gameObject);
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
