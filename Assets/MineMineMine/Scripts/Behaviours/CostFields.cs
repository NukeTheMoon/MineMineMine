using System;
using UnityEngine;
using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine.UI;

public class CostFields : MonoBehaviour
{

    public TextMeshProUGUI PulseUpgradeCost;
    public TextMeshProUGUI ScattershotUpgradeCost;
    public TextMeshProUGUI RailgunUpgradeCost;
    public TextMeshProUGUI EngineUpgradeCost;
    public TextMeshProUGUI ShieldUpgradeCost;

    public string MaxLevelText = "MAX";

    private void Start()
    {
        SceneReference.UpgradeMenuManager.OnPulseEmitterUpgrade += UpgradeMenuManager_OnPulseEmitterUpgrade;
        SceneReference.UpgradeMenuManager.OnScattershotUpgrade += UpgradeMenuManager_OnScattershotUpgrade;
        SceneReference.UpgradeMenuManager.OnRailgunUpgrade += UpgradeMenuManager_OnRailgunUpgrade;
        SceneReference.UpgradeMenuManager.OnEngineUpgrade += UpgradeMenuManager_OnEngineUpgrade;
        SceneReference.UpgradeMenuManager.OnShieldUpgrade += UpgradeMenuManager_OnShieldUpgrade;

        UpdatePulseUpgradeCost();
        UpdateScattershotUpgradeCost();
        UpdateRailgunUpgradeCost();
        UpdateEngineUpgradeCost();
        UpdateShieldUpgradeCost();
    }

    private void UpgradeMenuManager_OnPulseEmitterUpgrade(object sender, System.EventArgs e)
    {
        UpdatePulseUpgradeCost();
    }

    private void UpgradeMenuManager_OnScattershotUpgrade(object sender, System.EventArgs e)
    {
        UpdateScattershotUpgradeCost();
    }

    private void UpgradeMenuManager_OnRailgunUpgrade(object sender, System.EventArgs e)
    {
        UpdateRailgunUpgradeCost();
    }

    private void UpgradeMenuManager_OnEngineUpgrade(object sender, System.EventArgs e)
    {
        UpdateEngineUpgradeCost();
    }

    private void UpgradeMenuManager_OnShieldUpgrade(object sender, System.EventArgs e)
    {
        UpdateShieldUpgradeCost();
    }

    private void UpdatePulseUpgradeCost()
    {
        if (SceneReference.UpgradeMenuManager.PulseEmitterLevel < UpgradeMenuManager.MaxLevel)
        {
            PulseUpgradeCost.text = SceneReference.UpgradeMenuManager.GetPulseEmitterUpgradeCost().ToString("n0", CultureInfo.InvariantCulture);
        }
        else
        {
            MaxField(PulseUpgradeCost);
        }
    }

    private void UpdateScattershotUpgradeCost()
    {
        if (SceneReference.UpgradeMenuManager.ScattershotLevel < UpgradeMenuManager.MaxLevel)
        {
            ScattershotUpgradeCost.text = SceneReference.UpgradeMenuManager.GetScattershotUpgradeCost().ToString("n0", CultureInfo.InvariantCulture);
        }
        else
        {
            MaxField(ScattershotUpgradeCost);
        }
    }

    private void UpdateRailgunUpgradeCost()
    {
        if (SceneReference.UpgradeMenuManager.RailgunLevel < UpgradeMenuManager.MaxLevel)
        {
            RailgunUpgradeCost.text = SceneReference.UpgradeMenuManager.GetRailgunUpgradeCost().ToString("n0", CultureInfo.InvariantCulture);
        }
        else
        {
            MaxField(RailgunUpgradeCost);
        }
    }

    private void UpdateEngineUpgradeCost()
    {
        if (SceneReference.UpgradeMenuManager.EngineLevel < UpgradeMenuManager.MaxLevel)
        {
            EngineUpgradeCost.text = SceneReference.UpgradeMenuManager.GetEngineUpgradeCost().ToString("n0", CultureInfo.InvariantCulture);
        }
        else
        {
            MaxField(EngineUpgradeCost);
        }
    }

    private void UpdateShieldUpgradeCost()
    {
        if (SceneReference.UpgradeMenuManager.ShieldLevel < UpgradeMenuManager.MaxLevel)
        {
            ShieldUpgradeCost.text = SceneReference.UpgradeMenuManager.GetShieldUpgradeCost().ToString("n0", CultureInfo.InvariantCulture);
        }
        else
        {
            MaxField(ShieldUpgradeCost);
        }
    }

    private void MaxField(TextMeshProUGUI amount)
    {
        Image icon = amount.gameObject.transform.parent.GetComponentInChildren<Image>();
        if (icon != null) icon.gameObject.SetActive(false);
        amount.text = MaxLevelText;
    }
}
