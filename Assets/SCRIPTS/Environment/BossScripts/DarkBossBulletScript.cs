using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DarkBossBulletScript : MonoBehaviour
{

public GameObject DarkBoss_Prefab;
public Rigidbody RB;

public float speed;

float DestroyTime;

    void Update()
    {
        DestroyTime += Time.deltaTime;

        Vector3 forward = RB.transform.forward;
        RB.AddForce(forward * speed, ForceMode.Impulse);

        if(DestroyTime > 1)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {        
        if(other.tag == "LightTeam" || other.tag == "DarkTeam" && GetComponent<PhotonView>().IsMine)
        {
            other.GetComponent<PhotonView>().RPC("ReceiveDMG", RpcTarget.All, 60f);
            other.GetComponent<Player_MAIN>().Hater = DarkBoss_Prefab;
            Destroy(gameObject);
        }
    }
}
