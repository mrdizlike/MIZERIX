using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class T_SoldierSkill : A_Skill, IPunObservable
{
    SkillManager SM;

    private WeaponMain WM;
    private GunScript GS;
    public GameObject Bazooka;
    public GameObject Missle;
    public Transform SpawnPoint;

    private bool BazookaIsReady;

    protected override void Start()
    {
        SM = GetComponent<SkillManager>();
        GS = GetComponent<GunScript>();
        WM = GetComponent<WeaponMain>();
        _PM = GetComponent<Player_MAIN>();
        _PS = GetComponent<PlayerSTAT>();

        SpawnPoint = SM.SkillObject[0].transform;
        Bazooka = SM.SkillObject[1];
        Missle = SM.SkillObject[2];
        Bazooka.SetActive(false);

        if (!GetComponent<PhotonView>().IsMine)
        {
            Bazooka.layer = 0;
        }

        _SkillLock_UI = SM.SkillLock_UI[2];
        _SkillCoolDown_UI = SM.SkillCoolDown_UI[2];
        _SkillUpgradeButton_UI = SM.SkillUpgradeButton_UI[2];
        _SkillLVL_UI = SM.ThirdSkillLVL_UI;
        _SkillEffectUI = SM.SkillEffectUI[2];
        _SkillEffect = SM.SkillEffect[2];
        _SkillAudio = SM.SkillAudio[2];
        _SomeValue = SM.SomeValue[2];
        _CooldownTime = SM.SkillsCooldown[2];
        _Key = KeyCode.Z;
        _SkillActive = false;
    }

    protected override void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
                SkillUpgrade();
                CoolDownSys();

                if(!_PS.Debuffs.Contains(BuffList.ShieldBuff))
                {
                    if (Input.GetKey(_Key) && _SkillActive && !_PM.BlockUse && !_PM.SafeZone)
                    {
                        PrepareBazooka();
                    }

                    if(Input.GetKeyUp(_Key) && _SkillActive && !_PM.BlockUse && !_PM.SafeZone)
                    {
                        ShootBazooka();
                    }

                    if (BazookaIsReady && _PS.Dead)
                    {
                        ShootBazooka();
                        GetComponent<PhotonView>().RPC("NetworkAnimation", RpcTarget.All, 2);
                    }
                }
                if(_PS.Debuffs.Contains(BuffList.ShieldBuff) && BazookaIsReady)
                {
                    GetComponent<PhotonView>().RPC("NetworkAnimation", RpcTarget.All, 3);
                }
        }
    }

    void PrepareBazooka()
    {
        BazookaIsReady = true;
        _SkillEffect.GetComponent<ParticleSystem>().Pause();
        GetComponent<PhotonView>().RPC("NetworkAnimation", RpcTarget.All, 1);
        GS.blockUsing = true;
    }

    void ShootBazooka()
    {
        GetComponent<PhotonView>().RPC("NetworkAnimation", RpcTarget.All, 0);
        GetComponent<PhotonView>().RPC("BazookaMissleRPC", RpcTarget.All);
        _SkillCoolDown_UI.GetComponent<Image>().fillAmount = 1;

        _SkillEffect.SetActive(true);
        _SkillEffect.GetComponent<ParticleSystem>().Play();
        BazookaIsReady = false;
        _SkillActive = false;
        GS.blockUsing = false;
        _PM.MainAux.PlayOneShot(_SkillAudio);
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
            Bazooka.GetComponent<Animator>().Play("Hide");
        }
    }

    protected override void UpgradeSkillStat()
    {
        switch (_SkillLevel)
        {
            case 1:
                _CooldownTime = 8;
                _SomeValue = 4;
                break;
            case 2:
                _CooldownTime = 7;
                _SomeValue = 6;
                break;
            case 3:
                _CooldownTime = 6;
                _SomeValue = 8;
                break;
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
