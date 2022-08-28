using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BossManager : MonoBehaviourPun, IPunObservable
{
    public Transform LightBossSpawnPoint;
    public Transform DarkBossSpawnPoint;

    public GameObject LightBossPrefab;
    public GameObject DarkBossPrefab;

    public bool LightBossIsDead;
    public bool DarkBossIsDead;

    public float LightBossSpawnTime;
    public float DarkBossSpawnTime;

    private void Update()
    {
        if (LightBossIsDead)
        {
            LightBossSpawnTime += Time.deltaTime;

            if(LightBossSpawnTime >= 1)
            {
                LightBossSpawnTime = 0;
                LightBossIsDead = false;
                if(photonView.IsMine)
                {
                    PhotonNetwork.Instantiate(LightBossPrefab.name, LightBossSpawnPoint.position, LightBossSpawnPoint.rotation).name = "LightBoss";
                }
            }
        }

        if (DarkBossIsDead)
        {
            DarkBossSpawnTime += Time.deltaTime;

            if (DarkBossSpawnTime >= 300)
            {
                DarkBossSpawnTime = 0;
                DarkBossIsDead = false;
                if(photonView.IsMine)
                {
                    PhotonNetwork.Instantiate(DarkBossPrefab.name, DarkBossSpawnPoint.position, DarkBossSpawnPoint.rotation).name = "DarkBoss";
                }
            }
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(LightBossIsDead);
            stream.SendNext(DarkBossIsDead);
            stream.SendNext(LightBossSpawnTime);
            stream.SendNext(DarkBossSpawnTime);
        }
        else if (stream.IsReading)
        {
            LightBossIsDead = (bool)stream.ReceiveNext();
            DarkBossIsDead = (bool)stream.ReceiveNext();
            LightBossSpawnTime = (float)stream.ReceiveNext();
            DarkBossSpawnTime = (float)stream.ReceiveNext();
        }
    }
}
