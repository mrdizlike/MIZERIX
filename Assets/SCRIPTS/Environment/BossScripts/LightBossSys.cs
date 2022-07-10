using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Photon.Pun;

public class LightBossSys : MonoBehaviourPun, IPunObservable
{
    public BossManager BM;
    public NavMeshAgent Agent;

    public GameObject DeadParticles;
    public Transform IdlePoint;
    public Transform Player;

    public LayerMask WhatIsGround, WhatIsPlayer;

    public Vector3 WalkPoint;
    bool WalkPointSet;
    public float WalkPointRange;

    public float TimeBetweenAttacks;
    bool AlreadyAttacked;

    public float SightRange, AttackRange;
    public bool PlayerInSightRange, PlayerInAttackRange;

    public GameObject Header;
    public Image HealthBar;
    public Text HealthAmountText;
    public Animator BossAnimator;

    [Header("BossStat")]
    public float HealthAmount;
    public float AttackDMG;
    float HealthRegenTime;
    bool isDead;

    void Start()
    {
        BM = FindObjectOfType<BossManager>();
        IdlePoint = GameObject.Find("LightBoss_IdlePoint").transform;
    }

    private void Update()
    {
        if (!isDead)
        {
            if (Player != null)
            {
                float distancefromTarget = Vector3.Distance(Player.transform.position, transform.position);
                float distanceFromIdlePoint = Vector3.Distance(IdlePoint.position, transform.position);

                if (distancefromTarget < SightRange)
                {
                    PlayerInSightRange = true;
                }
                else
                {
                    PlayerInSightRange = false;
                }

                if (distancefromTarget < AttackRange)
                {
                    PlayerInAttackRange = true;
                }
                else
                {
                    PlayerInAttackRange = false;
                }

                if (distanceFromIdlePoint > 3)
                {
                    photonView.RPC("LightBossAction", RpcTarget.All, 5);
                }
            }
            BossMovementSys();
        }
        BossHealthSys();
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

        if(HealthAmount <= 0)
        {
            HealthAmount = 0;
            isDead = true;
        }

        if (isDead)
        {
            photonView.RPC("LightBossAction", RpcTarget.All, 3);
        }
    }

    void BossMovementSys()
    {
        if(Player == null)
        {
            Agent.SetDestination(IdlePoint.position);
            HealthAmount = 5000;
        }

        if (Player != null)
        {
            if (!PlayerInSightRange && !PlayerInAttackRange)
            {
                LightBossAction(0);

                if (WalkPointSet)
                {
                    Agent.SetDestination(WalkPoint);
                }

                Vector3 DistanceToWalkPoint = transform.position - WalkPoint;

                if (DistanceToWalkPoint.magnitude < 1f)
                {
                    WalkPointSet = false;
                    BossAnimator.Play("Idle");
                    Player = null;
                }
            }

            if (PlayerInSightRange && !PlayerInAttackRange)
            {
                photonView.RPC("LightBossAction", RpcTarget.All, 1);
            }

            if (PlayerInSightRange && PlayerInAttackRange)
            {
                photonView.RPC("LightBossAction", RpcTarget.All, 2);
            }
        }
    }

    [PunRPC]
    void LightBossTakeDamage(float dmg)
    {
        if (Player != null && !isDead)
        {
            HealthAmount -= dmg;
        }
    }

    [PunRPC]
    public void LightBossAction(int Action_ID)
    {
        if(Action_ID == 0) // ������ �� �����, ���� �� �����
        {
            WalkPoint = new Vector3(IdlePoint.position.x, IdlePoint.position.y, IdlePoint.position.z);
            Agent.SetDestination(IdlePoint.position);

            if (Physics.Raycast(WalkPoint, -transform.up, 2f, WhatIsGround))
            {
                WalkPointSet = true;
            }
        }

        if (Action_ID == 1 && Player != null) // ������ �� ��������� �������
        {
            Agent.SetDestination(Player.position);
            
        }

        if (Action_ID == 2) // ������ ������
        {
            Agent.SetDestination(transform.position);

            transform.LookAt(Player);

            if (!AlreadyAttacked)
            {
                 int SuperAttackChance = Random.Range(0, 100);
                 if (SuperAttackChance < 75)
                 {
                    Player.GetComponent<PhotonView>().RPC("ReceiveDMG", RpcTarget.All, AttackDMG);
                    BossAnimator.Play("MainAttack");
                 }
                 if (SuperAttackChance > 75)
                 {
                    Player.GetComponent<PhotonView>().RPC("ReceiveDMG", RpcTarget.All, AttackDMG * 2);
                    BossAnimator.Play("PowerAttack");
                 }

                 Player.GetComponent<Player_MAIN>().Hater = gameObject;

                 AlreadyAttacked = true;
                 Invoke(nameof(ResetAttack), TimeBetweenAttacks);
            }
        }

        if(Action_ID == 3) //������
        {
            BossAnimator.Play("LightBoss_Dead");
            Header.SetActive(false);
            transform.tag = "Untagged";
            Agent.isStopped = true;
        }

        if(Action_ID == 4) //����������� ������, ��������� �������� �����������
        {
            if(Player.tag == "LightTeam")
            {
                PlayerSTAT[] Players = FindObjectsOfType<PlayerSTAT>();

                Player.GetComponent<Player_MAIN>().KF.AddNewKillListing("Light team", "Purified");
                foreach (PlayerSTAT PS in Players)
                {
                    if(PS.gameObject.tag == "LightTeam")
                    {
                        PS.Debuffs.Add(BuffList.LightBossBuff);
                        PS.EnableDeBuff = true;
                    }
                }
            }
            if (Player.tag == "DarkTeam")
            {
                PlayerSTAT[] Players = FindObjectsOfType<PlayerSTAT>();

                Player.GetComponent<Player_MAIN>().KF.AddNewKillListing("Dark team", "Purified");
                foreach (PlayerSTAT PS in Players)
                {
                    if(PS.gameObject.tag == "DarkTeam")
                    {
                        PS.Debuffs.Add(BuffList.LightBossBuff);
                        PS.EnableDeBuff = true;
                    }
                }
            }
            BM.LightBossIsDead = true;
            Instantiate(DeadParticles);
            PhotonNetwork.Destroy(gameObject);
        }

        if(Action_ID == 5)
        {
            Player = null;
        }
    }

    public void Death()
    {
        photonView.RPC("LightBossAction", RpcTarget.All, 4);
    }

    void ResetAttack()
    {
        AlreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, SightRange);
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
