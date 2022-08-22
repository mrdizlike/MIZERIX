using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Photon.Pun;

public class SkillManager : MonoBehaviour
{
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
    public List<A_Skill> Skills;
    public A_SkillFactory SkillFactory;

    private void Start()
    {
        switch(PM.PlayerClassification.ToString())
        {
            case "Soldier":
                SkillFactory = gameObject.AddComponent<SoldierSkillFactory>();
                break;

        }

        Skills = SkillFactory.CreateSkill();
    }

}
