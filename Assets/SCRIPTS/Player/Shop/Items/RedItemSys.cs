using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedItemSys : MonoBehaviour
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
        ShopUI.Play("SwitchRed");
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
        if (ItemID == 200)
        {
            ItemDisplay.sprite = ItemIcon[0];
            QualityDisplay.sprite = QualityIcon[0];
            ItemName.text = "Barrel";
            ItemDescription.text = "+2 shooting accuracy";
        }

        if (ItemID == 201)
        {
            ItemDisplay.sprite = ItemIcon[1];
            QualityDisplay.sprite = QualityIcon[0];
            ItemName.text = "BraceletOfChoice";
            ItemDescription.text = "Improves one of the characteristics: \n +10 DMG \n +15 Movement Speed \n +3 armor";
        }

        if (ItemID == 202)
        {
            ItemDisplay.sprite = ItemIcon[2];
            QualityDisplay.sprite = QualityIcon[0];
            ItemName.text = "Butt";
            ItemDescription.text = "+1 shooting accuracy \n +1 fire rate";
        }

        if (ItemID == 203)
        {
            ItemDisplay.sprite = ItemIcon[3];
            QualityDisplay.sprite = QualityIcon[0];
            ItemName.text = "ChipStable";
            ItemDescription.text = "+2 shooting accuracy";
        }

        if (ItemID == 204)
        {
            ItemDisplay.sprite = ItemIcon[4];
            QualityDisplay.sprite = QualityIcon[0];
            ItemName.text = "ElectricCrown";
            ItemDescription.text = "Victim emits a charge that deals damage to AOE \n +6 DMG";
        }

        if (ItemID == 205)
        {
            ItemDisplay.sprite = ItemIcon[5];
            QualityDisplay.sprite = QualityIcon[0];
            ItemName.text = "Handle";
            ItemDescription.text = "+1 fire rate";
        }

        if (ItemID == 206)
        {
            ItemDisplay.sprite = ItemIcon[6];
            QualityDisplay.sprite = QualityIcon[0];
            ItemName.text = "KnifeOfJustice";
            ItemDescription.text = "Activate: Throws a knife that deals damage and bleeding \n Cooldown: 10 sec.";
        }

        if (ItemID == 207)
        {
            ItemDisplay.sprite = ItemIcon[7];
            QualityDisplay.sprite = QualityIcon[0];
            ItemName.text = "Magazines";
            ItemDescription.text = "+15 bullets";
        }

        if (ItemID == 208)
        {
            ItemDisplay.sprite = ItemIcon[8];
            QualityDisplay.sprite = QualityIcon[0];
            ItemName.text = "Overlap";
            ItemDescription.text = "+2 shooting accuracy";
        }

        if (ItemID == 209)
        {
            ItemDisplay.sprite = ItemIcon[9];
            QualityDisplay.sprite = QualityIcon[0];
            ItemName.text = "StrangeCard";
            ItemDescription.text = "Increases chance of a critical attack";
        }

        if (ItemID == 2110)
        {
            ItemDisplay.sprite = ItemIcon[10];
            QualityDisplay.sprite = QualityIcon[1];
            ItemName.text = "AcidBullet";
            ItemDescription.text = "Shots poison the victim \n +10 DMG";
        }

        if (ItemID == 2111)
        {
            ItemDisplay.sprite = ItemIcon[11];
            QualityDisplay.sprite = QualityIcon[1];
            ItemName.text = "ChipSight";
            ItemDescription.text = "+2 shooting accuracy \n Increases chance of a critical attack \n Increases FOV";
        }

        if (ItemID == 2112)
        {
            ItemDisplay.sprite = ItemIcon[12];
            QualityDisplay.sprite = QualityIcon[1];
            ItemName.text = "DarkNeedle";
            ItemDescription.text = "+15 DMG \n -150 HP \n +15 Movement speed";
        }

        if (ItemID == 2113)
        {
            ItemDisplay.sprite = ItemIcon[13];
            QualityDisplay.sprite = QualityIcon[1];
            ItemName.text = "JawMod";
            ItemDescription.text = "+10 DMG \n Vampirism buff \n Shoots cause bleeding";
        }

        if (ItemID == 2114)
        {
            ItemDisplay.sprite = ItemIcon[14];
            QualityDisplay.sprite = QualityIcon[1];
            ItemName.text = "SectantCloak";
            ItemDescription.text = "Activate: Makes invisible for 2 seconds \n Cooldown: 40 sec.";
        }

        if (ItemID == 2115)
        {
            ItemDisplay.sprite = ItemIcon[15];
            QualityDisplay.sprite = QualityIcon[1];
            ItemName.text = "ThermalSensor";
            ItemDescription.text = "Chance to deal additional AOE damage \n +5 DMG";
        }

        if (ItemID == 2116)
        {
            ItemDisplay.sprite = ItemIcon[16];
            QualityDisplay.sprite = QualityIcon[1];
            ItemName.text = "UpgradedMagazines";
            ItemDescription.text = "+30 bullets";
        }

        if (ItemID == 2217)
        {
            ItemDisplay.sprite = ItemIcon[17];
            QualityDisplay.sprite = QualityIcon[2];
            ItemName.text = "BarrelMark2";
            ItemDescription.text = "Vampirism buff \n Shoots cause bleeding \n + 1 fire rate";
        }

        if (ItemID == 2218)
        {
            ItemDisplay.sprite = ItemIcon[18];
            QualityDisplay.sprite = QualityIcon[2];
            ItemName.text = "EnergyHand";
            ItemDescription.text = "Deals AOE damage when reloading \n +10 DMG";
        }

        if (ItemID == 2219)
        {
            ItemDisplay.sprite = ItemIcon[19];
            QualityDisplay.sprite = QualityIcon[2];
            ItemName.text = "Mirror";
            ItemDescription.text = "Activate: Turns to stone and makes immune to damage \n Cooldown: 45 sec.";
        }

        if (ItemID == 2220)
        {
            ItemDisplay.sprite = ItemIcon[20];
            QualityDisplay.sprite = QualityIcon[2];
            ItemName.text = "ProBelt";
            ItemDescription.text = "+2 shooting accuracy \n +2 fire rate \n + 10 DMG";
        }

        if (ItemID == 2221)
        {
            ItemDisplay.sprite = ItemIcon[21];
            QualityDisplay.sprite = QualityIcon[2];
            ItemName.text = "SpeedyBoots";
            ItemDescription.text = "+25 movement speed \n +2 fire rate \n +15 DMG";
        }

        if (ItemID == 2322)
        {
            ItemDisplay.sprite = ItemIcon[22];
            QualityDisplay.sprite = QualityIcon[3];
            ItemName.text = "EritondEye";
            ItemDescription.text = "Activate: Immortality \n +25 dmg \n +250 HP \n Destroying at the second death \n Cooldown: 60 sec.";
        }

        if (ItemID == 2323)
        {
            ItemDisplay.sprite = ItemIcon[23];
            QualityDisplay.sprite = QualityIcon[3];
            ItemName.text = "MIZEmblem";
            ItemDescription.text = "+25 DMG \n +5 armor \n +250 HP \n +2 fire rate \n +2 shooting accuracy";
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
