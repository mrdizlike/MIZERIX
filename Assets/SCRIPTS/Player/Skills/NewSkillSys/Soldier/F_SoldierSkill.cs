using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class F_SoldierSkill : A_Skill
{
    SkillManager SM;
    bool isDashing;
    float dashStartTime;

    protected override void Start()
    {
        SM = GetComponent<SkillManager>();
        _PM = GetComponent<Player_MAIN>();
        _PS = GetComponent<PlayerSTAT>();

        _SkillLock_UI = SM.SkillLock_UI[0];
        _SkillCoolDown_UI = SM.SkillCoolDown_UI[0];
        _SkillUpgradeButton_UI = SM.SkillUpgradeButton_UI[0];
        _SkillLVL_UI = SM.FirstSkillLVL_UI;
        _SkillEffectUI = SM.SkillEffectUI[0];
        _SkillEffect = SM.SkillEffect[0];
        _SkillAudio = SM.SkillAudio[0];
        _SomeValue = SM.SomeValue[0];
        _CooldownTime = SM.SkillsCooldown[0];
        _Key = KeyCode.Q;
        _SkillActive = false;
    }

    protected override void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            SoldierSkill();
            SkillUpgrade();
            CoolDownSys();

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                if (Input.GetKeyDown(_Key) && _SkillActive && !_PM.BlockUse && !_PM.SafeZone)
                {
                    OnStartDash();
                }
            }
        }
    }

    public void SoldierSkill()
    {
        if (isDashing)
        {
            if (Time.time - dashStartTime <= 0.4f)
            {
                if (_PM.movementVector.Equals(Vector3.zero))
                {
                    _PM.controller.Move(transform.TransformDirection(_PM.movementVector) * _SomeValue  * Time.deltaTime);
                }
                else
                {
                    _PM.controller.Move(transform.TransformDirection(_PM.movementVector) * _SomeValue * Time.deltaTime);
                    _SkillActive = false;
                }
            }
            else
            {
                OnEndDash();
            }
        }
    }

    void OnStartDash()
    {
        isDashing = true;
        _PM.MainAux.PlayOneShot(_SkillAudio);
        dashStartTime = Time.time;
        _SkillEffect.SetActive(true);
    }

    void OnEndDash()
    {
        isDashing = false;
        dashStartTime = 0;
        _SkillEffect.SetActive(false);
    }

    protected override void UpgradeSkillStat()
    {
        switch(_SkillLevel)
        {
            case 1:
                _CooldownTime = 8;
                _SomeValue = 1;
                break;
            case 2:
                _CooldownTime = 7;
                _SomeValue = 2;
                break;
            case 3:
                _CooldownTime = 6;
                _SomeValue = 3;
                break;
        }
    }
}
