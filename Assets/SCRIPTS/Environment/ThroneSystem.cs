using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ThroneSystem : MonoBehaviourPunCallbacks, IPunObservable
{
    public MainPoint LightPoint;
    public MainPoint DarkPoint;

    public ThroneDamage LightThroneDMG;
    public ThroneDamage DarkThroneDMG;

    float TimeToDMG;
    float lerpTimer;
    public float chipSpeed;

    public float LightThroneHP;
    public float DarkThroneHP;

    public bool DarkTeamCaptured;
    public bool LightTeamCaptured;

    public bool EnableMusic;
    public bool DisableMusic;

    public Image LightHPBar;
    public Image LightBacksideHPBar;
    public Image DarkHPBar;
    public Image DarkBacksideHPBar;

    public GameObject LightWinText;
    public GameObject DarkWinText;
    void Update()
    {
        UpdateUI(1);
        UpdateUI(0);

        if (photonView.IsMine)
        {
            if(LightPoint.LightPointAmount == 27 && DarkPoint.LightPointAmount == 27)
            {
                photonView.RPC("RPCThroneSys", RpcTarget.All, 4, 0f);
                LightPoint.LightPointAmount = 28;
            } 

            if(LightPoint.LightPointAmount == 0 || DarkPoint.LightPointAmount == 0)
            {
                LightTeamCaptured = false;
                LightPoint.LightPointAmount = 1;
                DarkPoint.LightPointAmount = 1;
                DisableMusic = true;
            }

            if (LightPoint.DarkPointAmount == 27 && DarkPoint.DarkPointAmount == 27)
            {
                photonView.RPC("RPCThroneSys", RpcTarget.All, 5, 0f);
                LightPoint.DarkPointAmount = 28;
            }

            if (LightPoint.DarkPointAmount == 0 || DarkPoint.DarkPointAmount == 0)
            {
                DarkTeamCaptured = false;
                LightPoint.DarkPointAmount = 1;
                DarkPoint.DarkPointAmount = 1;
                DisableMusic = true;
            }

            if (LightTeamCaptured)
            {
                TimeToDMG += Time.deltaTime;
                DarkThroneDMG.canDMG = true;

                if (TimeToDMG >= 2)
                {
                    photonView.RPC("RPCThroneSys", RpcTarget.AllBuffered, 1, 25f);
                    TimeToDMG = 0;
                }
            }
            else
            {
                DarkThroneDMG.canDMG = false;
            }

            if (DarkTeamCaptured)
            {
                TimeToDMG += Time.deltaTime;
                LightThroneDMG.canDMG = true;

                if (TimeToDMG >= 2)
                {
                    photonView.RPC("RPCThroneSys", RpcTarget.AllBuffered, 0, 25f);
                    TimeToDMG = 0;
                }
            }
            else
            {
                LightThroneDMG.canDMG = false;
            }
        }
    }

    void UpdateUI(int UI_ID)
    {
        if(UI_ID == 0)
        {
            float fillB = LightBacksideHPBar.fillAmount;
            float hFraction = LightThroneHP / 2500f;

            if (fillB > hFraction)
            {
                LightHPBar.fillAmount = hFraction;
                LightBacksideHPBar.color = Color.red;
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / chipSpeed;
                LightBacksideHPBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
            }
        }

        if(UI_ID == 1)
        {
            float fillB = DarkBacksideHPBar.fillAmount;
            float hFraction = DarkThroneHP / 2500f;

            if (fillB > hFraction)
            {
                DarkHPBar.fillAmount = hFraction;
                DarkBacksideHPBar.color = Color.red;
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / chipSpeed;
                DarkBacksideHPBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
            }
        }
    }

    [PunRPC]
    void RPCThroneSys(int RPC_ID, float DMG)
    {
        if(RPC_ID == 0)
        {
            LightThroneHP -= DMG;
            lerpTimer = 0f;
        }

        if (RPC_ID == 1)
        {
            DarkThroneHP -= DMG;
            lerpTimer = 0f;
        }

        if (RPC_ID == 2)
        {
            LightWinText.SetActive(true);
        }

        if (RPC_ID == 3)
        {
            DarkWinText.SetActive(true);
        }

        if(RPC_ID == 4)
        {
            EnableMusic = true;
            LightTeamCaptured = true;
        }

        if(RPC_ID == 5)
        {
            EnableMusic = true;
            DarkTeamCaptured = true;
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(LightThroneHP);
            stream.SendNext(DarkThroneHP);
        }
        else if (stream.IsReading)
        {
            LightThroneHP = (float)stream.ReceiveNext();
            DarkThroneHP = (float)stream.ReceiveNext();
        }
    }
}
