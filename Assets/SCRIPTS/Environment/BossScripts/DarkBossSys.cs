using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DarkBossSys : MonoBehaviourPun, IPunObservable
{
    [Header("Main Options")]
    public float HealthAmount;
    public float HealthRegenTime;
    public int AttackSkill;
    public bool isDead;

    public GameObject Target;

    [Header("Prefabs")]
    public GameObject Skill1_Prefab;
    public GameObject Skill2_Prefab;
    public GameObject Skill3_Prefab;

    [Header("UI")]
    public GameObject Header;
    public Image HealthBar;
    public Text HealthAmountText;
    public Animator BossAnimator;

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

    void DarkBossSkill1()
    {
        
    }

    void DarkBossSkill2()
    {
        StartCoroutine(SendMissle(Skill2_Prefab));
    }

    public IEnumerator SendMissle(GameObject missle)
    {
        while(Vector3.Distance(Target.transform.position, missle.transform.position) > 0.3f)
        {
            missle.transform.position += (Target.transform.position - missle.transform.position).normalized * 0.5f * Time.deltaTime;
            missle.transform.LookAt(Target.transform);
            yield return null;
        }
        Destroy(missle);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            
        }
        else if (stream.IsReading)
        {
            
        }
    }

}
