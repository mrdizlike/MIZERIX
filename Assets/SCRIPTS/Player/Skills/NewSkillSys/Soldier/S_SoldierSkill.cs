using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class S_SoldierSkill : A_Skill
{
    SkillManager SM;

    protected override void Start()
    {
        SM = GetComponent<SkillManager>();
        _PM = GetComponent<Player_MAIN>();
        _PS = GetComponent<PlayerSTAT>();

        _SkillLock_UI = SM.SkillLock_UI[1];
        _SkillCoolDown_UI = SM.SkillCoolDown_UI[1];
        _SkillUpgradeButton_UI = SM.SkillUpgradeButton_UI[1];
        _SkillLVL_UI = SM.SecondSkillLVL_UI;
        _SkillEffectUI = SM.SkillEffectUI[1];
        _SkillEffect = SM.SkillEffect[1];
        _SkillAudio = SM.SkillAudio[1];
        _SomeValue = SM.SomeValue[1];
        _CooldownTime = SM.SkillsCooldown[1];
        _Key = KeyCode.E;
        _SkillActive = false;
    }

    protected override void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            SkillUpgrade();
            CoolDownSys();

            if (Input.GetKeyDown(_Key) && _SkillActive && !_PM.BlockUse && !_PM.SafeZone)
            {
                OnStartHeal();
            }
        }
    }

    public void OnStartHeal()
    {
        _SkillActive = false;
        _SkillCoolDown_UI.GetComponent<Image>().fillAmount = 1;
        _PS.Debuffs.Add(new Buff(Buff.BuffID.SoldierHeal_Buff, 5f, 0.5f, 0, false));
        _PM.MainAux.PlayOneShot(_SkillAudio);
        _PS.EnableDeBuff = true;
        _PS.HPRegen_Amount += _SomeValue;
        GetComponent<PhotonView>().RPC("EnableOrDisable", RpcTarget.All, 1);
        _SkillEffectUI.SetActive(true);
    }

    [PunRPC]
    private void EnableOrDisable(int NumberOfAction)
    {
        if (NumberOfAction == 0)
        {
            _SkillEffect.GetComponent<ParticleSystem>().Stop();
            _SkillEffect.SetActive(false);
        }

        if (NumberOfAction == 1)
        {
            _SkillEffect.SetActive(true);
            _SkillEffect.GetComponent<ParticleSystem>().Play();
        }
    }

    protected override void UpgradeSkillStat()
    {
        switch (_SkillLevel)
        {
            case 1:
                _CooldownTime = 15;
                _SomeValue = 5;
                break;
            case 2:
                _CooldownTime = 10;
                _SomeValue = 7;
                break;
            case 3:
                _CooldownTime = 8;
                _SomeValue = 9;
                break;
        }
    }
}
