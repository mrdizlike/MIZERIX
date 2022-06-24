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
                }

                if (TS.DarkTeamCaptured)
                {
                    DefendYourStructure.SetActive(true);
                }
            }

            if (gameObject.tag == "DarkTeam")
            {
                if (TS.LightTeamCaptured)
                {
                    DefendYourStructure.SetActive(true);
                }

                if (TS.DarkTeamCaptured)
                {
                    AttackEnemyStructure.SetActive(true);
                }
            }
        }
    }
}
