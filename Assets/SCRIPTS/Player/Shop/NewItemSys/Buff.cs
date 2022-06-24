using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : ScriptableObject
{
    public enum BuffID : int
    {
        No_Buff = 0,
        Bleed_Buff = 1,
        Poison_Buff = 2,
        ElectricCrown_Buff = 3,
        Shield_Buff = 4,
        CronerChain_Buff = 5,
        PlanB_Buff = 6,
        TyranitBelt_Buff = 7,
        KnifeOfJustice_Buff = 8,
        SectantCloak_Buff = 9,
        Mirror_Buff = 10,
        EritondEye_Buff = 11,
        EnergyDrink_Buff = 12,
        HealthRing_Buff = 13,
        Bleed_Debuff = 14,
        Poison_Debuff = 15,
        Electric_Debuff = 16,
        SoldierHeal_Buff = 17,
        EnergyHand_Buff = 18,
        EnergyHelmet_Buff = 19,
        Spike_Buff = 20,
        InfectedCevlar_Buff = 21,
        SectantBook_Buff = 22,
        ThermalSensor_Buff = 23,
        InfectedSkull_Buff = 24,
        MaskWithTubes_Buff = 25,
        ChipSight_Buff = 26,
        BrainMod_Buff = 27,
        LightBoss_Buff = 28
    }

    public BuffID BuffType;
    public float Time;
    public float DeltaTime;
    public int UIEffectID;
    public bool NegativeEffect;
    public Buff(BuffID BuffID, float Time, float DeltaTime, int UIEffectID, bool NegativeEffect)
    {
        this.BuffType = BuffID;
        this.Time = Time;
        this.DeltaTime = DeltaTime;
        this.UIEffectID = UIEffectID;
        this.NegativeEffect = NegativeEffect;
    }
}
