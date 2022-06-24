using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemRate
{
    public float minProb, maxProb;

    public ItemRate(float minProb, float maxProb)
    {
        this.minProb = minProb;
        this.maxProb = maxProb;
    }
}

public class Item : ScriptableObject
{
    public enum Color : int
    {
        Blue = 1,
        Red = 2,
        Green = 3
    }
    public Color ColorName;

    public bool isActiveItem;

    public int NewItemID { set; get; }
    public float HP_Amount { set; get; }
    public float HP_MaxAmount { set; get; }
    public float HPRegen_Amount { set; get; }
    public float Armor_Amount { set; get; }
    public float DMG_Amount { set; get; }
    public float DMGCrit_Amount { set; get; }
    public float RealodTime { set; get; }
    public float Spread { set; get; }
    public float ShootRate { set; get; }
    public float MagazineSize { set; get; }
    public float MovementSpeed { set; get; }
    public Sprite ItemIcon { set; get; }
    public Sprite ItemQuality { set; get; }
    public string ItemName { set; get; }
    public string ItemDescription { set; get; }

    public Buff SomeBuff;

    public ItemRate ItemRate;

    public Item()
    {

    }

    public Item(Color ColorName,
                bool isActiveItem,
                int NewItemID,
                float HP_Amount,
                float HP_MaxAmount,
                float HPRegen_Amount,
                float Armor_Amount,
                float DMG_Amount,
                float DMGCrit_Amount,
                float RealodTime,
                float Spread,
                float ShootRate,
                float MagazineSize,
                float MovementSpeed,
                Sprite ItemIcon,
                Sprite ItemQuality,
                string ItemName,
                string ItemDescription,
                Buff SomeBuff,
                ItemRate ItemRate)
    {
        this.ColorName = ColorName;
        this.NewItemID = NewItemID;
        this.isActiveItem = isActiveItem;
        this.HP_Amount = HP_Amount;
        this.HP_MaxAmount = HP_MaxAmount;
        this.HPRegen_Amount = HPRegen_Amount;
        this.Armor_Amount = Armor_Amount;
        this.DMG_Amount = DMG_Amount;
        this.DMGCrit_Amount = DMGCrit_Amount;
        this.RealodTime = RealodTime;
        this.Spread = Spread;
        this.ShootRate = ShootRate;
        this.MagazineSize = MagazineSize;
        this.MovementSpeed = MovementSpeed;
        this.ItemIcon = ItemIcon;
        this.ItemQuality = ItemQuality;
        this.ItemName = ItemName;
        this.ItemDescription = ItemDescription;
        this.SomeBuff = SomeBuff;
        this.ItemRate = ItemRate;
    }

    public Item(Item item)
    {
        this.ColorName = item.ColorName;
        this.NewItemID = item.NewItemID;
        this.HP_Amount = item.HP_Amount;
        this.HP_MaxAmount = item.HP_MaxAmount;
        this.HPRegen_Amount = item.HPRegen_Amount;
        this.Armor_Amount = item.Armor_Amount;
        this.DMG_Amount = item.DMG_Amount;
        this.DMGCrit_Amount = item.DMGCrit_Amount;
        this.RealodTime = item.RealodTime;
        this.Spread = item.Spread;
        this.ShootRate = item.ShootRate;
        this.MagazineSize = item.MagazineSize;
        this.MovementSpeed = item.MovementSpeed;
        this.ItemIcon = item.ItemIcon;
        this.ItemQuality = item.ItemQuality;
        this.ItemName = item.ItemName;
        this.ItemDescription = item.ItemDescription;
        this.SomeBuff = item.SomeBuff;
        this.ItemRate = item.ItemRate;
    }
}