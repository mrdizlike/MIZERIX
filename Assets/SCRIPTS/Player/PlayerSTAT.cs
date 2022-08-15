using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerSTAT : MonoBehaviourPun, IPunObservable
{
    [Header("MainOptions")]
    public float HP_Amount;
    public float HP_MaxAmount;
    public float HPRegen_Amount;
    private float HPRegen_Time;
    public float Armor_Amount;
    public float DMG_Amount;
    public float DMGCrit_Amount;

    public bool EnableDeBuff;

    public int KillsCount;
    public int DeathCount;
    public int TokenCount;
    public int SwapTokenCount;

    [Header("Buffs")]
    public List<Buff> Buffs = new List<Buff>();
    public List<Buff> Debuffs = new List<Buff>();

    public ItemList IL;
    public ShopScript SS;

    [Header("Dead_System")]
    public bool Dead;
    public float RespawnTime;
    public GameObject DeadPlayer_Camera;
    public GameObject Player_Camera;
    public GameObject Player_Model;
    public GameObject Effect_Model;
    public GameObject Player_Weapon;
    public GameObject MainHeader_UI;
    public GameObject ColliderSys;
    public Collider ColliderTrigger;
    public CharacterController CharController;
    public Text TimeForRespawn;

    [Header("LVL_System")]
    public GameObject LVL_Particles;
    public bool NextLevel;
    public int Level_Amount;
    public int SwitchItem_Amount;
    public float EXP_Amount;
    public float MaxEXP;
    private float TimeToReceiveEXP;

    public GunScript GS;
    public Player_MAIN PM;
    public SkillManager SM;

    [Header("UI_Elements")]
    public ItemUI GUI_Effects;
    public GameObject DamagedScreen;
    public Image HP_Bar;
    public Image HP_Backward_Bar;
    public Image EXP_Bar;
    public Image HP_Player_Bar; // �� ��� ����� ��� ������
    public Text SafeZone;
    public Text HP_Text;
    public Text LVL_text;
    public Text HP_Player_Text; // �� ��� ����� ��� ������
    public Text LVL_Player_Text; // �� ��� ����� ��� ������
    public GameObject ClassInfo;
    public Text Kills_Text;
    public Text Deaths_Text;
    public Text MaxHPStat_Text;
    public Text MaxDMGStat_Text;
    public Text MaxArmorStat_Text;

    float lerpTimer;
    public float chipSpeed;
    private float DamageScreenTimer;

    void Start()
    {
        if(gameObject.tag == "LightTeam")
        {
            HP_Player_Bar.color = new Color(0, 212, 255);
        }

        if (gameObject.tag == "DarkTeam")
        {
            HP_Player_Bar.color = new Color(95, 0, 255);
        }
    }
    void Update()
    {
        HealthSys();
        UpdateHealthBarUI();
        LevelSys();
        ItemSys();
        StatSys();

        ReceiveEXP(0);

        if (Input.GetKeyDown(KeyCode.U))
        {
            ReceiveDMG(10);
        }
    }

    public void AddStats(Item item)
    {
        if (photonView.IsMine)
        {
            HP_Amount += item.HP_Amount;
            HP_MaxAmount += item.HP_MaxAmount;
            HPRegen_Amount += item.HPRegen_Amount;
            Armor_Amount += item.Armor_Amount;
            DMG_Amount += item.DMG_Amount;
            DMGCrit_Amount += item.DMGCrit_Amount;
            GS.ReloadTime -= item.RealodTime;
            GS.Spread += item.Spread;
            GS.TimeBetweenShooting -= item.ShootRate;
            GS.MagazineSize += item.MagazineSize;
            PM.MovementSpeed += item.MovementSpeed;
            PM.PreviousMovement = PM.MovementSpeed;
            GS.BulletsInMagazine_Text.text = GS.MagazineSize.ToString();
            GS.BulletsLeft = GS.MagazineSize;          

            photonView.RPC("AddBuff", RpcTarget.All, item.ItemName);
        }
    }

    public void RemoveStats(Item item)
    {
        if (photonView.IsMine)
        {
            HP_Amount -= item.HP_Amount;
            HP_MaxAmount -= item.HP_MaxAmount;
            HPRegen_Amount -= item.HPRegen_Amount;
            Armor_Amount -= item.Armor_Amount;
            DMG_Amount -= item.DMG_Amount;
            DMGCrit_Amount -= item.DMGCrit_Amount;
            GS.ReloadTime += item.RealodTime;
            GS.Spread -= item.Spread;
            GS.TimeBetweenShooting += item.ShootRate;
            GS.MagazineSize -= item.MagazineSize;
            GS.BulletsInMagazine_Text.text = GS.MagazineSize.ToString();
            GS.BulletsLeft = GS.MagazineSize;
            PM.MovementSpeed -= item.MovementSpeed;
            PM.PreviousMovement = PM.MovementSpeed;

            photonView.RPC("RemoveBuff", RpcTarget.All, item.ItemName);
        }
    }

    [PunRPC]
    public void AddBuff(string ItemName)
    {
        foreach(Item i in IL.ItemResult)
        {
            if(i.ItemName == ItemName)
            {
                if (i.SomeBuff.BuffType != Buff.BuffID.No_Buff)
                {
                    Buffs.Add(i.SomeBuff);
                }
                break;
            }
        }

    }

    [PunRPC]
    public void AddDeBuff(string ItemName)
    {
        foreach (Item i in IL.ItemResult)
        {
            if (i.ItemName == ItemName)
            {
                if (i.SomeBuff.BuffType != Buff.BuffID.No_Buff)
                {
                    Debuffs.Add(i.SomeBuff);
                }
                break;
            }
        }

    }

    [PunRPC]
    public void RemoveBuff(string ItemName)
    {
        foreach (Item i in IL.ItemResult)
        {
            if (i.ItemName == ItemName)
            {
                if (i.SomeBuff.BuffType != Buff.BuffID.No_Buff)
                {
                    Buffs.Remove(i.SomeBuff);
                }
                break;
            }

            if (ItemName == "InfectedSkull") //InfectedSkull �����
            {
                HPRegen_Amount -= GetComponent<ItemSysScript>().InfectedSkull_NeedCount - 2;
                GetComponent<ItemSysScript>().InfectedSkull_NeedCount = 2;
                GetComponent<ItemSysScript>().InfectedSkull_Count = 0;
            }
        }
    }

    public void HealthSys()
    {
        HP_Text.text = HP_Amount.ToString("f0");
        HP_Bar.fillAmount = HP_Amount / HP_MaxAmount;

        HP_Player_Text.text = HP_Amount.ToString("f0");
        HP_Player_Bar.fillAmount = HP_Amount / HP_MaxAmount;

        if (HP_Amount > HP_MaxAmount)
        {
            HP_Amount = HP_MaxAmount;
        }

        if (HP_Amount < 0)
        {
            HP_Amount = 0;
        }

        if (HP_Amount <= 0 && !Dead)
        {
            photonView.RPC("PlayerDeadAndRespawn", RpcTarget.All, 0); //������

            foreach (Slot slot in GetComponent<Inventory>().slots)
            {
                if (Buffs.Contains(BuffList.CronerChainBuff))
                {
                    GetComponent<ItemSysScript>().EritondEye_DeathCount += 1;
                }

                if (Buffs.Contains(BuffList.InfectedSkullBuff))
                {
                    GetComponent<ItemSysScript>().InfectedSkull_Count -= 2;
                    HPRegen_Amount -= 2;
                    GetComponent<ItemSysScript>().InfectedSkull_NeedCount -= 2;
                }

                if (GetComponent<ItemSysScript>().EritondEye_DeathCount == 2)
                {
                    if (slot.item.NewItemID == 2322)
                    {
                        SS.SellRPCButton(slot.SlotIndex);
                        GetComponent<ItemSysScript>().EritondEye_DeathCount = 0;
                    }
                }
            }
        }

        if (Dead && photonView.IsMine)
        {
            HP_Amount = 0;
            GetComponent<Look>().cam = DeadPlayer_Camera.GetComponent<Camera>();
            DeadPlayer_Camera.SetActive(true);

            RespawnTime -= Time.deltaTime;
            TimeForRespawn.text = ((int)RespawnTime).ToString();
            if (RespawnTime <= 0)
            {
                photonView.RPC("PlayerDeadAndRespawn", RpcTarget.All, 2); //�����������
                RespawnTime = 5;

            }
        }

        if (HPRegen_Amount > 0) //����������� ��
        {
            HPRegen_Time += Time.deltaTime;

            if (HPRegen_Time >= 1)
            {
                HP_Amount += HPRegen_Amount;
                HPRegen_Time = 0;
            }
        }

        if(HPRegen_Amount < 0)
        {
            HPRegen_Amount = 0;
        }

        if (GetComponent<Player_MAIN>().SafeZone && HP_Amount < HP_MaxAmount) //����������� �� � ���������� ����
        {
            HPRegen_Time += Time.deltaTime;
            if (HPRegen_Time >= 1)
            {
                HP_Amount += 20;
                HPRegen_Time = 0;
            }
        }

        if (Buffs.Contains(BuffList.MaskWithTubesBuff) && EnableDeBuff)
        {
            if (!Debuffs.Contains(null))
            {
                foreach (Buff i in Debuffs)
                {
                    if (i.NegativeEffect)
                    {
                        i.Time--;
                    }
                }
            }
        }

        if (Buffs.Contains(BuffList.BrainModBuff) && EnableDeBuff)
        {
            if (!Debuffs.Contains(null))
            {
                foreach (Buff i in Debuffs)
                {
                    if (!i.NegativeEffect)
                    {
                        i.Time += 2;
                    }
                }
            }
        }

        if(DamagedScreen.activeSelf)
        {
            DamageScreenTimer += Time.deltaTime;
            if(DamageScreenTimer > 1f)
            {
                DamagedScreen.SetActive(false);
                DamageScreenTimer = 0;
            }
        }
    }

    void UpdateHealthBarUI()
    {
        float fillB = HP_Backward_Bar.fillAmount;
        float hFraction = HP_Amount / HP_MaxAmount;

        if (fillB > hFraction)
        {
            HP_Bar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            HP_Backward_Bar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
    }

    void LevelSys()
    {
        if(Level_Amount < 11)
        {
            EXP_Bar.fillAmount = EXP_Amount / MaxEXP;
            LVL_text.text = Level_Amount.ToString();
            LVL_Player_Text.text = Level_Amount.ToString();

            if (EXP_Amount >= MaxEXP)
            {
                NextLevel = true;
                EXP_Amount = 0;
                MaxEXP += 250;
            }

            if (NextLevel)
            {
               Level_Amount += 1;
               TokenCount++;
               SwapTokenCount += 2;
               IL.UpdateItems();
               IL.ItemChance();
               GetComponent<SkillManager>().UpgradeToken++;
               photonView.RPC("ReceiveEXP", RpcTarget.All, 1);
               NextLevel = false;
            }
        }
    }

    void ItemSys()
    {
        if (EnableDeBuff)
        {
            foreach (Buff i in Debuffs)
            {
                StartCoroutine(DebuffTimer(i));
                StartCoroutine(DeleteDebuff(i));
                EnableDeBuff = false;
            }
        }
    }

    [PunRPC]
    private void PlayerDeadAndRespawn(int NumberOfAction)
    {
        if (NumberOfAction == 0)
        {
            Player_Camera.SetActive(false);
            MainHeader_UI.SetActive(false);
            ColliderSys.SetActive(false);
            ColliderTrigger.enabled = false;
            CharController.enabled = false;
            PM.BossZone = false;
            GetComponent<Animator>().Play("Soldier_Die");
            Dead = true;
            DeathCount++;
            PM.UnderAttack = false;
            if (PM.Hater != null && PM.Hater.name != "LightBoss" && PM.Hater.name != "DarkBoss")
            {
                PM.KF.AddNewKillListing(PM.Hater.GetComponent<Player_MAIN>().PlayFab_Nickname, PM.PlayFab_Nickname);

                PM.Hater.GetComponent<PlayerSTAT>().ReceiveEXP(2);
                PM.Hater.GetComponent<PlayerSTAT>().SwapTokenCount += 2;

                PM.Hater.GetComponent<GunScript>().SoundScript.DeathHitCheck();

                if (PM.Hater.GetComponent<PlayerSTAT>().Buffs.Contains(BuffList.InfectedSkullBuff)) //�������
                {
                    PM.Hater.GetComponent<ItemSysScript>().InfectedSkull_Count++;
                }
            }
            
            if(PM.Hater == null)
            {
                PM.KF.AddNewKillListing(PM.PlayFab_Nickname, PM.PlayFab_Nickname);
            }

            if(PM.Hater != null && PM.Hater.name == "LightBoss")
            {
                PM.KF.AddNewKillListing("Purifier", PM.PlayFab_Nickname);
                PM.Hater.GetComponent<LightBossSys>().Player = null;
                PM.Hater.GetComponent<LightBossSys>().BossAnimator.Play("Idle");
                PM.Hater = null;
            }

            if(PM.Hater != null && PM.Hater.name == "DarkBoss")
            {
                PM.KF.AddNewKillListing("Summoner", PM.PlayFab_Nickname);
                PM.Hater = null;
            }

            if (Debuffs.Contains(BuffList.LightBossBuff))
            {
                HP_MaxAmount -= 250;
                DMG_Amount -= 25;
                Armor_Amount -= 3;
                GUI_Effects.PlayerEffect[9].SetActive(false);
            }

            if (Debuffs.Contains(BuffList.DarkBossBuff))
            {
                HP_MaxAmount -= 250;
                DMG_Amount -= 25;
                Armor_Amount -= 3;
                GUI_Effects.PlayerEffect[10].SetActive(false);
            }

            Debuffs.RemoveAll(p => p.BuffType != Buff.BuffID.TyranitBelt_Buff);
        }

        if(NumberOfAction == 1)
        {
            Player_Model.SetActive(false);

            for (int i = 0; i < Effect_Model.transform.childCount; i++)
            {
                var child = Effect_Model.transform.GetChild(i).gameObject;
                if (child != null)
                    child.SetActive(false);
            }
        }

        if(NumberOfAction == 2)
        {
            if (photonView.IsMine)
            {
                PM.transform.SetPositionAndRotation(PM.SpawnPoint.transform.position, PM.SpawnPoint.transform.rotation.normalized);
            }
            Dead = false;
            Player_Camera.SetActive(true);
            DeadPlayer_Camera.SetActive(false);
            ColliderSys.SetActive(true);
            ColliderTrigger.enabled = true;
            CharController.enabled = true;
            GetComponent<Look>().cam = Player_Camera.GetComponent<Camera>();
            MainHeader_UI.SetActive(true);
            Player_Model.SetActive(true);
            HP_Amount = HP_MaxAmount;
            GetComponent<Animator>().Play("Soldier_Idle_1");
        }
    }

    public void DieAnimation()
    {
        photonView.RPC("PlayerDeadAndRespawn", RpcTarget.All, 1);
    }

    [PunRPC]
    public void ReceiveDMG(float dmg)
    {
        if (dmg - Armor_Amount > 0 && !PM.SafeZone)
        {
            HP_Amount -= dmg - Armor_Amount;
            PM.MusicTimer = 0;
            DamagedScreen.SetActive(true);
            if(!PM.MusicAux.isPlaying)
            {
              PM.UnderAttack = true;
            }
        }

        if (Buffs.Contains(BuffList.EnergyHelmetBuff))
        {
            int Chance = Random.Range(0, 99);

            if(Chance < 10)
            {
                GetComponent<ItemSysScript>().photonView.RPC("ItemEffect", RpcTarget.All, 116, false);
            }
        }

        lerpTimer = 0f;
    }

    void StatSys()
    {
        Kills_Text.text = KillsCount.ToString();
        Deaths_Text.text = DeathCount.ToString();
        MaxHPStat_Text.text = HP_MaxAmount.ToString();
        MaxDMGStat_Text.text = DMG_Amount.ToString();
        MaxArmorStat_Text.text = Armor_Amount.ToString();

        if (Input.GetKey(KeyCode.F1))
        {
            ClassInfo.SetActive(true);
        } else if (!Input.GetKey(KeyCode.F1))
        {
            ClassInfo.SetActive(false);
        }
    }

    IEnumerator DeleteDebuff(Buff Buff)
    {
        yield return new WaitForSeconds(Buff.Time);
        GUI_Effects.UIEffect[Buff.UIEffectID].SetActive(false);
        GUI_Effects.PlayerEffect[4].SetActive(false);
        GUI_Effects.PlayerEffect[5].SetActive(false);
        Debuffs.Remove(Buff);

        if (photonView.IsMine)
        {

            if (Buff.BuffType == Buff.BuffID.Bleed_Debuff)
            {
                BuffList.BleedDebuff.Time = BuffList.BleedBuff.Time;
            }

            if (Buff.BuffType == Buff.BuffID.Poison_Debuff)
            {
                BuffList.PoisonDeBuff.Time = BuffList.PoisonBuff.Time;
            }

            if (Buff.BuffType == Buff.BuffID.Electric_Debuff) //����������
            {
                BuffList.ElectricDebuff.Time = BuffList.ElectricCrownBuff.Time;
                PM.MovementSpeed = PM.PreviousMovement;
            }

            if (Buff.BuffType == Buff.BuffID.SoldierHeal_Buff)
            {
                GetComponent<PhotonView>().RPC("EnableOrDisable", RpcTarget.All, 0);
                HPRegen_Amount -= SM.Skills[1].SomeValue;
            }

            if (Buff.BuffType == Buff.BuffID.Shield_Buff)
            {
                GetComponent<ItemSysScript>().photonView.RPC("ItemEffect", RpcTarget.All, 104, true);
                if (Buffs.Contains(BuffList.BrainModBuff))
                {
                    BuffList.ShieldBuff.Time -= 2f;
                }
            }

            if (Buff.BuffType == Buff.BuffID.CronerChain_Buff)
            {
                GetComponent<ItemSysScript>().photonView.RPC("ItemEffect", RpcTarget.All, 115, true);
                if (Buffs.Contains(BuffList.BrainModBuff))
                {
                    BuffList.CronerChainBuff.Time -= 2f;
                }
            }

            if (Buff.BuffType == Buff.BuffID.PlanB_Buff)
            {
                GetComponent<ItemSysScript>().photonView.RPC("ItemEffect", RpcTarget.All, 118, true);
                if (Buffs.Contains(BuffList.BrainModBuff))
                {
                    BuffList.PlanB_Buff.Time -= 2f;
                }
            }

            if (Buff.BuffType == Buff.BuffID.SectantCloak_Buff)
            {
                GetComponent<ItemSysScript>().photonView.RPC("ItemEffect", RpcTarget.All, 2114, true);
                if (Buffs.Contains(BuffList.BrainModBuff))
                {
                    BuffList.SectantCloackBuff.Time -= 2f;
                }
            }

            if (Buff.BuffType == Buff.BuffID.Mirror_Buff)
            {
                GetComponent<ItemSysScript>().photonView.RPC("ItemEffect", RpcTarget.All, 2219, true);
                if (Buffs.Contains(BuffList.BrainModBuff))
                {
                    BuffList.MirrorBuff.Time -= 2f;
                }
            }

            if (Buff.BuffType == Buff.BuffID.EritondEye_Buff)
            {
                GetComponent<ItemSysScript>().photonView.RPC("ItemEffect", RpcTarget.All, 2322, true);
                if (Buffs.Contains(BuffList.BrainModBuff))
                {
                    BuffList.EritondEyeBuff.Time -= 2f;
                }
            }

            if (Buff.BuffType == Buff.BuffID.EnergyDrink_Buff)
            {
                HP_Amount += 50;
                PM.MovementSpeed -= 0.2f;
                if (Buffs.Contains(BuffList.BrainModBuff))
                {
                    BuffList.EnergyDrinkBuff.Time -= 2f;
                }
            }

            if (Buff.BuffType == Buff.BuffID.HealthRing_Buff)
            {
                HP_MaxAmount -= 300;
                if (Buffs.Contains(BuffList.BrainModBuff))
                {
                    BuffList.HealthRingBuff.Time -= 2f;
                }
            }

            if (Buff.BuffType == Buff.BuffID.LightBoss_Buff)
            {
                HP_MaxAmount -= 250;
                DMG_Amount -= 25;
                Armor_Amount -= 3;
                GUI_Effects.PlayerEffect[9].SetActive(false);
            }

            if (Buff.BuffType == Buff.BuffID.DarkBoss_Buff)
            {
                HP_MaxAmount -= 250;
                DMG_Amount -= 25;
                Armor_Amount -= 3;
                GUI_Effects.PlayerEffect[10].SetActive(false);
            }
        }
    }

    IEnumerator DebuffTimer(Buff SomeBuff)
    {
        while (Debuffs.Contains(SomeBuff))
        {
            if (SomeBuff.BuffType == Buff.BuffID.Bleed_Debuff)
            {
                GUI_Effects.UIEffect[SomeBuff.UIEffectID].SetActive(true);
                GUI_Effects.PlayerEffect[4].SetActive(true);
                photonView.RPC("ReceiveDMG", RpcTarget.All, 5f);
            }

            if (SomeBuff.BuffType == Buff.BuffID.Poison_Debuff)
            {
                GUI_Effects.UIEffect[SomeBuff.UIEffectID].SetActive(true);
                GUI_Effects.PlayerEffect[5].SetActive(true);
                photonView.RPC("ReceiveDMG", RpcTarget.All, 3f);
            }

            if (SomeBuff.BuffType == Buff.BuffID.Electric_Debuff)
            {
                GUI_Effects.UIEffect[SomeBuff.UIEffectID].SetActive(true);
                photonView.RPC("ReceiveDMG", RpcTarget.All, 3f);
                PM.MovementSpeed = 0.6f;
            }

            if(SomeBuff.BuffType == Buff.BuffID.SoldierHeal_Buff)
            {
                GUI_Effects.UIEffect[SomeBuff.UIEffectID].SetActive(true);
            }

            if(SomeBuff.BuffType == Buff.BuffID.CronerChain_Buff)
            {
                HP_Amount = 1;
                GUI_Effects.UIEffect[SomeBuff.UIEffectID].SetActive(true);
            }

            if (SomeBuff.BuffType == Buff.BuffID.PlanB_Buff)
            {
                GUI_Effects.UIEffect[SomeBuff.UIEffectID].SetActive(true);
            }

            if (SomeBuff.BuffType == Buff.BuffID.SectantCloak_Buff)
            {
                GUI_Effects.UIEffect[SomeBuff.UIEffectID].SetActive(true);
            }

            if (SomeBuff.BuffType == Buff.BuffID.Mirror_Buff)
            {
                GUI_Effects.UIEffect[SomeBuff.UIEffectID].SetActive(true);
            }

            if (SomeBuff.BuffType == Buff.BuffID.EritondEye_Buff)
            {
                GUI_Effects.UIEffect[SomeBuff.UIEffectID].SetActive(true);
                HP_Amount = HP_MaxAmount;
            }

            if (SomeBuff.BuffType == Buff.BuffID.EnergyDrink_Buff)
            {
                HP_Amount -= 50;
                PM.MovementSpeed += 0.2f;
            }

            if (SomeBuff.BuffType == Buff.BuffID.HealthRing_Buff)
            {
                HP_MaxAmount += 300;
                HP_Amount += 300;
            }

            if(SomeBuff.BuffType == Buff.BuffID.LightBoss_Buff)
            {
                HP_MaxAmount += 250;
                HP_Amount += 250;
                DMG_Amount += 25;
                Armor_Amount += 3;
                GUI_Effects.PlayerEffect[9].SetActive(true);
            }

            if(SomeBuff.BuffType == Buff.BuffID.DarkBoss_Buff)
            {
                HP_MaxAmount += 250;
                HP_Amount += 250;
                DMG_Amount += 25;
                Armor_Amount += 3;
                GUI_Effects.PlayerEffect[10].SetActive(true);
            }
            yield return new WaitForSeconds(SomeBuff.DeltaTime);
        }
    }

    [PunRPC]
    void ReceiveEXP(int NumberOfACtion)
    {
            if (NumberOfACtion == 0)
            {
                TimeToReceiveEXP += Time.deltaTime;
                if (TimeToReceiveEXP >= 3f)
                {
                    EXP_Amount += 25;
                    TimeToReceiveEXP = 0;
                }
            }

            if (NumberOfACtion == 1)
            {
                LVL_Particles.SetActive(true);
            }

            if (NumberOfACtion == 2)
            {
                EXP_Amount += 150;
                KillsCount++;
            }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(HP_Amount);
            stream.SendNext(HP_MaxAmount);
            stream.SendNext(Armor_Amount);
            stream.SendNext(Level_Amount);
            stream.SendNext(gameObject.tag);
        }
        else if (stream.IsReading)
        {
            HP_Amount = (float)stream.ReceiveNext();
            HP_MaxAmount = (float)stream.ReceiveNext();
            Armor_Amount = (float)stream.ReceiveNext();
            Level_Amount = (int)stream.ReceiveNext();
            gameObject.tag = (string)stream.ReceiveNext();
        }
    }
}
