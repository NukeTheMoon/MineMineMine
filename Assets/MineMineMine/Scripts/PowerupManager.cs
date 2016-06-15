using UnityEngine;
using System.Collections;
using System;

public class PowerupManager : MonoBehaviour {


    [Range(0,1)] public float DropChance;
    public int CooldownDurationSeconds;
    public bool Cooldown { get; protected set; }
    public int ScattershotAddAmmoAmount;
    public int RailgunAddAmmoAmount;
    public int ScattershotBoostExpansionIncrease;
    public int RailgunBoostCooldownDecrease;
    public int RailgunBoostLifeMsIncrease;
    public int ShieldAddAmmoAmount;

    private int _scattershotBoostLevel;
    private int _railgunBoostLevel;

    private void Awake()
    {
        RegisterWithSceneReference();
    }

    private void Start()
    {
        Cooldown = false;
        ResetBoostLevels();
    }

    public void InitiateCooldown()
    {
        Cooldown = true;
        StartCoroutine(CooldownCoroutine());
    }

    public IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(CooldownDurationSeconds);
        Cooldown = false;
    }

    public void IncreaseScattershotBoostLevel()
    {
        ++_scattershotBoostLevel;
    }

    public void IncreaseRailgunBoostLevel()
    {
        ++_railgunBoostLevel;
    }

    public void ResetBoostLevels()
    {
        _scattershotBoostLevel = 0;
        _railgunBoostLevel = 0;
    }

    private void RegisterWithSceneReference()
    {
        SceneReference.PowerupManager = this;
    }
}
