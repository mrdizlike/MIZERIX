using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueItemSys : MonoBehaviour
{

    [Header("Scripts")]
    public Player_MAIN P_Main;
    public PlayerSTAT P_Stats;
    public ShopScript Shop;
    public GunScript Gun_Script;
    public ItemList IL;

    public ItemRate[] ItemRate;

    [Header("UI_Card")]
    public Image ItemDisplay;
    public Image QualityDisplay;
    public Sprite[] ItemIcon;
    public Sprite[] QualityIcon; // 0 - обычная, 1 - редкая, 2 - мифическая, 3 - легендарная
    public Text ItemName;
    public Text ItemDescription;
    public Button SwapItemButton;

    public Animator ShopUI;
    private IEnumerator coroutine;

    [Header("CardSys_Misc")]
    public int NewItemID;
    public int SwapCount;
    public int[] Common_ItemID;
    public int[] Rare_ItemID;
    public int[] Mythic_ItemID;
    public int[] Legendary_ItemID;

    private void Start()
    {
        ItemRate[0].minProb = 0;
        ItemRate[0].maxProb = 80;
        
        ItemRate[1].minProb = 81;
        ItemRate[1].maxProb = 99;

        ItemRate[2].minProb = 100;
        ItemRate[2].maxProb = 100;

        ItemRate[3].minProb = 100;
        ItemRate[3].maxProb = 100;

        GenerateItem(NewItemID);
    }

    private void Update()
    {
        if (SwapCount == 0)
        {
            SwapItemButton.interactable = false;
        }

        if (SwapCount > 0)
        {
            SwapItemButton.interactable = true;
        }

        //AntiRepeat();
    }

    public void SwapItem()
    {
        coroutine = InvokeSwap();
        SwapCount -= 1;
        StartCoroutine(coroutine);
        ShopUI.Play("SwitchBlue");
    }

    IEnumerator InvokeSwap()
    {
        yield return new WaitForSeconds(0.3f);
        GenerateItem(NewItemID);
    }

    private void GenerateItem(int CheckID)
    {
        float randomNum = Random.Range(0, 100);

        for (int i = 0; i < 4; i++)
        {
            if(randomNum >= ItemRate[i].minProb && randomNum <= ItemRate[i].maxProb)
            {

                Debug.Log(randomNum + " ");

                if(i == 0)
                {
                    NewItemID = Common_ItemID[Random.Range(0, Common_ItemID.Length)];
                    ItemInformation(NewItemID);
                    if(CheckID == NewItemID)
                    {
                        NewItemID = Common_ItemID[Random.Range(0, Common_ItemID.Length)];
                        ItemInformation(NewItemID);
                        Debug.Log("Copy!");
                    }
                    break;
                }

                if (i == 1)
                {
                    NewItemID = Rare_ItemID[Random.Range(0, Rare_ItemID.Length)];
                    ItemInformation(NewItemID);
                    if (CheckID == NewItemID)
                    {
                        NewItemID = Common_ItemID[Random.Range(0, Common_ItemID.Length)];
                        ItemInformation(NewItemID);
                        Debug.Log("Copy!");
                    }
                    break;
                }

                if (i == 2)
                {
                    NewItemID = Mythic_ItemID[Random.Range(0, Mythic_ItemID.Length)];
                    ItemInformation(NewItemID);
                    if (CheckID == NewItemID)
                    {
                        NewItemID = Common_ItemID[Random.Range(0, Common_ItemID.Length)];
                        ItemInformation(NewItemID);
                        Debug.Log("Copy!");
                    }
                    break;
                }

                if (i == 3)
                {
                    NewItemID = Legendary_ItemID[Random.Range(0, Legendary_ItemID.Length)];
                    ItemInformation(NewItemID);
                    if (CheckID == NewItemID)
                    {
                        NewItemID = Common_ItemID[Random.Range(0, Common_ItemID.Length)];
                        ItemInformation(NewItemID);
                        Debug.Log("Copy!");
                    }
                    break;
                }
            }
        }
    }
    private void ItemInformation(int ItemID) //ItemID состоит из трех цифр, где первая это вид предмета, вторая цифра - качество предмета, третья порядковый номер предмета
    {
        if(ItemID == 100)
        {
            ItemDisplay.sprite = ItemIcon[0];
            QualityDisplay.sprite = QualityIcon[0];
            ItemName.text = "Amulet Of Resistance";
            ItemDescription.text = "+ 2 armor \n +150 HP";
        }

        if (ItemID == 101)
        {
            ItemDisplay.sprite = ItemIcon[1];
            QualityDisplay.sprite = QualityIcon[0];
            ItemName.text = "Armor plate";
            ItemDescription.text = "+2 armor";
        }

        if (ItemID == 102)
        {
            ItemDisplay.sprite = ItemIcon[2];
            QualityDisplay.sprite = QualityIcon[0];
            ItemName.text = "Boots of strong";
            ItemDescription.text = "+50 movement speed \n +250 HP";
        }

        if (ItemID == 103)
        {
            ItemDisplay.sprite = ItemIcon[3];
            QualityDisplay.sprite = QualityIcon[0];
            ItemName.text = "Helmet";
            ItemDescription.text = "+3 armor";
        }

        if (ItemID == 104)
        {
            ItemDisplay.sprite = ItemIcon[4];
            QualityDisplay.sprite = QualityIcon[0];
            ItemName.text = "Shield";
            ItemDescription.text = "Activate: Blocks incoming damage, you cannot shoot and use abilities \n Usage time: 2 sec \n Cooldown: 10 sec.";
        }

        if (ItemID == 115)
        {
            ItemDisplay.sprite = ItemIcon[5];
            QualityDisplay.sprite = QualityIcon[1];
            ItemName.text = "Croner Chain";
            ItemDescription.text = "When a critical hit creates a shield that protects the player for a 2 seconds \n +2 armor \n Cooldown: 40 sec.";
        }

        if (ItemID == 116)
        {
            ItemDisplay.sprite = ItemIcon[6];
            QualityDisplay.sprite = QualityIcon[1];
            ItemName.text = "Energy Helmet";
            ItemDescription.text = "When taking damage, there is a chance to emit an energy charge that deals damage to the area. \n +3 armor";
        }

        if (ItemID == 117)
        {
            ItemDisplay.sprite = ItemIcon[7];
            QualityDisplay.sprite = QualityIcon[1];
            ItemName.text = "Mask With Tubes";
            ItemDescription.text = "Reducing time of negative effects \n +2 armor";
        }

        if (ItemID == 118)
        {
            ItemDisplay.sprite = ItemIcon[8];
            QualityDisplay.sprite = QualityIcon[1];
            ItemName.text = "Plan B";
            ItemDescription.text = "Activate: Activated armor that blocks 60% of damage by immobilizing the wearer \n Usage time: 3 sec. \n Cooldown: 25 sec.";
        }

        if (ItemID == 129)
        {
            ItemDisplay.sprite = ItemIcon[9];
            QualityDisplay.sprite = QualityIcon[2];
            ItemName.text = "Cevlar";
            ItemDescription.text = "+5 armor";
        }

        if (ItemID == 1210)
        {
            ItemDisplay.sprite = ItemIcon[10];
            QualityDisplay.sprite = QualityIcon[2];
            ItemName.text = "Spike";
            ItemDescription.text = "Damage Return \n +4 armor \n -25 Movement speed";
        }

        if (ItemID == 1211)
        {
            ItemDisplay.sprite = ItemIcon[11];
            QualityDisplay.sprite = QualityIcon[2];
            ItemName.text = "Spike Helmet";
            ItemDescription.text = "Damage Return. \n +2 armor";
        }

        if (ItemID == 1312)
        {
            ItemDisplay.sprite = ItemIcon[12];
            QualityDisplay.sprite = QualityIcon[3];
            ItemName.text = "Infected Cevlar";
            ItemDescription.text = "Blocks 40% of incoming damage. \n +3 armor";
        }

        if (ItemID == 1313)
        {
            ItemDisplay.sprite = ItemIcon[13];
            QualityDisplay.sprite = QualityIcon[3];
            ItemName.text = "Tyranit Belt";
            ItemDescription.text = "Blocks any ability. \n +2 armor \n -25 Movement speed";
        }
    }

    //void AntiRepeat()
    //{
    //    if(Shop.ItemID[0] == NewItemID)
    //    {
    //        GenerateItem(Shop.ItemID[0]);
    //    }

    //    if (Shop.ItemID[1] == NewItemID)
    //    {
    //        GenerateItem(Shop.ItemID[1]);
    //    }
    //}
    //public void BuyItem()
    //{
    //    SwapItem();
    //    SwapCount += 1; //Ну типа происходит смена предмета и мы возвращаем жетон смены
    //    Shop.TokenAmount--;
    //    //Shop.Item_Sys.UnUsableBuy(NewItemID);
    //    for (int i = 0; i < Shop.ItemID.Length; i++)
    //    {
    //        if (i == 0)
    //        {
    //            Shop.ItemBlock_Animator.Play("ItemBlock1_Buy");
    //        }

    //        if (i == 1)
    //        {
    //            Shop.ItemBlock_Animator.Play("ItemBlock2_Buy");
    //        }

    //        if (Shop.ItemID[i] == 0)
    //        {
    //            Shop.ItemID[i] = NewItemID;
    //            break;
    //        }
    //    }      
    //}

    public void ChanceOfDrop(int Lvl)
    {
        if(Lvl == 2)
        {
            ItemRate[0].minProb = 0;
            ItemRate[0].maxProb = 70;

            ItemRate[1].minProb = 71;
            ItemRate[1].maxProb = 90;

            ItemRate[2].minProb = 91;
            ItemRate[2].maxProb = 99;

            ItemRate[3].minProb = 100;
            ItemRate[3].maxProb = 100;
        }

        if (Lvl == 3)
        {
            ItemRate[0].minProb = 0;
            ItemRate[0].maxProb = 62;

            ItemRate[1].minProb = 63;
            ItemRate[1].maxProb = 85;

            ItemRate[2].minProb = 86;
            ItemRate[2].maxProb = 99;

            ItemRate[3].minProb = 100;
            ItemRate[3].maxProb = 100;
        }

        if (Lvl == 4)
        {
            ItemRate[0].minProb = 0;
            ItemRate[0].maxProb = 50;

            ItemRate[1].minProb = 51;
            ItemRate[1].maxProb = 86;

            ItemRate[2].minProb = 87;
            ItemRate[2].maxProb = 97;

            ItemRate[3].minProb = 98;
            ItemRate[3].maxProb = 99;
        }

        if (Lvl == 5)
        {
            ItemRate[0].minProb = 0;
            ItemRate[0].maxProb = 35;

            ItemRate[1].minProb = 36;
            ItemRate[1].maxProb = 74;

            ItemRate[2].minProb = 75;
            ItemRate[2].maxProb = 96;

            ItemRate[3].minProb = 97;
            ItemRate[3].maxProb = 99;
        }

        if (Lvl == 6)
        {
            ItemRate[0].minProb = 0;
            ItemRate[0].maxProb = 30;

            ItemRate[1].minProb = 31;
            ItemRate[1].maxProb = 70;

            ItemRate[2].minProb = 71;
            ItemRate[2].maxProb = 90;

            ItemRate[3].minProb = 91;
            ItemRate[3].maxProb = 99;
        }

        if (Lvl == 7)
        {
            ItemRate[0].minProb = 0;
            ItemRate[0].maxProb = 30;

            ItemRate[1].minProb = 31;
            ItemRate[1].maxProb = 55;

            ItemRate[2].minProb = 56;
            ItemRate[2].maxProb = 85;

            ItemRate[3].minProb = 86;
            ItemRate[3].maxProb = 99;
        }

        if (Lvl == 8)
        {
            ItemRate[0].minProb = 0;
            ItemRate[0].maxProb = 25;

            ItemRate[1].minProb = 26;
            ItemRate[1].maxProb = 47;

            ItemRate[2].minProb = 48;
            ItemRate[2].maxProb = 76;

            ItemRate[3].minProb = 77;
            ItemRate[3].maxProb = 99;
        }

        if (Lvl == 9)
        {
            ItemRate[0].minProb = 0;
            ItemRate[0].maxProb = 15;

            ItemRate[1].minProb = 16;
            ItemRate[1].maxProb = 27;

            ItemRate[2].minProb = 28;
            ItemRate[2].maxProb = 56;

            ItemRate[3].minProb = 57;
            ItemRate[3].maxProb = 99;
        }
    }
}
