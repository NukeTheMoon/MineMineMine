using System;
using UnityEngine;
using System.Collections;

public class UpgradeMenuManager : MonoBehaviour
{

    public int FirstLevelWeaponCost = 150000;
    public int SecondLevelWeaponCost = 300000;
    public int FirstLevelSecondaryCost = 50000;
    public int SecondLevelSecondaryCost = 100000;
    public int FirstLevelPulseCooldownDecreaseMs = 25;
    public int SecondLevelPulseCooldownDecreaseMs = 35;
    public int FirstLevelPulseMeterDecrease = 2;
    public int SecondLevelPulseMeterDecrease = 3;
    public int FirstLevelScattershotForceIncrease = 750;
    public int SecondLevelScattershotForceIncrease = 1250;
    public int FirstLevelScattershotTargetWidthIncrease = 1;
    public int SecondLevelScattershotTargetWidthIncrease = 1;
    public int FirstLevelScattershotMeterDecrease = 5;
    public int SecondLevelScattershotMeterDecrease = 10;
    public int FirstLevelRailgunLifetimeIncreaseMs = 75;
    public int SecondLevelRailgunLifetimeIncreaseMs = 125;
    public int FirstLevelRailgunMeterDecrease = 10;
    public int SecondLevelRailgunMeterDecrease = 15;
    public float FirstLevelEngineTurnForceIncrease = 0.05f;
    public float SecondLevelEngineTurnForceIncrease = 0.05f;
    public int FirstLevelBoostMeterDecrease = 5;
    public int SecondLevelBoostMeterDecrease = 10;
    public int FirstLevelShieldMeterPerSecondDecrease = 20;
    public int SecondLevelShieldMeterPerSecondDecrease = 30;
    public float FirstLevelShieldRadiusMultiplierIncrease = 0.1f;
    public float SecondLevelShieldRadiusMultiplierIncrease = 0.15f;

    public event EventHandler OnPulseEmitterUpgrade = delegate { };
    public event EventHandler OnScattershotUpgrade = delegate { };
    public event EventHandler OnRailgunUpgrade = delegate { };
    public event EventHandler OnEngineUpgrade = delegate { };
    public event EventHandler OnShieldUpgrade = delegate { };

    public int PulseEmitterLevel { get; private set; }
    public int ScattershotLevel { get; private set; }
    public int RailgunLevel { get; private set; }
    public int EngineLevel { get; private set; }
    public int ShieldLevel { get; private set; }

    public const int MaxLevel = 2;

    private void Awake()
    {
        RegisterWithSceneReference();
    }

    private void RegisterWithSceneReference()
    {
        SceneReference.UpgradeMenuManager = this;
    }

    public void UpgradePulseEmitter()
    {
        if (PulseEmitterLevel == 0 && SceneReference.ScorekeepingManager.TrySpend(FirstLevelWeaponCost))
        {
            SceneReference.WeaponManager.PulseCooldownMs -= FirstLevelPulseCooldownDecreaseMs;
            SceneReference.MeterManager.PulseCost -= FirstLevelPulseMeterDecrease;
            PulseEmitterLevel = 1;
        }
        else if (PulseEmitterLevel == 1 && SceneReference.ScorekeepingManager.TrySpend(SecondLevelWeaponCost))
        {
            SceneReference.WeaponManager.PulseCooldownMs -= SecondLevelPulseCooldownDecreaseMs;
            SceneReference.MeterManager.PulseCost -= SecondLevelPulseMeterDecrease;
            PulseEmitterLevel = 2;
        }
        if (OnPulseEmitterUpgrade != null) OnPulseEmitterUpgrade.Invoke(this, null);
    }

    public void UpgradeScattershot()
    {
        if (ScattershotLevel == 0 && SceneReference.ScorekeepingManager.TrySpend(FirstLevelWeaponCost))
        {
            SceneReference.WeaponManager.ScattershotForce += FirstLevelScattershotForceIncrease;
            SceneReference.WeaponManager.ScattershotTargetWidth += FirstLevelScattershotTargetWidthIncrease;
            SceneReference.MeterManager.ScattershotCost -= FirstLevelScattershotMeterDecrease;
            ScattershotLevel = 1;
        }
        else if (ScattershotLevel == 1 && SceneReference.ScorekeepingManager.TrySpend(SecondLevelWeaponCost))
        {
            SceneReference.WeaponManager.ScattershotForce += SecondLevelScattershotForceIncrease;
            SceneReference.WeaponManager.ScattershotTargetWidth += SecondLevelScattershotTargetWidthIncrease;
            SceneReference.MeterManager.ScattershotCost -= SecondLevelScattershotMeterDecrease;
            ScattershotLevel = 2;
        }
        if (OnScattershotUpgrade != null) OnScattershotUpgrade.Invoke(this, null);
    }

    public void UpgradeRailgun()
    {
        if (RailgunLevel == 0 && SceneReference.ScorekeepingManager.TrySpend(FirstLevelWeaponCost))
        {
            SceneReference.WeaponManager.RailgunLifeMs += FirstLevelRailgunLifetimeIncreaseMs;
            SceneReference.MeterManager.RailgunCost -= FirstLevelRailgunMeterDecrease;
            RailgunLevel = 1;
        }
        else if (RailgunLevel == 1 && SceneReference.ScorekeepingManager.TrySpend(SecondLevelWeaponCost))
        {
            SceneReference.WeaponManager.RailgunLifeMs += SecondLevelRailgunLifetimeIncreaseMs;
            SceneReference.MeterManager.RailgunCost -= SecondLevelRailgunMeterDecrease;
            RailgunLevel = 2;
        }
        if (OnRailgunUpgrade != null) OnRailgunUpgrade.Invoke(this, null);
    }

    public void UpgradeEngine()
    {
        if (EngineLevel == 0 && SceneReference.ScorekeepingManager.TrySpend(FirstLevelSecondaryCost))
        {
            SceneReference.PlayerMovementManager.AngularSpeed += FirstLevelEngineTurnForceIncrease;
            SceneReference.MeterManager.BoostCost -= FirstLevelBoostMeterDecrease;
            EngineLevel = 1;
        }
        else if (EngineLevel == 1 && SceneReference.ScorekeepingManager.TrySpend(SecondLevelSecondaryCost))
        {
            SceneReference.PlayerMovementManager.AngularSpeed += SecondLevelEngineTurnForceIncrease;
            SceneReference.MeterManager.BoostCost -= SecondLevelBoostMeterDecrease;
            EngineLevel = 2;
        }
        if (OnEngineUpgrade != null) OnEngineUpgrade.Invoke(this, null);
    }

    public void UpgradeShield()
    {
        if (ShieldLevel == 0 && SceneReference.ScorekeepingManager.TrySpend(FirstLevelSecondaryCost))
        {
            SceneReference.ShieldManager.RadiusMultiplier += FirstLevelShieldRadiusMultiplierIncrease;
            SceneReference.MeterManager.ShieldCostPerSecond -= FirstLevelShieldMeterPerSecondDecrease;
            ShieldLevel = 1;
        }
        else if (ShieldLevel == 1 && SceneReference.ScorekeepingManager.TrySpend(SecondLevelSecondaryCost))
        {
            SceneReference.ShieldManager.RadiusMultiplier += SecondLevelShieldRadiusMultiplierIncrease;
            SceneReference.MeterManager.ShieldCostPerSecond -= SecondLevelShieldMeterPerSecondDecrease;
            ShieldLevel = 2;
        }
        if (OnShieldUpgrade != null) OnShieldUpgrade.Invoke(this, null);
    }

    public int GetPulseEmitterUpgradeCost()
    {
        if (PulseEmitterLevel == 0)
        {
            return FirstLevelWeaponCost;
        }
        if (PulseEmitterLevel == 1)
        {
            return SecondLevelWeaponCost;
        }
        return 0;
    }

    public int GetScattershotUpgradeCost()
    {
        if (ScattershotLevel == 0)
        {
            return FirstLevelWeaponCost;
        }
        if (ScattershotLevel == 1)
        {
            return SecondLevelWeaponCost;
        }
        return 0;
    }

    public int GetRailgunUpgradeCost()
    {
        if (RailgunLevel == 0)
        {
            return FirstLevelWeaponCost;
        }
        if (RailgunLevel == 1)
        {
            return SecondLevelWeaponCost;
        }
        return 0;
    }

    public int GetEngineUpgradeCost()
    {
        if (EngineLevel == 0)
        {
            return FirstLevelSecondaryCost;
        }
        if (EngineLevel == 1)
        {
            return SecondLevelSecondaryCost;
        }
        return 0;
    }

    public int GetShieldUpgradeCost()
    {
        if (ShieldLevel == 0)
        {
            return FirstLevelSecondaryCost;
        }
        if (ShieldLevel == 1)
        {
            return SecondLevelSecondaryCost;
        }
        return 0;
    }
}
