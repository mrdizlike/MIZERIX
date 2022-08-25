using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class A_SkillFactory : MonoBehaviour
{
    public abstract List<A_Skill> CreateSkill();
}
