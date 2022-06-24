using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceSoundScript : MonoBehaviour
{
    public AudioSource AFX;

    public AudioClip BuyItem_Sound;
    public AudioClip SellItem_Sound;
    public AudioClip SelectItem_Sound;
    public AudioClip ChangeItem_Sound;

    public void BuyItem()
    {
        AFX.PlayOneShot(BuyItem_Sound);
    }

    public void SellItem()
    {
        AFX.PlayOneShot(SellItem_Sound);
    }

    public void SelectItem()
    {
        AFX.PlayOneShot(SelectItem_Sound);
    }

    public void ChangeItem()
    {
        AFX.PlayOneShot(ChangeItem_Sound);
    }
}
