using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ThroneDamage : MonoBehaviour
{
    public ThroneSystem TS;

    public bool canDMG;

    public void TakeDamageLight(float DMG)
    {
        if (canDMG)
        {
            TS.photonView.RPC("RPCThroneSys", RpcTarget.All, 0, DMG);
        }
    }

    public void TakeDamageDark(float DMG)
    {
        if (canDMG)
        {
            TS.photonView.RPC("RPCThroneSys", RpcTarget.All, 1, DMG);
        }
    }

}
