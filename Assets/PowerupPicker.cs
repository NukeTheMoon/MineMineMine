using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PowerupPicker : MonoBehaviour {

    private Powerup _powerupType;
    private TextMesh _debugName;

    private void Start()
    {
        PickPowerupType();
        SetDebugName();
    }

    private void PickPowerupType()
    {
        _powerupType = (Powerup)UnityEngine.Random.Range(0, Enum.GetNames(typeof(Powerup)).Length);
    }

    private void SetDebugName()
    {
        _debugName = GetComponentInChildren<TextMesh>();
        switch (_powerupType)
        {
            case (Powerup.ScattershotAmmo):
                _debugName.text = "SCATTERSHOT BOOST";
                break;
            case (Powerup.RailgunAmmo):
                _debugName.text = "RAILGUN AMMO";
                break;
            case (Powerup.ScattershotBoost):
                _debugName.text = "SCATTERSHOT BOOST";
                break;
            case (Powerup.RailgunBoost):
                _debugName.text = "RAILGUN BOOST";
                break;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == TagsReference.PLAYER)
        {
            switch (_powerupType)
            {
                case (Powerup.ScattershotAmmo):
                    SceneReference.WeaponManager.AddAmmo(Weapon.Scattershot, SceneReference.PowerupManager.ScattershotAddAmmoAmount);
                    break;
                case (Powerup.RailgunAmmo):
                    SceneReference.WeaponManager.AddAmmo(Weapon.Railgun, SceneReference.PowerupManager.RailgunAddAmmoAmount);
                    break;
                case (Powerup.ScattershotBoost):
                    SceneReference.WeaponManager.ScattershotExpansion +=
                        SceneReference.PowerupManager.ScattershotBoostExpansionIncrease;
                    break;
                case (Powerup.RailgunBoost):
                    if (SceneReference.WeaponManager.RailgunCooldownMs > 0)
                    {
                        SceneReference.WeaponManager.RailgunCooldownMs -=
                            SceneReference.PowerupManager.RailgunBoostCooldownDecrease;
                    }
                    SceneReference.WeaponManager.RailgunLifeMs +=
                        SceneReference.PowerupManager.RailgunBoostLifeMsIncrease;
                    break;
            }
            Destroy(gameObject);
        }
    }

}
