using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class T_Skill : MonoBehaviourPun, IPunObservable
{
    private WeaponMain WM;
    private GunScript GS;
    private SkillManager SM;
    public GameObject Bazooka;
    public GameObject Missle;
    public Transform SpawnPoint;

    [Header("Skill_Options(Soldier)")]
    private bool BazookaIsReady;


    private void Start()
    {
        WM = GetComponent<WeaponMain>();
        GS = GetComponent<GunScript>();
        SM = GetComponent<SkillManager>();

        if (!photonView.IsMine)
        {
            Bazooka.layer = 0;
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (BazookaIsReady && GetComponent<PlayerSTAT>().Dead)
            {
                ShootBazooka();
                photonView.RPC("NetworkAnimation", RpcTarget.All, 2);
            }
        }      
    }

    public void PrepareBazooka()
    {
        BazookaIsReady = true;
        SM.Skills[2].Skill_Effect.GetComponent<ParticleSystem>().Pause();
        photonView.RPC("NetworkAnimation", RpcTarget.All, 1);
        GS.blockUsing = true;
    }

    public void ShootBazooka()
    {
        photonView.RPC("NetworkAnimation", RpcTarget.All, 0);
        photonView.RPC("BazookaMissleRPC", RpcTarget.All);

        SM.Skills[2].Skill_Effect.GetComponent<ParticleSystem>().Play();
        SM.Skills[2].Skill_Effect.SetActive(true); //Чтобы частицы не активировались сразу после активации базуки
        BazookaIsReady = false;
        SM.Skills[2].Skill_Active = false;
        GS.blockUsing = false;
        GetComponent<Player_MAIN>().MainAux.PlayOneShot(SM.Skills[2].Skill_Audio);
    }

    [PunRPC]
    void BazookaMissleRPC()
    {
        GameObject Missle_Object = Instantiate(Missle, SpawnPoint.position, SpawnPoint.rotation);
        Missle_Object.GetComponent<Bazooka_Missle>().Photon_Player = GetComponent<PhotonView>();
    }

    [PunRPC]
    public void NetworkAnimation(int AnimIndex)
    {
        if (AnimIndex == 0)
        {
            Bazooka.GetComponent<Animator>().Play("Hide");
            WM.Weapon_Animator.Play("Pull_Out");
        }

        if (AnimIndex == 1)
        {
            WM.Weapon_Animator.Play("Hide");
            Bazooka.SetActive(true);
        }

        if (AnimIndex == 2)
        {
            Bazooka.SetActive(false);
        }

        if (AnimIndex == 3)
        {
            WM.Weapon_Animator.Play("Hide");
        }

        if (AnimIndex == 4)
        {
            WM.Weapon_Animator.Play("Pull_Out");
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(BazookaIsReady);
        }
        else if (stream.IsReading)
        {
            BazookaIsReady = (bool)stream.ReceiveNext();
        }
    }

}
