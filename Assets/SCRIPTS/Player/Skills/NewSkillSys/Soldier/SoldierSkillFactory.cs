using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSkillFactory : A_SkillFactory
{
    public override List<A_Skill> CreateSkill()
    {
        List<A_Skill> Skills = new List<A_Skill>();
        Skills.Add(gameObject.AddComponent<F_SoldierSkill>());
        Skills.Add(gameObject.AddComponent<S_SoldierSkill>());
        Skills.Add(gameObject.AddComponent<T_SoldierSkill>());
        Skills.Add(gameObject.AddComponent<Ultimate_SoldierSkill>());

        return Skills;
    }
}
