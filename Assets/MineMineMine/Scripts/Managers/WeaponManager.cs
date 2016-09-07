using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{

	public Weapon CurrentWeapon { get; protected set; }
	public Weapon InitialWeapon;

	public float PulseForce = 1000.0f;
	public int PulseCooldownMs = 0;
	public float PulseDrag = 0;
	public bool PulsePunchthrough = false;
	public int PulseInitialAmmo;
	private int _pulseAmmo;
	private bool _pulseCooldown;

	public int ScattershotLifeMs;
	public int ScattershotTargetWidth;
	public int ScattershotCooldownMs;
	public float ScattershotForce = 10000.0f;
	public float ScattershotDrag = 1;
	public bool ScattershotPunchthrough = true;
	public int ScattershotInitialAmmo;
	private int _scattershotAmmo;
	private bool _scattershotCooldown;
	private int _initialScattershotWidth;

	public int RailgunLifeMs = 20;
	public int RailgunCooldownMs;
	public bool RailgunPunchthrough = true;
	public int RailgunInitialAmmo;
	private int _railgunAmmo;
	private bool _railgunCooldown;
	private int _initialRailgunCooldownMs;
	private int _initialRailgunLifeMs;

	public int NukeLifeMs;
	public float NukeExpansion;

	public int ShieldInitialAmmo;
	private int _shieldAmmo;

	public Text DebugText;

	private void Awake()
	{
		RegisterWithSceneReference();
	}

	private void Start()
	{
		_initialScattershotWidth = ScattershotTargetWidth;
		_initialRailgunCooldownMs = RailgunCooldownMs;
		_initialRailgunLifeMs = RailgunLifeMs;
		ResetWeapon();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			ChangeWeapon(Weapon.PulseEmitter);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			ChangeWeapon(Weapon.Scattershot);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			ChangeWeapon(Weapon.Railgun);
		}
		if (Input.GetKeyDown(KeyCode.Alpha4) && !SceneReference.RespawnManager.Invulnerability && HaveAmmo(Weapon.Shield))
		{
			SceneReference.RespawnManager.StartInvulnerability();
			ConsumeAmmo(Weapon.Shield);
		}
		UpdateDebugText();

	}

	private void UpdateDebugText()
	{
		DebugText.text = "Pulse ammo: " + _pulseAmmo +
						 "\nScattershot ammo: " + _scattershotAmmo +
						 "\nRailgun ammo: " + _railgunAmmo +
						 "\nShield ammo: " + _shieldAmmo;
	}

	public void ResetWeapon()
	{
		ChangeWeapon(InitialWeapon);
		ScattershotTargetWidth = _initialScattershotWidth;
		RailgunCooldownMs = _initialRailgunCooldownMs;
		RailgunLifeMs = _initialRailgunLifeMs;
		_pulseAmmo = PulseInitialAmmo;
		_scattershotAmmo = ScattershotInitialAmmo;
		_railgunAmmo = RailgunInitialAmmo;
		_shieldAmmo = ShieldInitialAmmo;
	}

	public void InitiateCooldown()
	{
		switch (CurrentWeapon)
		{
			case (Weapon.PulseEmitter):
				_pulseCooldown = true;
				StartCoroutine(CooldownCoroutine(PulseCooldownMs, Weapon.PulseEmitter));
				break;
			case (Weapon.Scattershot):
				_scattershotCooldown = true;
				StartCoroutine(CooldownCoroutine(ScattershotCooldownMs, Weapon.Scattershot));
				break;
			case (Weapon.Railgun):
				_railgunCooldown = true;
				StartCoroutine(CooldownCoroutine(RailgunCooldownMs, Weapon.Railgun));
				break;
		}
	}

	public void StartGlobalCooldown(int cooldownMs)
	{
		_pulseCooldown = true;
		_scattershotCooldown = true;
		_railgunCooldown = true;
		StartCoroutine(GlobalCooldownCoroutine(cooldownMs));
	}

	private IEnumerator GlobalCooldownCoroutine(int cooldownMs)
	{
		yield return new WaitForSeconds(TimeHelper.MillisecondsToSeconds(cooldownMs));
		_pulseCooldown = false;
		_scattershotCooldown = false;
		_railgunCooldown = false;

	}

	internal void ChangeWeapon(Weapon _weapon)
	{
		CurrentWeapon = _weapon;
	}

	private IEnumerator CooldownCoroutine(int cooldownMs, Weapon weapon)
	{
		yield return new WaitForSeconds(TimeHelper.MillisecondsToSeconds(cooldownMs));
		switch (weapon)
		{
			case Weapon.PulseEmitter:
				_pulseCooldown = false;
				break;
			case Weapon.Scattershot:
				_scattershotCooldown = false;
				break;
			case Weapon.Railgun:
				_railgunCooldown = false;
				break;
		}
	}

	public bool IsPunchthrough(int missileId)
	{
		Weapon weaponType;
		if (SceneReference.MissileSpawnManager.WeaponTypeOfMissileId.TryGetValue(missileId, out weaponType))
		{
			switch (weaponType)
			{
				case (Weapon.PulseEmitter):
					return PulsePunchthrough;
				case (Weapon.Scattershot):
					return ScattershotPunchthrough;
				default:
					return true;
			}
		}

		return true;

	}

	private void RegisterWithSceneReference()
	{
		SceneReference.WeaponManager = this;
	}

	public bool IsInCooldown()
	{
		switch (CurrentWeapon)
		{
			case Weapon.PulseEmitter:
				return _pulseCooldown;
			case Weapon.Scattershot:
				return _scattershotCooldown;
			case Weapon.Railgun:
				return _railgunCooldown;
			default:
				return false;
		}
	}

	public bool HaveAmmo()
	{
		switch (CurrentWeapon)
		{
			case Weapon.PulseEmitter:
				return _pulseAmmo > 0 || _pulseAmmo == -1;
			case Weapon.Scattershot:
				return _scattershotAmmo > 0 || _scattershotAmmo == -1;
			case Weapon.Railgun:
				return _railgunAmmo > 0 || _railgunAmmo == -1;
			default:
				return false;
		}
	}

	public bool HaveAmmo(Weapon specificWeapon)
	{
		switch (specificWeapon)
		{
			case Weapon.PulseEmitter:
				return _pulseAmmo > 0 || _pulseAmmo == -1;
			case Weapon.Scattershot:
				return _scattershotAmmo > 0 || _scattershotAmmo == -1;
			case Weapon.Railgun:
				return _railgunAmmo > 0 || _railgunAmmo == -1;
			case Weapon.Shield:
				return _shieldAmmo > 0 || _shieldAmmo == -1;
			default:
				return false;
		}
	}

	public bool CanFire()
	{
		return !IsInCooldown() && HaveAmmo();
	}

	public void ConsumeAmmo()
	{
		switch (CurrentWeapon)
		{
			case Weapon.PulseEmitter:
				if (_pulseAmmo > 0) --_pulseAmmo;
				break;
			case Weapon.Scattershot:
				if (_scattershotAmmo > 0) --_scattershotAmmo;
				break;
			case Weapon.Railgun:
				if (_railgunAmmo > 0) --_railgunAmmo;
				break;
			case Weapon.Shield:
				if (_shieldAmmo > 0) --_shieldAmmo;
				break;
		}
	}

	public void ConsumeAmmo(Weapon specificWeapon)
	{
		switch (specificWeapon)
		{
			case Weapon.PulseEmitter:
				if (_pulseAmmo > 0) --_pulseAmmo;
				break;
			case Weapon.Scattershot:
				if (_scattershotAmmo > 0) --_scattershotAmmo;
				break;
			case Weapon.Railgun:
				if (_railgunAmmo > 0) --_railgunAmmo;
				break;
			case Weapon.Shield:
				if (_shieldAmmo > 0) --_shieldAmmo;
				break;
		}
	}

	public void AddAmmo(Weapon weapon, int amount)
	{
		switch (weapon)
		{
			case Weapon.PulseEmitter:
				_pulseAmmo += amount;
				break;
			case Weapon.Scattershot:
				_scattershotAmmo += amount; ;
				break;
			case Weapon.Railgun:
				_railgunAmmo += amount;
				break;
			case Weapon.Shield:
				_shieldAmmo += amount;
				break;
		}
	}
}
