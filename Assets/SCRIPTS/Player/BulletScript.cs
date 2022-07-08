using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class BulletScript : MonoBehaviour
{
    public Rigidbody RB;
    public GameObject explosion;
    public List<Buff> Buff;
    public PhotonView Photon_Player;
    public LayerMask Enemy;

    public float ExplosionDamage;
    public float ExplosionRange;

    public int MaxCollision;
    public float MaxLifeTime;
    public bool ExplodeOnTouch;

    int Collisions_Amount;
    PhysicMaterial Physic_Material;


    private void Start()
    {
        Setup();
    }

    private void Update()
    {
        if (Collisions_Amount > MaxCollision)
        {
            Explode();
        }

        MaxLifeTime -= Time.deltaTime;
        if(MaxLifeTime <= 0)
        {
            Explode();
        }
    }

    private void Explode()
    {
        PhotonNetwork.Instantiate(explosion.name, transform.position, Quaternion.identity);
        PhotonNetwork.Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {        
        LightTeamDMG(other);
        DarkTeamDMG(other);

        LightBossDMG(other);
        DarkBossDMG(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            Explode();
        }
    }
    private void Setup()
    {
        Physic_Material = new PhysicMaterial();
        Physic_Material.frictionCombine = PhysicMaterialCombine.Minimum;
        Physic_Material.bounceCombine = PhysicMaterialCombine.Maximum;

        GetComponent<SphereCollider>().material = Physic_Material;
    }


    private void LightTeamDMG(Collider other)
    {
        if (Photon_Player.tag == "LightTeam" && other.tag == "DarkTeam" && ExplodeOnTouch && other.GetComponent<PhotonView>().ViewID != Photon_Player.ViewID) 
        {
            other.GetComponent<Player_MAIN>().Hater = Photon_Player.gameObject;
            int CriticalAttackChance = Random.Range(0, 99);

            if (Photon_Player.GetComponent<PlayerSTAT>().DMGCrit_Amount == 0)
            {
                other.GetComponent<PhotonView>().RPC("ReceiveDMG", RpcTarget.All, ExplosionDamage);
                Photon_Player.GetComponent<GunScript>().DmgTextRelease(ExplosionDamage);
                Photon_Player.GetComponent<GunScript>().SoundScript.HitCheck();
                Explode();
            }
            else if (CriticalAttackChance <= Photon_Player.GetComponent<PlayerSTAT>().DMGCrit_Amount) //����������� �����
            {
                Debug.Log("Critical attack!");
                other.GetComponent<PhotonView>().RPC("ReceiveDMG", RpcTarget.All, ExplosionDamage * 2);
                Photon_Player.GetComponent<GunScript>().DmgTextRelease(ExplosionDamage * 2);
                Photon_Player.GetComponent<GunScript>().SoundScript.HitCheck(); // ���� �����
                Explode();
            }

        }

        if (Photon_Player.tag == "LightTeam" && other.tag == "DarkTeam")
        {
            other.GetComponent<Player_MAIN>().Hater = Photon_Player.gameObject;
            int ElectricCrownChance = Random.Range(0, 99);

            foreach (Buff i in Buff)
            {
                other.GetComponent<PlayerSTAT>().Debuffs.Add(i);
                other.GetComponent<PlayerSTAT>().EnableDeBuff = true;

                if (Buff.Contains(BuffList.ElectricCrownBuff) && ElectricCrownChance <= 20)
                {
                    other.GetComponent<PhotonView>().RPC("ItemEffect", RpcTarget.All, 116, false);
                }

                if (Buff.Contains(BuffList.BleedDebuff) || Buff.Contains(BuffList.SectantBookBuff))
                {
                    Photon_Player.GetComponent<PlayerSTAT>().HP_Amount += 10;
                }
            }
        }


        if (Photon_Player.tag == "LightTeam" && other.tag == "Throne")
        {
            other.GetComponent<ThroneDamage>().TakeDamageDark(ExplosionDamage);

            Photon_Player.GetComponent<GunScript>().DmgTextRelease(ExplosionDamage);
            Photon_Player.GetComponent<GunScript>().SoundScript.HitCheck();
            Explode();
        }

        if (Photon_Player.tag == "LightTeam" && other.tag == "DarkTeam" && other.GetComponent<PlayerSTAT>().Buffs.Contains(BuffList.SpikeBuff))
        {
            Photon_Player.GetComponent<Player_MAIN>().Hater = other.gameObject;
            Photon_Player.GetComponent<PhotonView>().RPC("ReceiveDMG", RpcTarget.All, ExplosionDamage / 2);
        }
    }

    private void DarkTeamDMG(Collider other)
    {
        if (Photon_Player.tag == "DarkTeam" && other.tag == "LightTeam" && ExplodeOnTouch && other.GetComponent<PhotonView>().ViewID != Photon_Player.ViewID) 
        {
            other.GetComponent<Player_MAIN>().Hater = Photon_Player.gameObject;
            int CriticalAttackChance = Random.Range(0, 99);

            if (Photon_Player.GetComponent<PlayerSTAT>().DMGCrit_Amount == 0)
            {
                other.GetComponent<PhotonView>().RPC("ReceiveDMG", RpcTarget.All, ExplosionDamage);
                Photon_Player.GetComponent<GunScript>().DmgTextRelease(ExplosionDamage);
                Photon_Player.GetComponent<GunScript>().SoundScript.HitCheck();
                Explode();
            }
            else if (CriticalAttackChance <= Photon_Player.GetComponent<PlayerSTAT>().DMGCrit_Amount) //����������� �����
            {
                Debug.Log("Critical attack!");
                other.GetComponent<PhotonView>().RPC("ReceiveDMG", RpcTarget.All, ExplosionDamage * 2);
                Photon_Player.GetComponent<GunScript>().DmgTextRelease(ExplosionDamage * 2);
                Photon_Player.GetComponent<GunScript>().SoundScript.HitCheck(); // ���� �����
                Explode();
            }

        }

        if (Photon_Player.tag == "DarkTeam" && other.tag == "LightTeam")
        {
            other.GetComponent<Player_MAIN>().Hater = Photon_Player.gameObject;
            int ElectricCrownChance = Random.Range(0, 99);

            foreach (Buff i in Buff)
            {
                other.GetComponent<PlayerSTAT>().Debuffs.Add(i);
                other.GetComponent<PlayerSTAT>().EnableDeBuff = true;

                if (Buff.Contains(BuffList.ElectricCrownBuff) && ElectricCrownChance <= 20)
                {
                    other.GetComponent<PhotonView>().RPC("ItemEffect", RpcTarget.All, 116, false);
                }

                if (Buff.Contains(BuffList.BleedDebuff) || Buff.Contains(BuffList.SectantBookBuff))
                {
                    Photon_Player.GetComponent<PlayerSTAT>().HP_Amount += 10;
                }
            }
        }

        if (Photon_Player.tag == "DarkTeam" && other.tag == "Throne")
        {
            other.GetComponent<ThroneDamage>().TakeDamageLight(ExplosionDamage);

            Photon_Player.GetComponent<GunScript>().DmgTextRelease(ExplosionDamage);
            Photon_Player.GetComponent<GunScript>().SoundScript.HitCheck();
            Explode();
        }

        if (Photon_Player.tag == "DarkTeam" && other.tag == "LightTeam" && other.GetComponent<PlayerSTAT>().Buffs.Contains(BuffList.SpikeBuff))
        {
            Photon_Player.GetComponent<Player_MAIN>().Hater = other.gameObject;
            Photon_Player.GetComponent<PhotonView>().RPC("ReceiveDMG", RpcTarget.All, ExplosionDamage / 2);
        }
    }

    private void LightBossDMG(Collider other)
    {
        if(other.tag == "LightBoss")
        {
            if(other.GetComponent<LightBossSys>().Player == null)
            {
                other.GetComponent<LightBossSys>().Player = Photon_Player.transform;
            }
            other.GetComponent<PhotonView>().RPC("LightBossTakeDamage", RpcTarget.All, ExplosionDamage);
        }
    }

    private void DarkBossDMG(Collider other)
    {
        if(other.tag == "DarkBoss" && Photon_Player.GetComponent<Player_MAIN>().BossZone)
        {
            if(other.GetComponent<DarkBossSys>().Target == null)
            {
                other.GetComponent<DarkBossSys>().Target = Photon_Player.gameObject;
            }
            other.GetComponent<PhotonView>().RPC("DarkBossTakeDamage", RpcTarget.All, ExplosionDamage);
            other.GetComponent<DarkBossSys>().InSafeTimer = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ExplosionRange);
    }
}
