using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class ItemSysScript : MonoBehaviourPun, IPunObservable
{
    [Header("Scripts")]
    private Inventory Inv;
    private PlayerSTAT PS;
    private Player_MAIN P_M;
    private GunScript GS;
    public AudioSource AFX;

    public Image[] CoolDownItem;

    [Header("Shield")]
    public GameObject Shield_Model;

    public AudioClip Shield_Audio;

    [Header("CronerChain")]
    public GameObject CronerChain_Effect;
    public GameObject CronerCHain_UIEffect;

    [Header("ElectricCrown")]
    public GameObject ElectricCrown_Effect;
    public GameObject ElectricDamage_Collider;

    [Header("PlanB")]
    public GameObject PlanB_Model;
    public GameObject PlanB_Effect;

    public AudioClip PlanB_Audio;


    [Header("KnifeOfJustice")]
    public GameObject KnifeOfJustice_Object;
    public Transform SpawnPoint;

    [Header("PoisonBullets")]
    public GameObject Poison_Effect;
    public GameObject Poison_UIEffect;

    [Header("SectantCloak")]
    public GameObject SectantCloak_Effect;
    public GameObject SectantCloak_UIEffect;

    public AudioClip SectantCloak_Audio;

    [Header("ThermalSensor")]
    public GameObject ThermalSensor_Object;

    [Header("Mirror")]
    public GameObject Mirror_Model;
    public GameObject Mirror_Effect;

    public AudioClip Mirror_Audio;

    [Header("EritondEye")]
    public int EritondEye_DeathCount;
    public GameObject EritondEye_Effect;
    public GameObject EritondEye_UIEffect;

    public AudioClip EritondEye_Audio;

    [Header("InfectedSkull")]
    public int InfectedSkull_Count;
    public int InfectedSkull_NeedCount;
    public Text InfectedSkullItem1_Text;
    public Text InfectedSkullItem2_Text;

    private void Start()
    {
        Inv = GetComponent<Inventory>();
        PS = GetComponent<PlayerSTAT>();
        P_M = GetComponent<Player_MAIN>();
        GS = GetComponent<GunScript>();
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            CoolDownItemSys();
            foreach(Slot i in Inv.slots)
            {
                if (i != null && i.Aitem != null)
                {
                    if (i.Aitem.PassiveActiveItem)
                    {
                        CronerChainSys(i.SlotIndex);
                        InfectedSkullSys();
                    }

                    if (Input.GetKeyDown(i.Key) && i.item.isActiveItem && i.Aitem.Active && !i.Aitem.PassiveActiveItem)
                    {
                        PS.photonView.RPC("AddDeBuff", RpcTarget.All, i.item.ItemName);
                        PS.photonView.RPC("ItemEffect", RpcTarget.All, i.item.NewItemID, false);
                        PS.EnableDeBuff = true;
                        i.Aitem.Active = false;
                    }
                }
            }

            SectantCloakSys();
            ThermalSensorSys();
            EnergyHandSys();
        }
    }

    void CoolDownItemSys()
    {
        foreach (Slot i in Inv.slots)
        {
            if(i != null && i.Aitem != null)
            {
                if (!i.Aitem.Active)
                {
                    Inv.CoolDownUI[i.SlotIndex].gameObject.SetActive(true);
                    Inv.CoolDownUI[i.SlotIndex].fillAmount -= 1 / i.Aitem.Cooldown * Time.deltaTime;

                    if (Inv.CoolDownUI[i.SlotIndex].fillAmount <= 0)
                    {
                        Inv.CoolDownUI[i.SlotIndex].fillAmount = 1;
                        i.Aitem.Active = true;
                        Inv.CoolDownUI[i.SlotIndex].gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    void CronerChainSys(int slotIndex)
    {
        if (photonView.IsMine)
        {
            foreach (Buff i in PS.Buffs)
            {
                if (i.BuffType == Buff.BuffID.CronerChain_Buff && Inv.slots[slotIndex].Aitem.Active && PS.HP_Amount < PS.HP_MaxAmount / 5)
                {
                    Inv.slots[slotIndex].Aitem.Active = false;
                    PS.Armor_Amount += 10000;
                    PS.photonView.RPC("AddDeBuff", RpcTarget.All, Inv.slots[slotIndex].item.ItemName);
                    PS.photonView.RPC("ItemEffect", RpcTarget.All, Inv.slots[slotIndex].item.NewItemID, false);

                    PS.EnableDeBuff = true;

                }
            }
        }
    }

    [PunRPC]
    public void TyranitBeltSys()
    {
        if (photonView.IsMine)
        {
            foreach (Slot slot in Inv.slots)
            {
                if (slot != null)
                {
                    if (slot.item.NewItemID == 1313)
                    {
                        if (slot.SlotIndex == 0)
                        {
                            Inv.slots[0].Aitem.Active = false;
                            PS.photonView.RPC("AddDeBuff", RpcTarget.All, Inv.slots[0].item.ItemName);
                            PS.EnableDeBuff = true;
                        }

                        if (slot.SlotIndex == 1)
                        {
                            Inv.slots[1].Aitem.Active = false;
                            PS.photonView.RPC("AddDeBuff", RpcTarget.All, Inv.slots[1].item.ItemName);
                            PS.EnableDeBuff = true;
                        }
                    }
                }
            }
        }
    }

    public void SectantCloakSys()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Q) ||
            Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Z) ||
            Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Mouse0) && PS.Debuffs.Contains(BuffList.SectantCloackBuff)) {

            PS.Debuffs.Remove(BuffList.SectantCloackBuff);
            photonView.RPC("ItemEffect", RpcTarget.All, 2114, true);
        }
    }

    public void ThermalSensorSys()
    {
        if (PS.Buffs.Contains(BuffList.ThermalSensorBuff) && GS.shooting)
        {
            int ThermalSensorChance = Random.Range(0, 99);

            if(ThermalSensorChance <= 5)
            {
                photonView.RPC("ItemEffect", RpcTarget.All, 2115, false);
            }
        }
    }

    public void EnergyHandSys()
    {
       if (Input.GetKeyDown(KeyCode.R) && PS.Buffs.Contains(BuffList.EnergyHandsBuff) && GS.BulletsLeft < GS.MagazineSize)
       {
            photonView.RPC("ItemEffect", RpcTarget.All, 116, false); //��������� ���� ����� ��� � 116 �������
       }
    }

    public void InfectedSkullSys()
    {
        if(InfectedSkull_NeedCount < 2)
        {
            InfectedSkull_NeedCount = 2;
        }

        if (InfectedSkull_Count < 0)
        {
            InfectedSkull_Count = 0;
        }

        if(InfectedSkull_Count > 10)
        {
            InfectedSkull_Count = 10;
        }

        if (InfectedSkull_Count == InfectedSkull_NeedCount && InfectedSkull_NeedCount != 10)
        {
            PS.HPRegen_Amount += 2;
            InfectedSkull_NeedCount += 2;
        }

        foreach (Slot slot in Inv.slots)
        {
            if (slot != null)
            {
                if (slot.item.NewItemID == 303)
                {
                    if (slot.SlotIndex == 0)
                    {
                        InfectedSkullItem1_Text.text = InfectedSkull_Count.ToString();
                        InfectedSkullItem1_Text.gameObject.SetActive(true);
                    }

                    if (slot.SlotIndex == 1)
                    {
                        InfectedSkullItem2_Text.text = InfectedSkull_Count.ToString();
                        InfectedSkullItem2_Text.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    [PunRPC]
    public void ItemEffect(int ItemID, bool Disable)
    {
        if (ItemID == 104 && !Disable)
        {
            Shield_Model.SetActive(true);
            AFX.PlayOneShot(Shield_Audio);
            P_M.MovementSpeed -= 0.5f;
            GS.GunAnimationNetwork(3);
            GS.blockUsing = true;
        }

        if (ItemID == 104 && Disable)
        {
            PS.Debuffs.Remove(BuffList.ShieldBuff);
            Shield_Model.GetComponent<Animator>().Play("Shield_Hide");
            AFX.PlayOneShot(Shield_Audio);
            P_M.MovementSpeed += 0.5f;
            GS.GunAnimationNetwork(4);
            GS.blockUsing = false;
        }

        if (ItemID == 115 && !Disable)
        {
            CronerChain_Effect.SetActive(true);
        }

        if (ItemID == 115 && Disable)
        {
            PS.Armor_Amount -= 10000;
            PS.Debuffs.Remove(BuffList.CronerChainBuff);
            CronerChain_Effect.SetActive(false);
        }

        if(ItemID == 116 && !Disable)
        {
            ElectricDamage_Collider.SetActive(true);
        }

        if (ItemID == 118 && !Disable)
        {
            PlanB_Model.SetActive(true);
            P_M.DisableMovement = true;
            AFX.PlayOneShot(PlanB_Audio);
        }

        if (ItemID == 118 && Disable)
        {
            P_M.DisableMovement = false;
            PlanB_Effect.SetActive(false);
            PS.Debuffs.Remove(BuffList.PlanB_Buff);
            PlanB_Model.GetComponent<Animator>().Play("PlanB_Destroy");
        }

        if(ItemID == 1313 && Disable)
        {
            PS.Debuffs.Remove(BuffList.TyranitBeltBuff);
        }

        if(ItemID == 206 && !Disable)
        {
            GameObject Knife_Object = Instantiate(KnifeOfJustice_Object, SpawnPoint.position, SpawnPoint.rotation);
            Knife_Object.GetComponent<KnifeOfJustice>().Photon_Player = GetComponent<PhotonView>();
        }

        if (ItemID == 2114 && Disable)
        {
            if (!photonView.IsMine)
            {
                PS.Player_Model.SetActive(true);
                PS.ColliderSys.SetActive(true);
                PS.CharController.enabled = true;
                PS.Player_Weapon.SetActive(true);
                PS.MainHeader_UI.SetActive(true);
            }
        }

        if (ItemID == 2114 && !Disable)
        {
            Instantiate(SectantCloak_Effect, transform.position, transform.rotation);
            if (!photonView.IsMine)
            {
                PS.Player_Model.SetActive(false);
                PS.ColliderSys.SetActive(false);
                PS.CharController.enabled = false;
                AFX.PlayOneShot(SectantCloak_Audio);
                PS.Player_Weapon.SetActive(false);
                PS.MainHeader_UI.SetActive(false);
            }
        }

        if (ItemID == 2115 && Disable)
        {
            ThermalSensor_Object.SetActive(false);
        }

        if (ItemID == 2115 && !Disable)
        {
            ThermalSensor_Object.SetActive(true);
        }

        if (ItemID == 2219 && !Disable)
        {
            P_M.BlockUse = true;
            Mirror_Model.SetActive(true);
            GS.blockUsing = true;
            P_M.DisableMovement = true;
            AFX.PlayOneShot(Mirror_Audio);
        }

        if (ItemID == 2219 && Disable)
        {
            P_M.BlockUse = false;
            Mirror_Model.SetActive(false);
            GS.blockUsing = false;
            P_M.DisableMovement = false;
        }

        if (ItemID == 2322 && !Disable)
        {
            EritondEye_Effect.SetActive(true);
            AFX.PlayOneShot(EritondEye_Audio);
        }

        if (ItemID == 2322 && Disable)
        {
            EritondEye_Effect.SetActive(false);
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {

        }
        else if (stream.IsReading)
        {

        }
    }
}
