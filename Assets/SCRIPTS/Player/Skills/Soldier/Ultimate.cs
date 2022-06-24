using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Ultimate : MonoBehaviourPun, IPunObservable
{
    private PlayerSTAT PS;
    public Animator Soldier_Animator;

    [Header("Skill_MAIN")]
    public bool Skill_State;
    public bool SkillBlock;
    public float Skill_CoolDown;
    public float ChargingSpeed;

    public AudioSource AFX_Main;
    public AudioSource AFX_Background;
    public AudioClip UltimateMainSound;
    public AudioClip UltimateBackgroundSound;

    [Header("GUI")]
    public GameObject Lock_UI;
    public GameObject CoolDown_UI;
    public GameObject SkillBlock_UI;
    public Text UltimateText;
    public GameObject UltimateParticles;


    [Header("Skill_Options")]
    private bool BazookaIsReady;


    private void Start()
    {
        PS = GetComponent<PlayerSTAT>();
        Lock_UI.GetComponent<Animator>().Play("UltimateLockIdle");
    }

    void UnlockSkill()
    {
        SkillBlock = false;
        Lock_UI.SetActive(false);
        SkillBlock_UI.SetActive(false);
        Lock_UI.GetComponent<Animator>().Play("LockOut");
        CoolDown_UI.GetComponent<Image>().fillAmount = 0;
        UltimateText.gameObject.SetActive(true);
    }

    void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            if (Input.GetKeyDown(KeyCode.X) && Skill_State && !GetComponent<Player_MAIN>().SafeZone && !GetComponent<PlayerSTAT>().Dead)
            {
                ActivateUltimate();
                photonView.RPC("NetworkSync", RpcTarget.All, 2);
            }
            Skill();
        }
    }

    public void Skill()
    {
        if (SkillBlock)
        {
            if (PS.Level_Amount >= 3)
            {
                UnlockSkill();
            }
        }

        if (!Skill_State && !SkillBlock)
        {
            UltimateText.text = ((int)Skill_CoolDown).ToString() + "%";

            if (Skill_CoolDown < 100)
            {
                Skill_CoolDown += ChargingSpeed * Time.deltaTime;
            }

            CoolDown_UI.GetComponent<Image>().fillAmount = Skill_CoolDown / 100;

            if (Skill_CoolDown >= 100)
            {
                Skill_State = true;
                UltimateText.gameObject.SetActive(false);
            }
        }
    }

    void ActivateUltimate()
    {
        StartCoroutine(isActivated());
        photonView.RPC("NetworkSync", RpcTarget.All, 0);
    }

    void DeactivateUltimate()
    {
        photonView.RPC("NetworkSync", RpcTarget.All, 1);
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
            PS.HP_Amount += 250;
            PS.HP_MaxAmount += 250;
            PS.Armor_Amount += 5;
            PS.DMG_Amount += 15;
            UltimateParticles.SetActive(true);
            Skill_State = false;
            Skill_CoolDown = 0;
            UltimateText.gameObject.SetActive(true);
            AFX_Main.PlayOneShot(UltimateMainSound);
            AFX_Background.PlayOneShot(UltimateBackgroundSound);
        }

        if (AnimIndex == 1)
        {
            PS.HP_MaxAmount -= 250;
            PS.Armor_Amount -= 5;
            PS.DMG_Amount -= 15;
            UltimateParticles.SetActive(false);
            Soldier_Animator.Play("Soldier_Idle_1");
        }

        if (AnimIndex == 2)
        {
            Soldier_Animator.Play("Soldier_Ultimate");
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {

        }
        else if (stream.IsReading)
        {

        }
    }

}