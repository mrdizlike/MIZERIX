using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Ultimate_SoldierSkill : A_Skill
{
    SkillManager SM;
    Animator Soldier_Animator;

    bool SkillBlock;
    float Skill_CoolDown;
    float ChargingSpeed;

    AudioSource AFX_Main;
    AudioSource AFX_Background;
    AudioClip UltimateMainSound;

    Text UltimateText;
    GameObject UltimateParticles;

    protected override void Start()
    {
        SM = GetComponent<SkillManager>();
        _PM = GetComponent<Player_MAIN>();
        _PS = GetComponent<PlayerSTAT>();
        Soldier_Animator = GetComponent<Animator>();
        AFX_Main = GetComponent<AudioSource>();
        AFX_Background = GameObject.Find("GunShoot_Point").GetComponent<AudioSource>();
        UltimateText = GameObject.Find("Cooldown").GetComponent<Text>();
        UltimateText.gameObject.SetActive(false);

        ChargingSpeed = 1f;
        _SkillLock_UI = SM.SkillLock_UI[3];
        _SkillCoolDown_UI = SM.SkillCoolDown_UI[3];
        _SkillUpgradeButton_UI = SM.SkillUpgradeButton_UI[3];
        _SkillEffectUI = SM.SkillEffectUI[3];
        _SkillEffect = SM.SkillEffect[3];
        _SkillAudio = SM.SkillAudio[3];
        _SomeValue = SM.SomeValue[3];
        _CooldownTime = SM.SkillsCooldown[3];
        _Key = KeyCode.X;
        _SkillActive = false;
    }

    protected override void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            CoolDownSys();

            if (Input.GetKey(_Key) && _SkillActive && !_PM.BlockUse && !_PM.SafeZone && !_PS.Dead)
            {
                ActivateUltimate();
                GetComponent<PhotonView>().RPC("NetworkSync", RpcTarget.All, 2);
            }
        }
    }

    protected override void CoolDownSys()
    {
        if (SkillBlock)
        {
            if (_PS.Level_Amount >= 3)
            {
                UnlockSkill();
            }
        }

        if (!_SkillActive && !SkillBlock)
        {
            UltimateText.text = ((int)Skill_CoolDown).ToString() + "%";

            if (Skill_CoolDown < 100)
            {
                Skill_CoolDown += ChargingSpeed * Time.deltaTime;
            }

            _SkillCoolDown_UI.GetComponent<Image>().fillAmount = Skill_CoolDown / 100;

            if (Skill_CoolDown >= 100)
            {
                _SkillActive = true;
                UltimateText.gameObject.SetActive(false);
            }
        }
    }

    void UnlockSkill()
    {
        SkillBlock = false;
        _SkillLock_UI.SetActive(false);
        _SkillCoolDown_UI.SetActive(false);
        _SkillLock_UI.GetComponent<Animator>().Play("LockOut");
        _SkillCoolDown_UI.GetComponent<Image>().fillAmount = 0;
        UltimateText.gameObject.SetActive(true);
    }

    void ActivateUltimate()
    {
        StartCoroutine(isActivated());
        GetComponent<PhotonView>().RPC("NetworkSync", RpcTarget.All, 0);
    }

    void DeactivateUltimate()
    {
        GetComponent<PhotonView>().RPC("NetworkSync", RpcTarget.All, 1);
    }
    IEnumerator isActivated()
    {
        float currentAmount = 0;
        do
        {
            yield return new WaitForSeconds(0.1f);
            currentAmount += 0.1f;
        } while (currentAmount <= 10);
        DeactivateUltimate();
        currentAmount = 0;
    }

    [PunRPC]
    void NetworkSync(int AnimIndex)
    {
        if (AnimIndex == 0)
        {
            _PS.HP_Amount += 250;
            _PS.HP_MaxAmount += 250;
            _PS.Armor_Amount += 5;
            _PS.DMG_Amount += 15;
            UltimateParticles.SetActive(true);
            _SkillActive = false;
            Skill_CoolDown = 0;
            UltimateText.gameObject.SetActive(true);
            AFX_Main.PlayOneShot(_SkillAudio);
        }

        if (AnimIndex == 1)
        {
            _PS.HP_MaxAmount -= 250;
            _PS.Armor_Amount -= 5;
            _PS.DMG_Amount -= 15;
            UltimateParticles.SetActive(false);
            Soldier_Animator.Play("Soldier_Idle_1");
        }

        if (AnimIndex == 2)
        {
            Soldier_Animator.Play("Soldier_Ultimate");
        }
    }

    protected override void UpgradeSkillStat()
    {

    }
}
