using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreenItemSys : MonoBehaviour
{

    [Header("Scripts")]
    public Player_MAIN P_Main;
    public PlayerSTAT P_Stats;
    public ShopScript Shop;
    public GunScript Gun_Script;

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
        if(SwapCount == 0)
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
        ShopUI.Play("SwitchGreen");
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
            if (randomNum >= ItemRate[i].minProb && randomNum <= ItemRate[i].maxProb)
            {

                Debug.Log(randomNum + " ");

                if (i == 0)
                {
                    NewItemID = Common_ItemID[Random.Range(0, Common_ItemID.Length)];
                    ItemInformation(NewItemID);
                    if (CheckID == NewItemID)
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
        if (ItemID == 300)
        {
            ItemDisplay.sprite = ItemIcon[0];
            QualityDisplay.sprite = QualityIcon[0];
            ItemName.text = "EnergyDrink";
            ItemDescription.text = "Activate: -50 HP, +2 movement speed \n cooldown: 10 sec.";
        }

        if (ItemID == 301)
        {
            ItemDisplay.sprite = ItemIcon[1];
            QualityDisplay.sprite = QualityIcon[0];
            ItemName.text = "GemOfLife";
            ItemDescription.text = "+150 HP";
        }

        if (ItemID == 302)
        {
            ItemDisplay.sprite = ItemIcon[2];
            QualityDisplay.sprite = QualityIcon[0];
            ItemName.text = "GloryPoster";
            ItemDescription.text = "+250 HP";
        }

        if (ItemID == 303)
        {
            ItemDisplay.sprite = ItemIcon[3];
            QualityDisplay.sprite = QualityIcon[0];
            ItemName.text = "InfectedSkull";
            ItemDescription.text = "Kills increase HP regeneration";
        }

        if (ItemID == 304)
        {
            ItemDisplay.sprite = ItemIcon[4];
            QualityDisplay.sprite = QualityIcon[0];
            ItemName.text = "SecretEmblem";
            ItemDescription.text = "-150 HP \n Increase HP regeneration";
        }

        if (ItemID == 315)
        {
            ItemDisplay.sprite = ItemIcon[5];
            QualityDisplay.sprite = QualityIcon[1];
            ItemName.text = "HealthRing";
            ItemDescription.text = "Activate: +300 HP \n Cooldown: 25 sec.";
        }

        if (ItemID == 316)
        {
            ItemDisplay.sprite = ItemIcon[6];
            QualityDisplay.sprite = QualityIcon[1];
            ItemName.text = "Helper";
            ItemDescription.text = "Increase HP regeneration";
        }

        if (ItemID == 317)
        {
            ItemDisplay.sprite = ItemIcon[7];
            QualityDisplay.sprite = QualityIcon[1];
            ItemName.text = "ProBoot";
            ItemDescription.text = "+50 movement speed \n +Increase HP regeneration";
        }

        if (ItemID == 318)
        {
            ItemDisplay.sprite = ItemIcon[8];
            QualityDisplay.sprite = QualityIcon[1];
            ItemName.text = "SectantBook";
            ItemDescription.text = "-150 HP \n Vampirism";
        }

        if (ItemID == 329)
        {
            ItemDisplay.sprite = ItemIcon[9];
            QualityDisplay.sprite = QualityIcon[2];
            ItemName.text = "BrainMod";
            ItemDescription.text = "Increase buff time";
        }

        if (ItemID == 3210)
        {
            ItemDisplay.sprite = ItemIcon[10];
            QualityDisplay.sprite = QualityIcon[2];
            ItemName.text = "TyranitGlasses";
            ItemDescription.text = "+5 armor \n Increase HP regeneration";
        }

        if (ItemID == 3311)
        {
            ItemDisplay.sprite = ItemIcon[11];
            QualityDisplay.sprite = QualityIcon[3];
            ItemName.text = "NecklesOfNature";
            ItemDescription.text = "+300 HP \n Increase HP Regeneration";
        }
        
    }

    //void AntiRepeat()
    //{
    //    if (Shop.ItemID[0] == NewItemID)
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

    //    for (int i = 0;i < Shop.ItemID.Length; i++)
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
        if (Lvl == 2)
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
