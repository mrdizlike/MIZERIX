using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_SoldierSkillFactory : AF_SkillFactory
{
    public override AF_Skill CreateSkill()
    {
        return gameObject.AddComponent<F_SoldierSkill>();
    }
}
