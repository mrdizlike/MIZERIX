using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MainPoint : MonoBehaviourPun, IPunObservable
{
    public float AddTimePoint;
    public int LightPointAmount;
    public int DarkPointAmount;

    public Material[] LightPoint;
    public Material[] DarkPoint;
    public Material[] NeutralPoint;

    public Image LightPointBar;
    public Image DarkPointBar;

    public Animator Anounce;
    public GameObject LightTeamCaptured_Text;
    public GameObject DarkTeamCaptured_Text;

    public GameObject Point;

    void Update()
    {
        if(LightPointAmount == 26)
        {
            Point.GetComponent<MeshRenderer>().materials = LightPoint;
        }

        if(LightPointAmount == 0 && DarkPointAmount == 0)
        {
            Point.GetComponent<MeshRenderer>().materials = NeutralPoint;
        }

        if (DarkPointAmount == 26)
        {
            Point.GetComponent<MeshRenderer>().materials = DarkPoint;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PhotonView>().IsMine)
        {
            if (other.tag == "LightTeam")
            {
                if (LightPointAmount < 26)
                {
                    AddTimePoint += Time.deltaTime;

                    if (AddTimePoint >= 1)
                    {
                        if (DarkPointAmount == 0)
                        {
                            photonView.RPC("UpdatePointStatus", RpcTarget.All, 0);
                        }
                        else
                        {
                            photonView.RPC("UpdatePointStatus", RpcTarget.All, 1);
                        }

                        AddTimePoint = 0;
                    }

                    if (AddTimePoint == 0 && LightPointAmount == 26)
                    {
                        photonView.RPC("UpdatePointStatus", RpcTarget.All, 4);
                    }
                }
            }

            if (other.tag == "DarkTeam")
            {
                if (DarkPointAmount < 26)
                {
                    AddTimePoint += Time.deltaTime;

                    if (AddTimePoint >= 1)
                    {
                        if (LightPointAmount == 0)
                        {
                            photonView.RPC("UpdatePointStatus", RpcTarget.All, 2);
                        }
                        else
                        {
                            photonView.RPC("UpdatePointStatus", RpcTarget.All, 3);
                        }

                        AddTimePoint = 0;
                    }
                    if (AddTimePoint == 0 && DarkPointAmount == 26)
                    {
                        photonView.RPC("UpdatePointStatus", RpcTarget.All, 5);
                    }
                }
            }
        }
    }

    [PunRPC]
    void UpdatePointStatus(int TeamID)
    {
        if (TeamID == 0)
        {
            LightPointAmount += 2;
        }

        if (TeamID == 1)
        {
            DarkPointAmount -= 2;
        }

        if (TeamID == 2)
        {
            DarkPointAmount += 2;
        }

        if (TeamID == 3)
        {
            LightPointAmount -= 2;
        }

        if (TeamID == 4)
        {
            AddTimePoint = 0.1f;
            LightTeamCaptured_Text.SetActive(true);
            Anounce.gameObject.SetActive(true);
            Anounce.Play("LightTextActive");
        }

        if (TeamID == 5)
        {
            AddTimePoint = 0.1f;
            DarkTeamCaptured_Text.SetActive(true);
            Anounce.gameObject.SetActive(true);
            Anounce.Play("DarkTextActive");
        }

        LightPointBar.fillAmount = LightPointAmount / 26f;
        DarkPointBar.fillAmount = DarkPointAmount / 26f;
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(LightPointAmount);
            stream.SendNext(DarkPointAmount);
        }
        else if (stream.IsReading)
        {
            LightPointAmount = (int)stream.ReceiveNext();
            DarkPointAmount = (int)stream.ReceiveNext();
        }
    }
}
