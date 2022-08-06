using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Photon.Pun;

public class SkillManager : MonoBehaviour
{
    const GameObject NO_EFFECT = null;
    public int UpgradeToken;
    public bool BlockUse;

    public Player_MAIN PM;
    public SkillsList SkillsList;
    public List<Skill> Skills;

    public GameObject[] SkillsLock_UI;
    public GameObject[] SkillsCoolDown_UI;
    public GameObject[] SkillsUpgradeButton_UI;
    public GameObject[] FirstSkillLVL_UI;
    public GameObject[] SecondSkillLVL_UI;
    public GameObject[] ThirdSkillLVL_UI;
    public GameObject[] SkillsEffectUI;
    public GameObject[] SkillsEffect;

    public AudioClip[] SkillsAudio;

    private void Start()
    {
        Skills[0] = new Skill(PM.PlayerClassification, false, SkillsList.FirstSkillCoolDown[0], 0, SkillsList.FirstSkillValue[0], SkillsAudio[0], SkillsLock_UI[0],
                        SkillsCoolDown_UI[0], SkillsUpgradeButton_UI[0], FirstSkillLVL_UI, SkillsEffect[0]);

        Skills[1] = new Skill(PM.PlayerClassification, false, SkillsList.SecondSkillCoolDown[0], 0, SkillsList.SecondSkillValue[0], SkillsAudio[1], SkillsLock_UI[1],
                    SkillsCoolDown_UI[1], SkillsUpgradeButton_UI[1], SecondSkillLVL_UI, SkillsEffect[1]);

        Skills[2] = new Skill(PM.PlayerClassification, false, SkillsList.ThirdSkillCoolDown[0], 0, SkillsList.ThirdSkillValue[0], SkillsAudio[2], SkillsLock_UI[2],
                    SkillsCoolDown_UI[2], SkillsUpgradeButton_UI[2], ThirdSkillLVL_UI, SkillsEffect[2]);
    }

    private void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            SkillUpgrade();

            if (PM.PlayerClassification == Skill.PlayerClass.Soldier && !PM.ChatOpen)
            {
                SoldierSkillSys();
            }
        }
    }

    void SkillUpgrade()
    {
        if(UpgradeToken == 0)
        {
            Skills[0].UpgradeButton_UI.SetActive(false);
            Skills[1].UpgradeButton_UI.SetActive(false);
            Skills[2].UpgradeButton_UI.SetActive(false);
        }

        if (!Input.GetKey(KeyCode.LeftControl) && !GetComponent<PlayerSTAT>().Debuffs.Contains(BuffList.MirrorBuff))
        {
            BlockUse = false;
        }

        if (UpgradeToken > 0)
        {
            foreach (Skill i in Skills)
            {
                if(i.Skill_Level != 3)
                {
                    i.UpgradeButton_UI.SetActive(true);
                }              
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                BlockUse = true;
                if (Input.GetKeyDown(KeyCode.Q) && Skills[0].Skill_Level != 3)
                {
                    Skills[0].Skill_Level++;
                    Skills[0].CoolDown_UI.GetComponent<Image>().fillAmount = 0;
                    Skills[0].SkillLVL_UI[Skills[0].Skill_Level - 1].SetActive(true);
                    Skills[0].Lock_UI.GetComponent<Animator>().Play("LockOut");                    
                    Skills[0].UpgradeButton_UI.SetActive(false);
                    Skills[0].Skill_CoolDown = SkillsList.FirstSkillCoolDown[Skills[0].Skill_Level - 1];
                    Skills[0].SomeValue = SkillsList.FirstSkillValue[Skills[0].Skill_Level - 1];

                    UpgradeToken--;
                }

                if (Input.GetKeyDown(KeyCode.E) && Skills[1].Skill_Level != 3)
                {
                    Skills[1].Skill_Level++;
                    Skills[1].CoolDown_UI.GetComponent<Image>().fillAmount = 0;
                    Skills[1].SkillLVL_UI[Skills[1].Skill_Level - 1].SetActive(true);
                    Skills[1].Lock_UI.GetComponent<Animator>().Play("LockOut");
                    Skills[1].UpgradeButton_UI.SetActive(false);
                    Skills[1].Skill_CoolDown = SkillsList.SecondSkillCoolDown[Skills[1].Skill_Level - 1];
                    Skills[1].SomeValue = SkillsList.SecondSkillValue[Skills[1].Skill_Level - 1];

                    UpgradeToken--;
                }

                if (Input.GetKeyDown(KeyCode.Z) && Skills[2].Skill_Level != 3)
                {
                    Skills[2].Skill_Level++;
                    Skills[2].CoolDown_UI.GetComponent<Image>().fillAmount = 0;
                    Skills[2].SkillLVL_UI[Skills[2].Skill_Level - 1].SetActive(true);
                    Skills[2].Lock_UI.GetComponent<Animator>().Play("LockOut");
                    Skills[2].UpgradeButton_UI.SetActive(false);
                    Skills[2].Skill_CoolDown = SkillsList.ThirdSkillCoolDown[Skills[2].Skill_Level - 1];
                    Skills[2].SomeValue = SkillsList.ThirdSkillValue[Skills[2].Skill_Level - 1];

                    UpgradeToken--;
                }
            }
        }

        if(GetComponent<PlayerSTAT>().Dead)
        {
            for(int i = 0;i < Skills.Count;i++)
            {
                if(Skills[i].Skill_Level > 0)
                {
                    Skills[i].Lock_UI.SetActive(false);
                }
            }
        }
    }

    void SoldierSkillSys()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKeyDown(KeyCode.Q) && Skills[0].Skill_Active && !BlockUse && !PM.SafeZone)
            {
                GetComponent<F_Skill>().OnStartDash();
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && Skills[1].Skill_Active && !BlockUse && !PM.SafeZone)
        {
            GetComponent<S_Skill>().OnStartHeal();
        }

        if (!GetComponent<PlayerSTAT>().Debuffs.Contains(BuffList.ShieldBuff))
        {
            if (Input.GetKeyDown(KeyCode.Z) && Skills[2].Skill_Active && !BlockUse && !PM.SafeZone)
            {
                GetComponent<T_Skill>().PrepareBazooka();
                GetComponent<T_Skill>().Missle.GetComponent<Bazooka_Missle>().dmg = Skills[2].SomeValue;
            }

            if (Input.GetKeyUp(KeyCode.Z) && Skills[2].Skill_Active && !BlockUse && !PM.SafeZone)
            {
                GetComponent<T_Skill>().ShootBazooka();
            }

            if (Input.GetKeyDown(KeyCode.Z) && GetComponent<PlayerSTAT>().Dead)
            {
                GetComponent<T_Skill>().photonView.RPC("NetworkAnimation", RpcTarget.All, 2);
            }
        }


        foreach (Skill i in Skills)
        {
            if (!i.Skill_Active && i.Skill_Level > 0)
            {
                i.CoolDown_UI.SetActive(true);
                i.CoolDown_UI.GetComponent<Image>().fillAmount -= 1 / i.Skill_CoolDown * Time.deltaTime;
                if (i.CoolDown_UI.GetComponent<Image>().fillAmount <= 0)
                {
                    i.CoolDown_UI.GetComponent<Image>().fillAmount = 1;
                    i.CoolDown_UI.SetActive(false);
                    i.Skill_Active = true;
                }
            }
        }
    }
}
