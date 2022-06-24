using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerNetworkAnim : MonoBehaviourPun, IPunObservable
{
    [Header("WeaponsAndItems")]
    public GameObject Weapon;
    public Transform Camera;
    [Header("UI")]
    public GameObject PlayerUI;

    void Start()
    {
        NetworkDisabler();
    }

    void NetworkDisabler()
    {
        if (!photonView.IsMine)
        {
            Weapon.layer = 0;
            PlayerUI.SetActive(false);
        }
    }
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {

        }
        else if (stream.IsReading)
        {

        }
    }
}
