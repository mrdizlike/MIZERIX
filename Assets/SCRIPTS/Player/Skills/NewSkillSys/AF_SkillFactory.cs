using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AF_SkillFactory : MonoBehaviour
{
    public abstract AF_Skill CreateSkill();
}
