using System;
using UnityEngine;
using System.Collections;
using System.Diagnostics;
using Assets.MineMineMine.Scripts.Managers;
using UnityEngine.UI;

public class MeterManager : MonoBehaviour
{
    public float MaximumMeter = 100;
    public float RegeneratePerSecond = 20;
    public float PulseCost = 15;
    public float ScattershotCost = 40;
    public float RailgunCost = 80;
    public float ShieldCostPerSecond = 30; // += RegeneratePerSecond, make a Get() for this?
    public float BoostCost = 30;
    private float _currentMeter;
    private Text _debugText;

    [ExecuteInEditMode]
    void OnValidate()
    {
        EnsureMaximumMeterIsNonNegative();
    }

    private void EnsureMaximumMeterIsNonNegative()
    {
        MaximumMeter = Mathf.Max(0, MaximumMeter);
    }

    private void Awake()
    {
        RegisterWithSceneReference();
    }

    private void RegisterWithSceneReference()
    {
        SceneReference.MeterManager = this;
    }

    private void Start()
    {
        _currentMeter = MaximumMeter;
        _debugText = GameObject.Find("MeterDebug").GetComponent<Text>();
    }

    private void Update()
    {
        RegenerateMeter();
        _debugText.text = "Meter: " + _currentMeter;

    }

    public float GetCurrentMeter()
    {
        return _currentMeter;
    }

    private void RegenerateMeter()
    {
        if (_currentMeter < 0)
        {
            _currentMeter = 0;
        }
        else
        {
            if (_currentMeter < MaximumMeter)
            {
                _currentMeter += RegeneratePerSecond * Time.deltaTime;
            }
            if (_currentMeter > MaximumMeter)
            {
                _currentMeter = MaximumMeter;
            }
        }
    }

    public MeterAction WeaponToMeterAction(Weapon weapon)
    {
        switch (weapon)
        {
            case Weapon.PulseEmitter:
                return MeterAction.Pulse;
            case Weapon.Scattershot:
                return MeterAction.Scattershot;
            case Weapon.Railgun:
                return MeterAction.Railgun;
            default:
                throw new ArgumentOutOfRangeException("weapon", weapon, null);
        }
    }

    public bool HaveEnoughMeter(MeterAction meterAction)
    {
        switch (meterAction)
        {
            case MeterAction.Pulse:
                return HaveEnoughMeter(PulseCost);
            case MeterAction.Scattershot:
                return HaveEnoughMeter(ScattershotCost);
            case MeterAction.Railgun:
                return HaveEnoughMeter(RailgunCost);
            case MeterAction.Shield:
                return HaveEnoughMeter(AdjustedShieldCost());
            case MeterAction.Boost:
                return HaveEnoughMeter(BoostCost);
            default:
                throw new ArgumentOutOfRangeException("meterAction", meterAction, null);
        }
    }

    public bool HaveEnoughMeter(Weapon weapon)
    {
        return HaveEnoughMeter(WeaponToMeterAction(weapon));
    }

    public void ExpendMeter(MeterAction meterAction)
    {
        switch (meterAction)
        {
            case MeterAction.Pulse:
                ExpendMeter(PulseCost);
                break;
            case MeterAction.Scattershot:
                ExpendMeter(ScattershotCost);
                break;
            case MeterAction.Railgun:
                ExpendMeter(RailgunCost);
                break;
            case MeterAction.Shield:
                ExpendMeter(AdjustedShieldCost());
                break;
            case MeterAction.Boost:
                ExpendMeter(AdjustedBoostCost());
                break;
            default:
                throw new ArgumentOutOfRangeException("meterAction", meterAction, null);
        }
    }

    private float AdjustedShieldCost()
    {
        return (ShieldCostPerSecond + RegeneratePerSecond) * Time.deltaTime;
    }

    private float AdjustedBoostCost()
    {
        return BoostCost / SceneReference.PlayerSpawnManager.GetAllPlayers().Count;
    }

    public void ExpendMeter(Weapon weapon)
    {
        ExpendMeter(WeaponToMeterAction(weapon));
    }

    public bool HaveEnoughMeter(float cost)
    {
        if (_currentMeter >= cost)
        {
            return true;
        }
        return false;
    }

    private void ExpendMeter(float cost)
    {
        if (HaveEnoughMeter(cost))
        {
            _currentMeter -= cost;
        }
    }
}
