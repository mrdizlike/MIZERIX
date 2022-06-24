using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewItemSys : MonoBehaviour
{
    public class Buff
    {
        public int BuffID;

        public Buff(int BuffID)
        {
            this.BuffID = BuffID;
        }
    }

    public class Item : MonoBehaviour
    {

        public int NewItemID { get; }
        public float HP_Amount { get; }
        public float HP_MaxAmount { get; }
        public float HPRegen_Amount { get; }
        private float HP_Regen_Time { get; }
        public float Armor_Amount { get; }
        public float DMG_Amount { get; }
        public float DMGCrit_Amount { get; }
        public float RealodTime { get; }
        public float Spread { get; }
        public float MovementSpeed { get; }
        public Sprite ItemIcon { get; }
        public Sprite ItemQuality { get; }
        public Text ItemName { get; }
        public Text ItemDescription { get; }
        public Buff SomeBuff { get; }

        public Item(int NewItemID,
                    float HP_Amount,
                    float HP_MaxAmount,
                    float HPRegen_Amount,
                    float HP_Regen_Time,
                    float Armor_Amount,
                    float DMG_Amount,
                    float DMGCrit_Amount,
                    float RealodTime,
                    float Spread,
                    float MovementSpeed,
                    Sprite ItemIcon,
                    Sprite ItemQuality,
                    Text ItemName,
                    Text ItemDescription,
                    Buff SomeBuff)

        {
            this.NewItemID = NewItemID;
            this.HP_Amount = HP_Amount;
            this.HP_MaxAmount = HP_MaxAmount;
            this.HPRegen_Amount = HPRegen_Amount;
            this.HP_Regen_Time = HP_Regen_Time;
            this.Armor_Amount = Armor_Amount;
            this.DMG_Amount = DMG_Amount;
            this.DMGCrit_Amount = DMGCrit_Amount;
            this.RealodTime = RealodTime;
            this.Spread = Spread;
            this.MovementSpeed = MovementSpeed;
            this.ItemIcon = ItemIcon;
            this.ItemQuality = ItemQuality;
            this.ItemName = ItemName;
            this.ItemDescription = ItemDescription;
            this.SomeBuff = SomeBuff;
        }
    }


    public class ActiveItem : Item
    {
        public float Cooldown { set; get; }
        public bool Active { set; get; }
        public float Time { set; get; }
        public AudioClip Audio { set; get; }
        public GameObject UIEffect { set; get; }
        public GameObject PlayerEffect { set; get; }

        public ActiveItem(int NewItemID,
                    float HP_Amount,
                    float HP_MaxAmount,
                    float HPRegen_Amount,
                    float HP_Regen_Time,
                    float Armor_Amount,
                    float DMG_Amount,
                    float DMGCrit_Amount,
                    float RealodTime,
                    float Spread,
                    float MovementSpeed,
                    Sprite ItemIcon,
                    Sprite ItemQuality,
                    Text ItemName,
                    Text ItemDescription,
                    Buff SomeBuff,
                    float Cooldown,
                    bool Active,
                    float Time,
                    AudioClip Audio,
                    GameObject UIEffect,
                    GameObject PlayerEffect) : base(NewItemID,
                        HP_Amount,
                        HP_MaxAmount,
                        HPRegen_Amount,
                        HP_Regen_Time,
                        Armor_Amount,
                        DMG_Amount,
                        DMGCrit_Amount,
                        RealodTime,
                        Spread,
                        MovementSpeed,
                        ItemIcon,
                        ItemQuality,
                        ItemName,
                        ItemDescription,
                        SomeBuff)
        {
            this.Cooldown = Cooldown;
            this.Active = Active;
            this.Time = Time;
            this.Audio = Audio;
            this.UIEffect = UIEffect;
            this.PlayerEffect = PlayerEffect;
        }
        private void ServerItemEffect(bool disable)
        {

        }
        private void ClientItemEffect(bool disable)
        {

        }
    }
}
