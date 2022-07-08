using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DarkBossSys : MonoBehaviourPun, IPunObservable
{
    [Header("Main Options")]
    public BossManager BM;
    public ConnectionSystem CS;
    public float HealthAmount;
    public float HealthRegenTime;
    public int AttackSkill;
    public float RandomSkillTimer;
    public float InSafeTimer;
    public bool isDead;

    public GameObject Target;

    public Animator MainAnimator;

    [Header("Skill1")]
    public float SkillTime;
    public float TimeToShoot;
    public GameObject BulletSpawner;
    public bool Skill1_Anim_BackToSide;
    public bool Skill1_Anim_Rotation;
    float SpawnBulletTime;

    [Header("Prefabs")]
    public GameObject Skill1_Prefab;
    public GameObject Skill2_Prefab;
    public GameObject Skill3_Prefab;

    [Header("UI")]
    public GameObject Header;
    public Image HealthBar;
    public Text HealthAmountText;
    public Animator BossAnimator;

    void Start()
    {
        BM = FindObjectOfType<BossManager>();
        CS = FindObjectOfType<ConnectionSystem>();
    }

    void Update()
    {
        BossHealthSys();

        if(Target != null && !isDead)
        {
            RandomSkillTimer += Time.deltaTime;
            InSafeTimer += Time.deltaTime;

            if(RandomSkillTimer >= 2 && AttackSkill == 0)
            {
                AttackSkill = Random.Range(1,4);
                RandomSkillTimer = 0;
            }

            if(InSafeTimer > 5)
            {
                Target = null;
                AttackSkill = 0;
                MainAnimator.Play("Idle1");
                InSafeTimer = 0;
            }
        }

        if(AttackSkill == 1)
        {
            photonView.RPC("NetworkSkill", RpcTarget.All, 1);
        }

        if(AttackSkill == 2)
        {
            photonView.RPC("NetworkSkill", RpcTarget.All, 2);
        }

        if(AttackSkill == 3)
        {
            photonView.RPC("NetworkSkill", RpcTarget.All, 3);
        }
    }

    void BossHealthSys()
    {
        HealthBar.fillAmount = HealthAmount / 5000f;
        HealthAmountText.text = HealthAmount.ToString();

        if (HealthAmount < 5000 && !isDead)
        {
            HealthRegenTime += Time.deltaTime;

            if (HealthRegenTime > 1f)
            {
                HealthAmount += 150;
                HealthRegenTime = 0;               
            }
        }

        if(HealthAmount > 5000)
        {
            HealthAmount = 5000;
        }

        if(HealthAmount <= 0 && !isDead)
        {
            HealthAmount = 1;
            MainAnimator.Play("DarkBoss_Dead");
            isDead = true;
        }

    }

    void DarkBossSkill1(int SkillSet)
    {
        SpawnBulletTime += Time.deltaTime;
        SkillTime += Time.deltaTime;

        if(SkillSet == 1 && SkillTime < 0.1f)
        {
            Skill1_Anim_BackToSide = true;
        }

        if(SkillSet == 2 && SkillTime < 0.1f)
        {
            Skill1_Anim_Rotation = true;
        }

        if(SpawnBulletTime > TimeToShoot)
        {
            PhotonNetwork.Instantiate(Skill1_Prefab.name, BulletSpawner.transform.position, BulletSpawner.transform.rotation.normalized).GetComponent<DarkBossBulletScript>().DarkBoss_Prefab = gameObject;
            SpawnBulletTime = 0;
        }

        if(Skill1_Anim_BackToSide)
        {
            TimeToShoot = 0.2f;
            BulletSpawner.GetComponent<Animator>().Play("Skill1");
            MainAnimator.Play("DarkBoss_Skill1(BackToSide)");
            Skill1_Anim_BackToSide = false;
        }

         if(Skill1_Anim_Rotation)
        {
            TimeToShoot = 0f;
            BulletSpawner.GetComponent<Animator>().Play("Skill1(Rotate)");
            MainAnimator.Play("DarkBoss_Skill1(Rotate)");
            Skill1_Anim_Rotation = false;
        }

        if(SkillTime > 3)
        {
            AttackSkill = 0;
            SkillTime = 0;
            MainAnimator.Play("Idle1");
        }
    }

    void DarkBossSkill2()
    {
        Skill2_Prefab.SetActive(true);
        MainAnimator.Play("DarkBoss_Skill2");
        AttackSkill = 0;
    }

    void DarkBossSkill3()
    {
        Skill3_Prefab.SetActive(true);
        MainAnimator.Play("DarkBoss_Skill3");
        MainAnimator.Play("Skill3");
        AttackSkill = 0;
    }

    [PunRPC]
    void NetworkSkill(int Skill_ID)
    {
         if(Skill_ID == 1)
        {
            DarkBossSkill1(Random.Range(1,3));
        }

        if(Skill_ID == 2)
        {
            DarkBossSkill2();
        }

        if(Skill_ID == 3)
        {
            DarkBossSkill3();
        }

        if(Skill_ID == 4)
        {
            if(Target.tag == "LightTeam")
            {
                Target.GetComponent<Player_MAIN>().KF.AddNewKillListing("Light team", "Summoner");
                foreach(GameObject PS in CS.PlayersObjects)
                {
                    if(PS.tag == "LightTeam")
                    {
                      PS.GetComponent<PlayerSTAT>().Debuffs.Add(BuffList.DarkBossBuff);
                      PS.GetComponent<PlayerSTAT>().EnableDeBuff = true;
                    }
                }
            }
            if (Target.tag == "DarkTeam")
            {
                Target.GetComponent<Player_MAIN>().KF.AddNewKillListing("Dark team", "Summoner");
                foreach(GameObject PS in CS.PlayersObjects)
                {
                    if(PS.tag == "DarkTeam")
                    {
                      PS.GetComponent<PlayerSTAT>().Debuffs.Add(BuffList.DarkBossBuff);
                      PS.GetComponent<PlayerSTAT>().EnableDeBuff = true;
                    }
                }
            }

            BM.DarkBossIsDead = true;
            Header.SetActive(false);   
        }
    }

    [PunRPC]
    void DarkBossTakeDamage(float dmg)
    {
        if (Target != null && !isDead)
        {
            HealthAmount -= dmg;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(HealthAmount);
        }
        else if (stream.IsReading)
        {
            HealthAmount = (float)stream.ReceiveNext();
        }
    }
}
