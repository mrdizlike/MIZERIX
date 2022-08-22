using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class S_Skill : MonoBehaviour
{
    //private Player_MAIN PM;
    //private PlayerSTAT PS;
    //private SkillManager SM;

    //private void Start()
    //{
    //    PM = GetComponent<Player_MAIN>();
    //    PS = GetComponent<PlayerSTAT>();
    //    SM = GetComponent<SkillManager>();
    //}

    //public void OnStartHeal()
    //{
    //    PS.Debuffs.Add(new Buff(Buff.BuffID.SoldierHeal_Buff, 5f, 0.5f, 0, false));
    //    PM.MainAux.PlayOneShot(SM.Skills[1].Skill_Audio);
    //    PS.EnableDeBuff = true;
    //    PS.HPRegen_Amount += SM.Skills[1].SomeValue;
    //    GetComponent<PhotonView>().RPC("EnableOrDisable", RpcTarget.All, 1);
    //    SM.SkillsEffectUI[0].SetActive(true);
    //    SM.Skills[1].Skill_Active = false;
    //}

    //[PunRPC]
    //private void EnableOrDisable(int NumberOfAction)
    //{
    //    if(NumberOfAction == 0)
    //    {
    //        SM.Skills[1].Skill_Effect.GetComponent<ParticleSystem>().Stop();
    //        SM.Skills[1].Skill_Effect.SetActive(false);
    //    }

    //    if (NumberOfAction == 1)
    //    {
    //        SM.Skills[1].Skill_Effect.SetActive(true);
    //        SM.Skills[1].Skill_Effect.GetComponent<ParticleSystem>().Play();
    //    }
    //}
}
