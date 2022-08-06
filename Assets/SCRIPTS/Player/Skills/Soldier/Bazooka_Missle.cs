using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Bazooka_Missle : MonoBehaviour
{
    private Rigidbody RB;
    public PhotonView Photon_Player;
    public GameObject Particles;
    [SerializeField]
    public float MaxLifeTime;
    private float speed = 1f;
    public float dmg = 25;

    private void Update()
    {
        OnMissleFired();
    }
    private void Awake()
    {
        RB = GetComponent<Rigidbody>();
    }

    private void OnMissleFired()
    {
        Vector3 forward = RB.transform.forward;
        RB.AddForce(forward * speed, ForceMode.Impulse);

    }

    void LightTeamDMG(Collider other)
    {
        if (Photon_Player.tag == "LightTeam" && other.tag == "DarkTeam")
        {
            other.GetComponent<PhotonView>().RPC("ReceiveDMG", RpcTarget.All, dmg);
            other.GetComponent<Player_MAIN>().controller.Move(transform.TransformDirection(transform.up) * 10f * Time.deltaTime); //������������ �����, ����� ��������� � ������� ����
            Photon_Player.GetComponent<GunScript>().DmgTextRelease(dmg);
            GameObject Boom = PhotonNetwork.Instantiate(Particles.name, transform.position, transform.rotation);
            Boom.GetComponent<BazookaBoom>().Photon_Player = Photon_Player;
            Boom.GetComponent<BazookaBoom>().dmg = dmg;
            PhotonNetwork.Destroy(gameObject);

            if (other.GetComponent<PlayerSTAT>().Buffs.Contains(BuffList.TyranitBeltBuff) && !other.GetComponent<PlayerSTAT>().Debuffs.Contains(BuffList.TyranitBeltBuff))
            {
                other.GetComponent<ItemSysScript>().TyranitBeltSys();
                PhotonNetwork.Destroy(gameObject);
                PhotonNetwork.Instantiate(Particles.name, transform.position, transform.rotation);
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
            GameObject Boom = PhotonNetwork.Instantiate(Particles.name, transform.position, transform.rotation);
            Boom.GetComponent<BazookaBoom>().Photon_Player = Photon_Player;
            Boom.GetComponent<BazookaBoom>().dmg = dmg;
            PhotonNetwork.Destroy(gameObject);
        }
    }

    void DarkTeamDMG(Collider other)
    {
        if (Photon_Player.tag == "DarkTeam" && other.tag == "DarkTeam")
        {
            other.GetComponent<PhotonView>().RPC("ReceiveDMG", RpcTarget.All, dmg);
            other.GetComponent<Player_MAIN>().controller.Move(transform.TransformDirection(transform.up) * 10f * Time.deltaTime); //������������ �����, ����� ��������� � ������� ����
            Photon_Player.GetComponent<GunScript>().DmgTextRelease(dmg);
            PhotonNetwork.Destroy(gameObject);
            PhotonNetwork.Instantiate(Particles.name, transform.position, transform.rotation);

            if (other.GetComponent<PlayerSTAT>().Buffs.Contains(BuffList.TyranitBeltBuff) && !other.GetComponent<PlayerSTAT>().Debuffs.Contains(BuffList.TyranitBeltBuff))
            {
                other.GetComponent<ItemSysScript>().TyranitBeltSys();
                GameObject Boom = PhotonNetwork.Instantiate(Particles.name, transform.position, transform.rotation);
                Boom.GetComponent<BazookaBoom>().Photon_Player = Photon_Player;
                Boom.GetComponent<BazookaBoom>().dmg = dmg;
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
            GameObject Boom = PhotonNetwork.Instantiate(Particles.name, transform.position, transform.rotation);
            Boom.GetComponent<BazookaBoom>().Photon_Player = Photon_Player;
            Boom.GetComponent<BazookaBoom>().dmg = dmg;
            PhotonNetwork.Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        LightTeamDMG(other);
        DarkTeamDMG(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            GameObject Boom = PhotonNetwork.Instantiate(Particles.name, transform.position, transform.rotation);
            Boom.GetComponent<BazookaBoom>().Photon_Player = Photon_Player;
            Boom.GetComponent<BazookaBoom>().dmg = dmg;
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
