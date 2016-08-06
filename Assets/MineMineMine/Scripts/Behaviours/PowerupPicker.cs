using System;
using UnityEngine;

public class PowerupPicker : MonoBehaviour
{

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
			case (Powerup.ShieldAmmo):
				_debugName.text = "SHIELD AMMO";
				break;
			default:
				Debug.LogWarning("Undefined debug name for pickup type: " + _powerupType);
				break;
		}
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag != TagsReference.PLAYER) return;
		switch (_powerupType)
		{
			case (Powerup.ScattershotAmmo):
				ScattershotAmmoPickupEffect();
				break;
			case (Powerup.RailgunAmmo):
				RailgunAmmoPickupEffect();
				break;
			case (Powerup.ScattershotBoost):
				ScattershotBoostPickupEffect();
				break;
			case (Powerup.RailgunBoost):
				RailgunBoostPickupEffect();
				break;
			case (Powerup.ShieldAmmo):
				ShieldAmmoPickupEffect();
				break;
			default:
				Debug.LogError("Undefined effect for pickup type: " + _powerupType);
				throw new ArgumentOutOfRangeException();
		}
		Destroy(gameObject);
	}

	private void ScattershotAmmoPickupEffect()
	{
		SceneReference.WeaponManager.AddAmmo(Weapon.Scattershot, SceneReference.PowerupManager.ScattershotAddAmmoAmount);
	}

	private void RailgunAmmoPickupEffect()
	{
		SceneReference.WeaponManager.AddAmmo(Weapon.Railgun, SceneReference.PowerupManager.RailgunAddAmmoAmount);
	}

	private void ScattershotBoostPickupEffect()
	{
		SceneReference.WeaponManager.ScattershotExpansion += SceneReference.PowerupManager.ScattershotBoostExpansionIncrease;
	}

	private void RailgunBoostPickupEffect()
	{
		if (SceneReference.WeaponManager.RailgunCooldownMs > 0)
		{
			SceneReference.WeaponManager.RailgunCooldownMs -= SceneReference.PowerupManager.RailgunBoostCooldownDecrease;
		}
		SceneReference.WeaponManager.RailgunLifeMs += SceneReference.PowerupManager.RailgunBoostLifeMsIncrease;
	}

	private void ShieldAmmoPickupEffect()
	{
		SceneReference.WeaponManager.AddAmmo(Weapon.Shield, SceneReference.PowerupManager.ShieldAddAmmoAmount);
	}
}
