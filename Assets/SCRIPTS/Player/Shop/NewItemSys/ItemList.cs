using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour
{
    public const float ZERO_ATTRIBUTE = 0f;
    public const float ItemConstChance = 5;

    public float CommonItem_MinRate, CommonItem_MaxRate;
    public float RareItem_MinRate, RareItem_MaxRate;
    public float MythicItem_MinRate, MythicItem_MaxRate;
    public float LegendaryItem_MinRate, LegendaryItem_MaxRate;

    public ItemUI UI;
    public PlayerSTAT PS;
    public List<Item> ItemResult;

    public Item AmuletOfResistance;
    public Item ArmorPlate;
    public Item BootsOfStrong;
    public Item Helmet;
    public ActiveItem Shield; //�������� �������
    public ActiveItem CronerChain; //�������� �������?
    public Item EnergyHelmet; //����?
    public Item MaskWithTubes; 
    public ActiveItem PlanB; //�������� �������
    public Item Cevlar;
    public Item Spike; //����?
    public Item SpikeHelmet; //����?
    public Item InfectedCevlar; //����?
    public ActiveItem TyranitBelt; //�������� �������?
    public Item Barrel;
    public Item Butt;
    public Item ChipStable;
    public Item ElectricCrown; //����?
    public Item Handle;
    public ActiveItem KnifeOfJustice; //�������� �������
    public Item Magazines;
    public Item Overlap;
    public Item StrangeCard;
    public Item AcidBullet; //����
    public Item ChipSight; //����?
    public Item DarkNeedle;
    public Item JawMod; //����?
    public ActiveItem SectantCloak;
    public Item ThermalSensor; //����?
    public Item UpgradedMagazines;
    public Item BarrelMark2; //����?
    public Item EnergyHand; //����?
    public ActiveItem Mirror; //�������� �������
    public Item ProBelt;
    public Item SpeedyBoots;
    public ActiveItem EritondEye; //�������� �������
    public Item MIZEmblem;
    public ActiveItem EnergyDrink; //�������� �������
    public Item GemOfLife;
    public Item GloryPoster;
    public Item InfectedSkull; //����?
    public Item SecretEmblem;
    public ActiveItem HealthRing; //�������� �������
    public Item Helper;
    public Item ProBoot;
    public Item SectantBook; //����?
    public Item BrainMod; //����?
    public Item TyranitGlasses;
    public Item NecklesOfNature;

    public void Start() //Init(ItemID, HP, MaxHP, HP_RegenAmount, Armor, DMG, DMGCrit, ReloadTime, Spread, MovementSpeed)
    {
        UpdateItems();
    }

    public void UpdateItems()
    {
        #region BlueCard

        AmuletOfResistance = new Item(Item.Color.Blue, false, 100, 150, 150, ZERO_ATTRIBUTE, 2, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[0], UI.ItemQualityIcon[0], "Amulet Of Resistance", "+ 2 armor \n +150 HP",
                                      BuffList.NO_BUFF, new ItemRate(CommonItem_MinRate, CommonItem_MaxRate));

        ArmorPlate = new Item(Item.Color.Blue, false, 101, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 2, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[1], UI.ItemQualityIcon[0], "Armor plate", "+2 armor",
                                      BuffList.NO_BUFF, new ItemRate(CommonItem_MinRate, CommonItem_MaxRate));

        BootsOfStrong = new Item(Item.Color.Blue, false, 102, 250, 250, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 0.2f, UI.ItemIcon[2], UI.ItemQualityIcon[0], "Boots of strong", "+50 movement speed \n +250 HP",
                                      BuffList.NO_BUFF, new ItemRate(CommonItem_MinRate, CommonItem_MaxRate));

        Helmet = new Item(Item.Color.Blue, false, 103, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 3, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[3], UI.ItemQualityIcon[0], "Helmet", "+3 armor",
                                      BuffList.NO_BUFF, new ItemRate(CommonItem_MinRate, CommonItem_MaxRate));

        Shield = new ActiveItem(Item.Color.Blue, 104, true, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[4], UI.ItemQualityIcon[0],
                                      "Shield", "Activate: Blocks incoming damage, you cannot shoot and use abilities \n Usage time: 2 sec \n Cooldown: 10 sec.",
                                      BuffList.ShieldBuff, new ItemRate(CommonItem_MinRate, CommonItem_MaxRate), 15, true, false, 3, UI.Audio[1], UI.UIEffect[0], UI.PlayerEffect[1]);

        CronerChain = new ActiveItem(Item.Color.Blue, 115, true, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 2, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[5], UI.ItemQualityIcon[1],
                                      "Croner Chain", "When a critical hit creates a shield that protects the player for a 2 seconds \n +2 armor \n Cooldown: 40 sec.",
                                      BuffList.CronerChainBuff, new ItemRate(RareItem_MinRate, RareItem_MaxRate), 35, true, true, 3, UI.Audio[2], UI.UIEffect[1], UI.PlayerEffect[2]);

        EnergyHelmet = new Item(Item.Color.Blue, false, 116, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 3, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[6], UI.ItemQualityIcon[1], "Energy helmet",
                                      "When taking damage, there is a chance to emit an energy charge that deals damage to the area. \n +3 armor",
                                      BuffList.EnergyHelmetBuff, new ItemRate(RareItem_MinRate, RareItem_MaxRate));

        MaskWithTubes = new Item(Item.Color.Blue, false, 117, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 3, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[7], UI.ItemQualityIcon[1], "Mask With Tubes", "Reducing time of negative effects \n +2 armor",
                                      BuffList.MaskWithTubesBuff, new ItemRate(RareItem_MinRate, RareItem_MaxRate));

        PlanB = new ActiveItem(Item.Color.Blue, 118, true, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[8], UI.ItemQualityIcon[1],
                                      "Plan B", "Activate: Activated armor that blocks damage by immobilizing the wearer \n Usage time: 3 sec. \n Cooldown: 25 sec.",
                                      BuffList.PlanB_Buff, new ItemRate(RareItem_MinRate, RareItem_MaxRate), 25, true, false, 3, UI.Audio[3], UI.UIEffect[8], UI.PlayerEffect[2]);

        Cevlar = new Item(Item.Color.Blue, false, 129, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 5, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[9], UI.ItemQualityIcon[2], "Cevlar", "+5 armor", BuffList.NO_BUFF, new ItemRate(MythicItem_MinRate, MythicItem_MaxRate));

        Spike = new Item(Item.Color.Blue, false, 1210, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 4, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 0.1f, UI.ItemIcon[10], UI.ItemQualityIcon[2], "Spike", "Damage Return \n +4 armor \n -25 Movement speed",
                                      BuffList.SpikeBuff, new ItemRate(MythicItem_MinRate, MythicItem_MaxRate));

        SpikeHelmet = new Item(Item.Color.Blue, false, 1211, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 2, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[11], UI.ItemQualityIcon[2], "Spike Helmet", "Damage Return. \n +2 armor",
                                      BuffList.SpikeBuff, new ItemRate(MythicItem_MinRate, MythicItem_MaxRate));

        InfectedCevlar = new Item(Item.Color.Blue, false, 1312, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 3, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[12], UI.ItemQualityIcon[3], "Infected Cevlar", "Blocks 40% of incoming damage. \n +3 armor",
                                      BuffList.InfectedCevlarBuff, new ItemRate(LegendaryItem_MinRate, LegendaryItem_MaxRate));

        TyranitBelt = new ActiveItem(Item.Color.Blue, 1313, true, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 2, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 0.1f, UI.ItemIcon[13], UI.ItemQualityIcon[3],
                                      "Tyranit Belt", "Blocks any ability. \n +2 armor \n -25 Movement speed",
                                      BuffList.TyranitBeltBuff, new ItemRate(LegendaryItem_MinRate, LegendaryItem_MaxRate), 45, true, true, 3, UI.Audio[4], UI.UIEffect[3], UI.PlayerEffect[4]);
        #endregion
        #region RedCard
        Barrel = new Item(Item.Color.Red, false, 200, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 0.08f, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[14], UI.ItemQualityIcon[0], "Barrel", "+2 shooting accuracy",
                                      BuffList.NO_BUFF, new ItemRate(CommonItem_MinRate, CommonItem_MaxRate));
        Butt = new Item(Item.Color.Red, false, 202, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 0.05f, 0.08f,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[16], UI.ItemQualityIcon[0], "Butt", "+1 shooting accuracy \n +1 fire rate",
                                      BuffList.NO_BUFF, new ItemRate(CommonItem_MinRate, CommonItem_MaxRate));

        ChipStable = new Item(Item.Color.Red, false, 203, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 0.1f, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[17], UI.ItemQualityIcon[0], "ChipStable", "+2 shooting accuracy",
                                      BuffList.NO_BUFF, new ItemRate(CommonItem_MinRate, CommonItem_MaxRate));

        ElectricCrown = new Item(Item.Color.Red, false, 204, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 6, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[18], UI.ItemQualityIcon[0], "ElectricCrown", "Victim emits a charge that deals damage to AOE \n +6 DMG",
                                      BuffList.ElectricCrownBuff, new ItemRate(CommonItem_MinRate, CommonItem_MaxRate));

        Handle = new Item(Item.Color.Red, false, 205, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 0.1f, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[19], UI.ItemQualityIcon[0], "Handle", "+1 fire rate",
                                      BuffList.NO_BUFF, new ItemRate(CommonItem_MinRate, CommonItem_MaxRate));

        KnifeOfJustice = new ActiveItem(Item.Color.Red, 206, true, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[20], UI.ItemQualityIcon[0],
                                      "KnifeOfJustice", "Activate: Throws a knife that deals damage and bleeding \n Cooldown: 10 sec.",
                                      BuffList.NO_BUFF, new ItemRate(CommonItem_MinRate, CommonItem_MaxRate), 25, true, false, 0, UI.Audio[0], UI.UIEffect[0], UI.PlayerEffect[0]);

        Magazines = new Item(Item.Color.Red, false, 207, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, 15, ZERO_ATTRIBUTE, UI.ItemIcon[21], UI.ItemQualityIcon[0], "Magazine", "+15 bullets",
                                      BuffList.NO_BUFF, new ItemRate(CommonItem_MinRate, CommonItem_MaxRate));

        Overlap = new Item(Item.Color.Red, false, 208, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 0.03f, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[22], UI.ItemQualityIcon[0], "Overlap", "+2 shooting accuracy",
                                      BuffList.NO_BUFF, new ItemRate(CommonItem_MinRate, CommonItem_MaxRate));

        StrangeCard = new Item(Item.Color.Red, false, 209, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 10, 5, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[23], UI.ItemQualityIcon[0], "StrangeCard", "Increases chance of a critical attack /n +10 DMG",
                                      BuffList.NO_BUFF, new ItemRate(CommonItem_MinRate, CommonItem_MaxRate));

        AcidBullet = new Item(Item.Color.Red, false, 2110, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 10, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[24], UI.ItemQualityIcon[1], "AcidBullet", "Shots poison the victim \n +10 DMG",
                                      BuffList.PoisonBuff, new ItemRate(RareItem_MinRate, RareItem_MaxRate)); //���� ����

        ChipSight = new Item(Item.Color.Red, false, 2111, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 5, ZERO_ATTRIBUTE, 0.05f, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[25], UI.ItemQualityIcon[1], "ChipSight", "+2 shooting accuracy \n Increases chance of a critical attack \n Increases FOV",
                                      BuffList.ChipSightBuff, new ItemRate(RareItem_MinRate, RareItem_MaxRate));

        DarkNeedle = new Item(Item.Color.Red, false, 2112, ZERO_ATTRIBUTE, -150, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 15, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 0.2f, UI.ItemIcon[26], UI.ItemQualityIcon[1], "DarkNeedle", "+15 DMG \n -150 HP \n +15 Movement speed",
                                      BuffList.NO_BUFF, new ItemRate(RareItem_MinRate, RareItem_MaxRate));

        JawMod = new Item(Item.Color.Red, false, 2113, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 10, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[27], UI.ItemQualityIcon[1], "JawMod", "+10 DMG \n Vampirism buff \n Shoots cause bleeding",
                                      BuffList.BleedBuff, new ItemRate(RareItem_MinRate, RareItem_MaxRate));

        SectantCloak = new ActiveItem(Item.Color.Red, 2114, true, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[28], UI.ItemQualityIcon[1],
                                      "SectantCloak", "Activate: Makes invisible for 2 seconds \n Cooldown: 40 sec.",
                                      BuffList.SectantCloackBuff, new ItemRate(RareItem_MinRate, RareItem_MaxRate), 40, true, false, 3, UI.Audio[5], UI.UIEffect[4], UI.PlayerEffect[5]);

        ThermalSensor = new Item(Item.Color.Red, false, 2115, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 5, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[29], UI.ItemQualityIcon[1], "ThermalSensor", "Chance to deal additional AOE damage \n +5 DMG",
                                      BuffList.ThermalSensorBuff, new ItemRate(RareItem_MinRate, RareItem_MaxRate));

        UpgradedMagazines = new Item(Item.Color.Red, false, 2116, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, 30, ZERO_ATTRIBUTE, UI.ItemIcon[30], UI.ItemQualityIcon[1], "UpgradedMagazines", "+30 bullets",
                                      BuffList.NO_BUFF, new ItemRate(RareItem_MinRate, RareItem_MaxRate));

        BarrelMark2 = new Item(Item.Color.Red, false, 2217, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 0.1f,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[31], UI.ItemQualityIcon[2], "BarrelMark2", "Vampirism buff \n Shoots cause bleeding \n + 1 fire rate",
                                      BuffList.BleedBuff, new ItemRate(MythicItem_MinRate, MythicItem_MaxRate));

        EnergyHand = new Item(Item.Color.Red, false, 2218, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 10, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[32], UI.ItemQualityIcon[2], "EnergyHand", "Deals AOE damage when reloading \n +10 DMG",
                                      BuffList.EnergyHandsBuff, new ItemRate(MythicItem_MinRate, MythicItem_MaxRate));

        Mirror = new ActiveItem(Item.Color.Red, 2219, true, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[33], UI.ItemQualityIcon[2],
                                      "Mirror", "Activate: Turns to stone and makes immune to damage \n Cooldown: 45 sec.",
                                      BuffList.MirrorBuff, new ItemRate(MythicItem_MinRate, MythicItem_MaxRate), 45, true, false, 3, UI.Audio[6], UI.UIEffect[5], UI.PlayerEffect[6]);

        ProBelt = new Item(Item.Color.Red, false, 2220, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 10, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 0.03f, 0.1f,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[34], UI.ItemQualityIcon[2], "ProBelt", "+2 shooting accuracy \n +2 fire rate \n + 10 DMG",
                                      BuffList.NO_BUFF, new ItemRate(MythicItem_MinRate, MythicItem_MaxRate));

        SpeedyBoots = new Item(Item.Color.Red, false, 2221, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 15, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, 0.5f, ZERO_ATTRIBUTE, 0.1f, UI.ItemIcon[35], UI.ItemQualityIcon[2], "SpeedyBoots", "+25 movement speed \n +2 fire rate \n +15 DMG",
                                      BuffList.NO_BUFF, new ItemRate(MythicItem_MinRate, MythicItem_MaxRate));

        EritondEye = new ActiveItem(Item.Color.Red, 2322, true, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[36], UI.ItemQualityIcon[3],
                                      "EritondEye", "Activate: Immortality \n +25 dmg \n +250 HP \n Destroying at the second death \n Cooldown: 60 sec.",
                                      BuffList.EritondEyeBuff, new ItemRate(LegendaryItem_MinRate, LegendaryItem_MaxRate), 60, true, false, 3, UI.Audio[7], UI.UIEffect[6], UI.PlayerEffect[7]);

        MIZEmblem = new Item(Item.Color.Red, false, 2323, 250, 250, ZERO_ATTRIBUTE, 5, 25, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 0.1f, 0.1f,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[37], UI.ItemQualityIcon[3], "MIZEmblem", "+25 DMG \n +5 armor \n +250 HP \n +2 fire rate \n +2 shooting accuracy",
                                      BuffList.NO_BUFF, new ItemRate(LegendaryItem_MinRate, LegendaryItem_MaxRate));
        #endregion
        #region GreenCard
        EnergyDrink = new ActiveItem(Item.Color.Green, 300, true, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[38], UI.ItemQualityIcon[0],
                                      "EnergyDrink", "Activate: -50 HP, +2 movement speed \n cooldown: 10 sec.",
                                      BuffList.EnergyDrinkBuff, new ItemRate(CommonItem_MinRate, CommonItem_MaxRate), 10, true, false, 5, UI.Audio[8], UI.UIEffect[7], UI.PlayerEffect[8]);

        GemOfLife = new Item(Item.Color.Green, false, 301, 150, 150, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[39], UI.ItemQualityIcon[0], "GemOfLife", "+150 HP",
                                      BuffList.NO_BUFF, new ItemRate(CommonItem_MinRate, CommonItem_MaxRate));

        GloryPoster = new Item(Item.Color.Green, false, 302, 250, 250, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[40], UI.ItemQualityIcon[0], "GloryPoster", "+250 HP",
                                      BuffList.NO_BUFF, new ItemRate(CommonItem_MinRate, CommonItem_MaxRate));

        InfectedSkull = new ActiveItem(Item.Color.Green, 303, true, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[41], UI.ItemQualityIcon[0], "InfectedSkull", "Kills increase HP regeneration",
                                      BuffList.InfectedSkullBuff, new ItemRate(CommonItem_MinRate, CommonItem_MaxRate), ZERO_ATTRIBUTE, true, true, ZERO_ATTRIBUTE, null, null, null);

        SecretEmblem = new Item(Item.Color.Green, false, 304, ZERO_ATTRIBUTE, -150, 10, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[42], UI.ItemQualityIcon[0], "SecretEmblem", "-150 HP \n Increase HP regeneration",
                                      BuffList.NO_BUFF, new ItemRate(CommonItem_MinRate, CommonItem_MaxRate));

        HealthRing = new ActiveItem(Item.Color.Green, 315, true, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[43], UI.ItemQualityIcon[1],
                                      "HealthRing", "Activate: +300 HP \n Cooldown: 25 sec.",
                                      BuffList.HealthRingBuff, new ItemRate(RareItem_MinRate, RareItem_MaxRate), 25, true, false, 5, UI.Audio[9], UI.UIEffect[0], UI.PlayerEffect[0]);

        Helper = new Item(Item.Color.Green, false, 316, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 5, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[44], UI.ItemQualityIcon[1], "Helper", "Increase HP regeneration",
                                      BuffList.NO_BUFF, new ItemRate(RareItem_MinRate, RareItem_MaxRate));

        ProBoot = new Item(Item.Color.Green, false, 317, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 5, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 0.2f, UI.ItemIcon[45], UI.ItemQualityIcon[1], "ProBoot", "+50 movement speed \n +Increase HP regeneration",
                                      BuffList.NO_BUFF, new ItemRate(RareItem_MinRate, RareItem_MaxRate));

        SectantBook = new Item(Item.Color.Green, false, 318, ZERO_ATTRIBUTE, -150, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[46], UI.ItemQualityIcon[1], "SectantBook", "-150 HP \n Vampirism",
                                      BuffList.SectantBookBuff, new ItemRate(RareItem_MinRate, RareItem_MaxRate));

        BrainMod = new Item(Item.Color.Green, false, 319, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[47], UI.ItemQualityIcon[1], "BrainMod", "Increase buff time",
                                      BuffList.BrainModBuff, new ItemRate(RareItem_MinRate, RareItem_MaxRate));

        TyranitGlasses = new Item(Item.Color.Green, false, 3210, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, 10, 5, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[48], UI.ItemQualityIcon[2], "TyranitGlasses", "+5 armor \n Increase HP regeneration",
                                      BuffList.NO_BUFF, new ItemRate(MythicItem_MinRate, MythicItem_MaxRate));

        NecklesOfNature = new Item(Item.Color.Green, false, 3311, 300, 300, 10, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, ZERO_ATTRIBUTE,
                                      ZERO_ATTRIBUTE, ZERO_ATTRIBUTE, UI.ItemIcon[49], UI.ItemQualityIcon[3], "NecklesOfNature", "+300 HP \n Increase HP Regeneration",
                                      BuffList.NO_BUFF, new ItemRate(LegendaryItem_MinRate, LegendaryItem_MaxRate));
        #endregion
    }

    public void ItemChance()
    {
        CommonItem_MaxRate -= ItemConstChance;

        RareItem_MinRate -= ItemConstChance;
        RareItem_MaxRate -= ItemConstChance;

        MythicItem_MinRate -= ItemConstChance;
        MythicItem_MaxRate -= ItemConstChance;

        LegendaryItem_MinRate -= ItemConstChance;
        LegendaryItem_MaxRate -= ItemConstChance;

    }

    public List<Item> GetItemList() 
    {
        ItemResult = new List<Item>(){AmuletOfResistance, ArmorPlate, BootsOfStrong, Helmet, Shield, CronerChain, EnergyHelmet,
        MaskWithTubes,
        PlanB,
        Cevlar,
        Spike,
        SpikeHelmet,
        InfectedCevlar,
        TyranitBelt,
        Barrel,
        Butt,
        ChipStable,
        ElectricCrown,
        Handle,
        KnifeOfJustice,
        Magazines,
        Overlap,
        StrangeCard,
        AcidBullet,
        ChipSight,
        DarkNeedle,
        JawMod,
        SectantCloak,
        ThermalSensor,
        UpgradedMagazines,
        BarrelMark2,
        EnergyHand,
        Mirror,
        ProBelt,
        SpeedyBoots,
        EritondEye,
        MIZEmblem,
        EnergyDrink,
        GemOfLife,
        GloryPoster,
        InfectedSkull,
        SecretEmblem,
        HealthRing,
        Helper,
        ProBoot,
        SectantBook,
        BrainMod,
        TyranitGlasses,
        NecklesOfNature};
        return ItemResult;
    } 
}
