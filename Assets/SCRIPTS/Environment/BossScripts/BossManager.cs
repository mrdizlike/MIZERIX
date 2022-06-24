using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BossManager : MonoBehaviour
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

            if(LightBossSpawnTime >= 300)
            {
                LightBossSpawnTime = 0;
                LightBossIsDead = false;
                PhotonNetwork.Instantiate(LightBossPrefab.name, LightBossSpawnPoint.position, LightBossSpawnPoint.rotation);
            }
        }

        if (DarkBossIsDead)
        {
            DarkBossSpawnTime += Time.deltaTime;

            if (DarkBossSpawnTime >= 300)
            {
                DarkBossSpawnTime = 0;
                DarkBossIsDead = false;
                PhotonNetwork.Instantiate(DarkBossPrefab.name, DarkBossSpawnPoint.position, DarkBossSpawnPoint.rotation);
            }
        }
    }
}
