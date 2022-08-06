using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAnouncmentHUD : MonoBehaviour
{
    public ThroneSystem TS;

    public GameObject DefendYourStructure;
    public GameObject AttackEnemyStructure;

    private void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            if(TS.DisableMusic)
            {
                TS.DisableMusic = false;
                GetComponent<Player_MAIN>().Calm = true;
            }

            if (!TS.LightTeamCaptured && !TS.DarkTeamCaptured)
            {
                DefendYourStructure.SetActive(false);
                AttackEnemyStructure.SetActive(false);
            }

            if (gameObject.tag == "LightTeam")
            {
                if (TS.LightTeamCaptured)
                {
                    AttackEnemyStructure.SetActive(true);
                    DefendYourStructure.SetActive(false);
                    if(TS.EnableMusic)
                    {
                        GetComponent<Player_MAIN>().AttackEnemyBase = true;
                        TS.EnableMusic = false;
                    }
                }

                if (TS.DarkTeamCaptured)
                {
                    DefendYourStructure.SetActive(true);
                    AttackEnemyStructure.SetActive(false);
                    if(TS.EnableMusic)
                    {
                        GetComponent<Player_MAIN>().DefendBase = true;
                        TS.EnableMusic = false;
                    }
                }
            }

            if (gameObject.tag == "DarkTeam")
            {
                if (TS.LightTeamCaptured)
                {
                    DefendYourStructure.SetActive(true);
                    AttackEnemyStructure.SetActive(false);
                    if(TS.EnableMusic)
                    {
                        GetComponent<Player_MAIN>().DefendBase = true;
                        TS.EnableMusic = false;
                    }
                }

                if (TS.DarkTeamCaptured)
                {
                    AttackEnemyStructure.SetActive(true);
                    DefendYourStructure.SetActive(false);
                    if(TS.EnableMusic)
                    {
                        GetComponent<Player_MAIN>().AttackEnemyBase = true;
                        TS.EnableMusic = false;
                    }
                }
            }
        }
    }
}
