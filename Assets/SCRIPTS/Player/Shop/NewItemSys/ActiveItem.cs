using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ActiveItem : Item
{
    public float Cooldown { set; get; }
    public bool Active { set; get; }
    public bool PassiveActiveItem { set; get; }
    public float Time { set; get; }
    public AudioClip Audio { set; get; }
    public GameObject UIEffect { set; get; }
    public GameObject PlayerEffect { set; get; }

    public ActiveItem()
    {

    }

    public ActiveItem(Color ColorName, 
                int NewItemID,
                bool isActiveItem,
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
                ItemRate ItemRate,
                float Cooldown,
                bool Active,
                bool PassiveActiveItem,
                float Time,
                AudioClip Audio,
                GameObject UIEffect,
                GameObject PlayerEffect) : base(ColorName,isActiveItem,NewItemID,HP_Amount,HP_MaxAmount,HPRegen_Amount,Armor_Amount,DMG_Amount,DMGCrit_Amount,RealodTime,Spread,ShootRate,MagazineSize,MovementSpeed,ItemIcon,ItemQuality,ItemName,ItemDescription,SomeBuff,ItemRate)
    {       
        this.Cooldown = Cooldown;
        this.Active = Active;
        this.PassiveActiveItem = PassiveActiveItem;
        this.Time = Time;
        this.Audio = Audio;
        this.UIEffect = UIEffect;
        this.PlayerEffect = PlayerEffect;
    }

    public ActiveItem(ActiveItem item) : base(item)
    {
        this.Cooldown = item.Cooldown;
        this.Active = item.Active;
        this.Time = item.Time;
        this.Audio = item.Audio;
        this.UIEffect = item.UIEffect;
        this.PlayerEffect = item.PlayerEffect;
    }

}
