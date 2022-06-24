using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SoundScript : MonoBehaviour
{
    public PhotonView Player_PV;

    public AudioSource Weapon_Aux;

    public AudioClip Weapon_Inspect;
    public AudioClip Weapon_PullOut;
    public AudioClip Weapon_MagOut;
    public AudioClip Weapon_MagIn;
    public AudioClip Weapon_Charge;
    public AudioClip HitPlayer;
    public AudioClip DeathPlayerHit;


    public void Inspect()
    {
        if (Player_PV.IsMine)
        {
            Weapon_Aux.PlayOneShot(Weapon_Inspect);
        }
    }
    public void PullOut()
    {
        if (Player_PV.IsMine)
        {
            Weapon_Aux.PlayOneShot(Weapon_PullOut);
        }
    }
    public void MagOut()
    {
        if (Player_PV.IsMine)
        {
            Weapon_Aux.PlayOneShot(Weapon_MagOut);
        }
    }

    public void MagIn()
    {
        if (Player_PV.IsMine)
        {
            Weapon_Aux.PlayOneShot(Weapon_MagIn);
        }
    }

    public void Charge()
    {
        if (Player_PV.IsMine)
        {
            Weapon_Aux.PlayOneShot(Weapon_Charge);
        }
    }

    public void HitCheck()
    {
        if (Player_PV.IsMine)
        {
            Weapon_Aux.PlayOneShot(HitPlayer);
        }
    }
    public void DeathHitCheck()
    {
        if (Player_PV.IsMine)
        {
            Weapon_Aux.PlayOneShot(DeathPlayerHit);
        }
    }
}
