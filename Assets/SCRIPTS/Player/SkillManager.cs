using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Photon.Pun;

public class SkillManager : MonoBehaviour
{
    const GameObject NO_EFFECT = null;
    

    public Player_MAIN PM;

    public GameObject[] SkillLock_UI;
    public GameObject[] SkillCoolDown_UI;
    public GameObject[] SkillUpgradeButton_UI;
    public GameObject[] FirstSkillLVL_UI;
    public GameObject[] SecondSkillLVL_UI;
    public GameObject[] ThirdSkillLVL_UI;
    public GameObject[] SkillEffectUI;
    public GameObject[] SkillEffect;
    public float[] SkillsCooldown;
    public float[] SomeValue;

    public AudioClip[] SkillAudio;
    public AF_Skill FirstSkill;
    public AF_SkillFactory F_SkillFactory;

    private void Start()
    {
        switch(PM.PlayerClassification.ToString())
        {
            case "Soldier":
                F_SkillFactory = gameObject.AddComponent<F_SoldierSkillFactory>();
                Debug.Log(F_SkillFactory);
                break;

        }

        FirstSkill = F_SkillFactory.CreateSkill();
        Debug.Log(FirstSkill);
    }

    private void Update()
    {
        FirstSkill.SkillSys();
        FirstSkill.SkillUpgrade();
    }
}
