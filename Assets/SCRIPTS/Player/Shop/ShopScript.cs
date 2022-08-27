using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ShopScript : MonoBehaviour, IPunObservable
{
    public const int CARDSIZE = 3;

    public CardSys[] CardSys = new CardSys[CARDSIZE];
    public Card result;
    public Inventory InventoryScript;
    public ItemUI UI;
    public ItemList IL;
    public PlayerSTAT PS;

    private void Start()
    {
        GetComponent<PhotonView>().RPC("Swap", RpcTarget.All, 1);
        GetComponent<PhotonView>().RPC("Swap", RpcTarget.All, 2);
        GetComponent<PhotonView>().RPC("Swap", RpcTarget.All, 3);
    }

    private void Update()
    {
        if(PS.SwapTokenCount <= 0)
        {
            PS.SwapTokenCount = 0;

            foreach (CardSys i in CardSys)
            {
                i.SwapButton.interactable = false;
            }
        }
        else
        {
            foreach (CardSys i in CardSys)
            {
                i.SwapButton.interactable = true;
            }
        }

        if(PS.TokenCount <= 0)
        {
            PS.TokenCount = 0;
            foreach (CardSys i in CardSys)
            {
                i.GetComponent<Button>().interactable = false;
            }
        }
        else if(PS.TokenCount > 0)
        {
            foreach (CardSys i in CardSys)
            {
                i.GetComponent<Button>().interactable = true;
            }
        }

        if(InventoryScript.EmptySlot() == Inventory.SlotInfo.Full && PS.TokenCount >= 0)
        {
            foreach(CardSys i in CardSys)
            {
                i.GetComponent<Button>().interactable = false;
            }
        } 
        else if(PS.TokenCount > 0)
        {
            foreach (CardSys i in CardSys)
            {
                i.GetComponent<Button>().interactable = true;
            }
        }

        // if(InventoryScript.slots[0] != null) НА ВРЕМЯ ТЕСТА ОТКЛЮЧЕНО
        // {
        //     InventoryScript.SellButton[0].interactable = true;
        // }
        // else
        // {
        //     InventoryScript.SellButton[0].interactable = false;
        // }

        // if (InventoryScript.slots[1] != null)
        // {
        //     InventoryScript.SellButton[1].interactable = true;
        // }
        // else
        // {
        //     InventoryScript.SellButton[1].interactable = false;
        // }
    }

    public void Swap(Item.Color Color)
    {
        CardSys[(int)Color - 1].Card = Generate(Color);
        CardSys[(int)Color - 1].CardUI();
    }

    [PunRPC]
    public void Swap(int ColorInt)
    {
        Swap((Item.Color)ColorInt);
        PS.SwapTokenCount--;
    }

    public void SwapRPCButton(int ColorInt)
    {
        GetComponent<PhotonView>().RPC("Swap", RpcTarget.All, ColorInt);
    }

    [PunRPC]
    public void BuyButton(int Color)
    {
        Buy(CardSys[Color - 1].Card);
        Swap(Color);
    }

    public void BuyRPCButton(int Color)
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            GetComponent<PhotonView>().RPC("BuyButton", RpcTarget.All, Color);
        }
    }


    public void Buy(Card Card)
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            int CurrentSlot = (int)InventoryScript.EmptySlot();
            PS.TokenCount--;

            InventoryScript.slots[CurrentSlot] = new Slot(Card.item, (KeyCode)(49 + CurrentSlot));
            InventoryScript.slots[CurrentSlot].SlotIndex = CurrentSlot;
            if (Card.item.isActiveItem)
            {
                InventoryScript.slots[CurrentSlot].Aitem = (ActiveItem)Card.item;
            }
            InventoryScript.SlotsHeaderUI[CurrentSlot].sprite = Card.item.ItemIcon;
            PS.AddStats(InventoryScript.slots[CurrentSlot].item);
        }
    }

    void TEST_ITEM_BUY(Item item)
    {
            if (GetComponent<PhotonView>().IsMine)
        {
            int CurrentSlot = (int)InventoryScript.EmptySlot();
            PS.TokenCount--;

            InventoryScript.slots[CurrentSlot] = new Slot(item, (KeyCode)(49 + CurrentSlot));
            InventoryScript.slots[CurrentSlot].SlotIndex = CurrentSlot;
            if (item.isActiveItem)
            {
                InventoryScript.slots[CurrentSlot].Aitem = (ActiveItem)item;
            }
            InventoryScript.SlotsHeaderUI[CurrentSlot].sprite = item.ItemIcon;
            PS.AddStats(InventoryScript.slots[CurrentSlot].item);
        }
    }

    public void TEST_ITEM_RPC(int ITEM_ID)
    {
        GetComponent<PhotonView>().RPC("TEST_ITEM_BUTTON", RpcTarget.All, ITEM_ID);
    }

    [PunRPC]
    public void TEST_ITEM_BUTTON(int ID)
    {
        if(ID == IL.AmuletOfResistance.NewItemID)
        {
            TEST_ITEM_BUY(IL.AmuletOfResistance);
        }

        if(ID == IL.ArmorPlate.NewItemID)
        {
            TEST_ITEM_BUY(IL.ArmorPlate);
        }

        if(ID == IL.BootsOfStrong.NewItemID)
        {
            TEST_ITEM_BUY(IL.BootsOfStrong);
        }

        if(ID == IL.Helmet.NewItemID)
        {
            TEST_ITEM_BUY(IL.Helmet);
        }

        if(ID == IL.Shield.NewItemID)
        {
            TEST_ITEM_BUY(IL.Shield);
        }

        if(ID == IL.CronerChain.NewItemID)
        {
            TEST_ITEM_BUY(IL.CronerChain);
        }

        if(ID == IL.EnergyHelmet.NewItemID)
        {
            TEST_ITEM_BUY(IL.EnergyHelmet);
        }

        if(ID == IL.MaskWithTubes.NewItemID)
        {
            TEST_ITEM_BUY(IL.MaskWithTubes);
        }

        if(ID == IL.PlanB.NewItemID)
        {
            TEST_ITEM_BUY(IL.PlanB);
        }

        if(ID == IL.Cevlar.NewItemID)
        {
            TEST_ITEM_BUY(IL.Cevlar);
        }

        if(ID == IL.Spike.NewItemID)
        {
            TEST_ITEM_BUY(IL.Spike);
        }

        if(ID == IL.SpikeHelmet.NewItemID)
        {
            TEST_ITEM_BUY(IL.SpikeHelmet);
        }

        if(ID == IL.InfectedCevlar.NewItemID)
        {
            TEST_ITEM_BUY(IL.InfectedCevlar);
        }

        if(ID == IL.TyranitBelt.NewItemID)
        {
            TEST_ITEM_BUY(IL.TyranitBelt);
        }

        if(ID == IL.Barrel.NewItemID)
        {
            TEST_ITEM_BUY(IL.Barrel);
        }

        if(ID == IL.Butt.NewItemID)
        {
            TEST_ITEM_BUY(IL.Butt);
        }

        if(ID == IL.ChipStable.NewItemID)
        {
            TEST_ITEM_BUY(IL.ChipStable);
        }

        if(ID == IL.ElectricCrown.NewItemID)
        {
            TEST_ITEM_BUY(IL.ElectricCrown);
        }

        if(ID == IL.Handle.NewItemID)
        {
            TEST_ITEM_BUY(IL.Handle);
        }

        if(ID == IL.KnifeOfJustice.NewItemID)
        {
            TEST_ITEM_BUY(IL.KnifeOfJustice);
        }

        if(ID == IL.Magazines.NewItemID)
        {
            TEST_ITEM_BUY(IL.Magazines);
        }

        if(ID == IL.Overlap.NewItemID)
        {
            TEST_ITEM_BUY(IL.Overlap);
        }

        if(ID == IL.StrangeCard.NewItemID)
        {
            TEST_ITEM_BUY(IL.StrangeCard);
        }

        if(ID == IL.AcidBullet.NewItemID)
        {
            TEST_ITEM_BUY(IL.AcidBullet);
        }

        if(ID == IL.ChipSight.NewItemID)
        {
            TEST_ITEM_BUY(IL.ChipSight);
        }

        if(ID == IL.DarkNeedle.NewItemID)
        {
            TEST_ITEM_BUY(IL.DarkNeedle);
        }

        if(ID == IL.JawMod.NewItemID)
        {
            TEST_ITEM_BUY(IL.JawMod);
        }

        if(ID == IL.SectantCloak.NewItemID)
        {
            TEST_ITEM_BUY(IL.SectantCloak);
        }

        if(ID == IL.ThermalSensor.NewItemID)
        {
            TEST_ITEM_BUY(IL.ThermalSensor);
        }

        if(ID == IL.UpgradedMagazines.NewItemID)
        {
            TEST_ITEM_BUY(IL.UpgradedMagazines);
        }

        if(ID == IL.BarrelMark2.NewItemID)
        {
            TEST_ITEM_BUY(IL.BarrelMark2);
        }

        if(ID == IL.EnergyHand.NewItemID)
        {
            TEST_ITEM_BUY(IL.EnergyHand);
        }

        if(ID == IL.Mirror.NewItemID)
        {
            TEST_ITEM_BUY(IL.Mirror);
        }

        if(ID == IL.ProBelt.NewItemID)
        {
            TEST_ITEM_BUY(IL.ProBelt);
        }

        if(ID == IL.SpeedyBoots.NewItemID)
        {
            TEST_ITEM_BUY(IL.SpeedyBoots);
        }

        if(ID == IL.EritondEye.NewItemID)
        {
            TEST_ITEM_BUY(IL.EritondEye);
        }

        if(ID == IL.MIZEmblem.NewItemID)
        {
            TEST_ITEM_BUY(IL.MIZEmblem);
        }

        if(ID == IL.EnergyDrink.NewItemID)
        {
            TEST_ITEM_BUY(IL.EnergyDrink);
        }

        if(ID == IL.GemOfLife.NewItemID)
        {
            TEST_ITEM_BUY(IL.GemOfLife);
        }

        if(ID == IL.GloryPoster.NewItemID)
        {
            TEST_ITEM_BUY(IL.GloryPoster);
        }

        if(ID == IL.InfectedSkull.NewItemID)
        {
            TEST_ITEM_BUY(IL.InfectedSkull);
        }

        if(ID == IL.SecretEmblem.NewItemID)
        {
            TEST_ITEM_BUY(IL.SecretEmblem);
        }

        if(ID == IL.HealthRing.NewItemID)
        {
            TEST_ITEM_BUY(IL.HealthRing);
        }

        if(ID == IL.Helper.NewItemID)
        {
            TEST_ITEM_BUY(IL.Helper);
        }

        if(ID == IL.ProBoot.NewItemID)
        {
            TEST_ITEM_BUY(IL.ProBoot);
        }

        if(ID == IL.SectantBook.NewItemID)
        {
            TEST_ITEM_BUY(IL.SectantBook);
        }

        if(ID == IL.BrainMod.NewItemID)
        {
            TEST_ITEM_BUY(IL.BrainMod);
        }

        if(ID == IL.TyranitGlasses.NewItemID)
        {
            TEST_ITEM_BUY(IL.TyranitGlasses);
        }

        if(ID == IL.NecklesOfNature.NewItemID)
        {
            TEST_ITEM_BUY(IL.NecklesOfNature);
        }
    }

    [PunRPC]
    public void Sell(int SlotNumber)        
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            PS.RemoveStats(InventoryScript.slots[SlotNumber].item);
            InventoryScript.slots[SlotNumber] = null;
            InventoryScript.SlotsHeaderUI[SlotNumber].sprite = InventoryScript.EmptySlotUI;
        }
    }

    public void SellRPCButton(int SlotNumber)
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            GetComponent<PhotonView>().RPC("Sell", RpcTarget.All, SlotNumber);
        }
    }

    private Card Generate(Item.Color Color)
    {
        int randomNum = Random.Range(1, 101);
        List<Item> CorrectedItemList = new List<Item>();
        foreach (Item i in IL.GetItemList())
        {        
            if (randomNum >= i.ItemRate.minProb && randomNum <= i.ItemRate.maxProb && i.ColorName == Color)
            {
                CorrectedItemList.Add(i);
            }  
        }
        randomNum = Random.Range(0, CorrectedItemList.Count-1);
        result = new Card(CorrectedItemList[randomNum]);
        return result;
    }

    public void SwapCardAnimation(int ColorInt)
    {
        if(ColorInt == 1)
        {
            GetComponent<Animator>().Play("SwitchBlue");
        }

        if (ColorInt == 2)
        {
            GetComponent<Animator>().Play("SwitchRed");
        }

        if (ColorInt == 3)
        {
            GetComponent<Animator>().Play("SwitchGreen");
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
