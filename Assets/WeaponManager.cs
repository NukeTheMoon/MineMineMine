using UnityEngine;
using System.Collections;
using System;

public class WeaponManager : MonoBehaviour {

    public Weapon CurrentWeapon { get; protected set; }
    public Weapon InitialWeapon;

    public float PulseForce = 1000.0f;
    public int PulseCooldownMs = 0;
    public float PulseDrag = 0;
    public bool PulsePunchthrough = false;

    public int ScattershotLifeMs;
    public int ScattershotExpansion;
    public int ScattershotCooldownMs;
    public float ScattershotForce = 10000.0f;
    public float ScattershotDrag = 1;
    public bool ScattershotPunchthrough = true;


    public bool Cooldown { get; protected set; }

    void Awake()
    {
        RegisterWithSceneReference();
    }

    void Start()
    {
        ResetWeapon();
    }

    public void ResetWeapon()
    {
        ChangeWeapon(InitialWeapon);
    }

    public void InitiateCooldown()
    {
        Cooldown = true;
        switch (CurrentWeapon)
        {
            case (Weapon.PulseEmitter):
                StartCoroutine(CooldownCoroutine(PulseCooldownMs));
                break;
            case (Weapon.Scattershot):
                StartCoroutine(CooldownCoroutine(ScattershotCooldownMs));
                break;
        }
    }

    internal void ChangeWeapon(Weapon _weapon)
    {
        CurrentWeapon = _weapon;
        ResetCooldown();
    }

    private IEnumerator CooldownCoroutine(int cooldownMs)
    {
        yield return new WaitForSeconds(TimeHelper.MillisecondsToSeconds(cooldownMs));
        Cooldown = false;
    }

    public bool IsPunchthrough()
    {
        switch (CurrentWeapon)
        {
            case (Weapon.PulseEmitter):
                return PulsePunchthrough;
            case (Weapon.Scattershot):
                return ScattershotPunchthrough;
            default:
                return true;
        }
    }

    public void ResetCooldown()
    {
        Cooldown = false;
    }

    private void RegisterWithSceneReference()
    {
        SceneReference.WeaponManager = this;
    }
	
}
