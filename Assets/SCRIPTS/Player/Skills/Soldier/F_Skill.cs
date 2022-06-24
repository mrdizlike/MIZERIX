using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class F_Skill : MonoBehaviour
{
    private Player_MAIN PM;
    private SkillManager SM;

    [Header("Skill_Options(Soldier)")]
    private float dashStartTime;
    private bool isDashing;
    public float DashSpeed;

    private void Start()
    {
        PM = GetComponent<Player_MAIN>();
        SM = GetComponent<SkillManager>();
    }

    void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            SoldierSkill();
        }
    }

    public void SoldierSkill()
    {
        if (isDashing)
        {
            if (Time.time - dashStartTime <= 0.4f)
            {
                if (PM.movementVector.Equals(Vector3.zero))
                {
                    PM.controller.Move(transform.TransformDirection(PM.movementVector) * DashSpeed * Time.deltaTime);
                }
                else
                {
                    PM.controller.Move(transform.TransformDirection(PM.movementVector) * DashSpeed * Time.deltaTime);
                    SM.Skills[0].Skill_Active = false;
                }
            }
            else
            {
                OnEndDash();
            }
        }
    }

    public void OnStartDash()
    {
        isDashing = true;
        PM.MainAux.PlayOneShot(SM.Skills[0].Skill_Audio);
        dashStartTime = Time.time;
        SM.Skills[0].Skill_Effect.SetActive(true);
    }

    void OnEndDash()
    {
        isDashing = false;
        dashStartTime = 0;
        SM.Skills[0].Skill_Effect.SetActive(false);
    }
}
