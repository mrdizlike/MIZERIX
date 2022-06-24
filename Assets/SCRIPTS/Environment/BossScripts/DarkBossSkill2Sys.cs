using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DarkBossSkill2Sys : MonoBehaviour
{
    public float LifeTime;

    void Update()
    {
        LifeTime += Time.deltaTime;

        if(LifeTime >= 2)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "LightTeam" || other.tag == "DarkTeam")
        {
            other.GetComponent<PhotonView>().RPC("ReceiveDMG", RpcTarget.All, 35f);
        }
    }
}
