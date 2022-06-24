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

        if(InventoryScript.slots[0] != null)
        {
            InventoryScript.SellButton[0].interactable = true;
        }
        else
        {
            InventoryScript.SellButton[0].interactable = false;
        }

        if (InventoryScript.slots[1] != null)
        {
            InventoryScript.SellButton[1].interactable = true;
        }
        else
        {
            InventoryScript.SellButton[1].interactable = false;
        }
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
