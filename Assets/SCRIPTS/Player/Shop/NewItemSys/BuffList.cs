using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BuffList
{
    public static Buff NO_BUFF = new Buff(Buff.BuffID.No_Buff, 0, 0, 0, false);
    public static Buff BleedBuff = new Buff(Buff.BuffID.Bleed_Buff, 5, 1, 3, false);
    public static Buff BleedDebuff = new Buff(Buff.BuffID.Bleed_Debuff, 5, 1, 3, true);
    public static Buff PoisonBuff = new Buff(Buff.BuffID.Poison_Buff, 5, 2, 4, false);
    public static Buff PoisonDeBuff = new Buff(Buff.BuffID.Poison_Debuff, 5, 2, 4, true);
    public static Buff ElectricCrownBuff = new Buff(Buff.BuffID.ElectricCrown_Buff, 5, 2, 2, false);
    public static Buff EnergyHelmetBuff = new Buff(Buff.BuffID.EnergyHelmet_Buff, 0, 0, 2, false);
    public static Buff ElectricDebuff = new Buff(Buff.BuffID.Electric_Debuff, 5, 2, 2, true);
    public static Buff ShieldBuff = new Buff(Buff.BuffID.Shield_Buff, 3, 0, 0, false);
    public static Buff CronerChainBuff = new Buff(Buff.BuffID.CronerChain_Buff, 3, 0, 1, false);
    public static Buff PlanB_Buff = new Buff(Buff.BuffID.PlanB_Buff, 3, 0, 8, false);
    public static Buff SpikeBuff = new Buff(Buff.BuffID.Spike_Buff, 0, 0, 0, false);
    public static Buff InfectedCevlarBuff = new Buff(Buff.BuffID.InfectedCevlar_Buff, 0, 0, 0, false);
    public static Buff TyranitBeltBuff = new Buff(Buff.BuffID.TyranitBelt_Buff, 45, 0, 0, false);
    public static Buff SectantCloackBuff = new Buff(Buff.BuffID.SectantCloak_Buff, 2, 0, 5, false);
    public static Buff MirrorBuff = new Buff(Buff.BuffID.Mirror_Buff, 3, 0, 6, false);
    public static Buff EritondEyeBuff = new Buff(Buff.BuffID.EritondEye_Buff, 3, 0, 7, false);
    public static Buff EnergyDrinkBuff = new Buff(Buff.BuffID.EnergyDrink_Buff, 3, 4, 0, false);
    public static Buff HealthRingBuff = new Buff(Buff.BuffID.HealthRing_Buff, 3, 4, 0, false);
    public static Buff SectantBookBuff = new Buff(Buff.BuffID.SectantBook_Buff, 0, 0, 0, false);
    public static Buff ThermalSensorBuff = new Buff(Buff.BuffID.ThermalSensor_Buff, 0, 0, 0, false);
    public static Buff EnergyHandsBuff = new Buff(Buff.BuffID.EnergyHand_Buff, 0, 0, 0, false);
    public static Buff InfectedSkullBuff = new Buff(Buff.BuffID.InfectedSkull_Buff, 0, 0, 0, false);
    public static Buff MaskWithTubesBuff = new Buff(Buff.BuffID.MaskWithTubes_Buff, 0, 0, 0, false);
    public static Buff ChipSightBuff = new Buff(Buff.BuffID.ChipSight_Buff, 0, 0, 0, false);
    public static Buff BrainModBuff = new Buff(Buff.BuffID.BrainMod_Buff, 0, 0, 0, false);
    public static Buff LightBossBuff = new Buff(Buff.BuffID.LightBoss_Buff, 150, 200, 0, false);
    public static Buff DarkBossBuff = new Buff(Buff.BuffID.DarkBoss_Buff, 150, 200, 0, false);
}
