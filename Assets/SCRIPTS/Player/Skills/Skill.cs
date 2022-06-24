using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject
{
    public enum PlayerClass : int
    {
        Soldier = 1,
        Scout = 2,
        Medic = 3,
        Engineer = 4,
        Shadow = 5
    }

    public PlayerClass PlayerClassification;
    public bool Skill_Active;
    public float Skill_CoolDown;
    public int Skill_Level;
    public float SomeValue;
    public AudioClip Skill_Audio;
    public GameObject Lock_UI;
    public GameObject CoolDown_UI;
    public GameObject UpgradeButton_UI;
    public GameObject[] SkillLVL_UI;
    public GameObject Skill_Effect;

    public Skill(PlayerClass PlayerClassification,
        bool Skill_Active,
        float Skill_CoolDown,
        int Skill_Level,
        float SomeValue,
        AudioClip Skill_Audio,
        GameObject Lock_UI,
        GameObject CoolDown_UI,         
        GameObject UpgradeButton_UI,
        GameObject[] SkillLVL_UI,
        GameObject Skill_Effect)
    {
        this.PlayerClassification = PlayerClassification;
        this.Skill_Active = Skill_Active;
        this.Skill_CoolDown = Skill_CoolDown;
        this.Skill_Level = Skill_Level;
        this.SomeValue = SomeValue;
        this.Skill_Audio = Skill_Audio;
        this.Lock_UI = Lock_UI;
        this.CoolDown_UI = CoolDown_UI;
        this.UpgradeButton_UI = UpgradeButton_UI;
        this.SkillLVL_UI = SkillLVL_UI;
        this.Skill_Effect = Skill_Effect;
    }
}
