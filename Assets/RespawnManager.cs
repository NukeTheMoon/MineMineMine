﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class RespawnManager : MonoBehaviour {

    public Transform ReticleSpawnPoint;
    public int RespawnWeaponCooldownMs;
    public int RespawnInvulnerabilityMs;

    public bool Respawning { get; private set; }
    public bool Invulnerability { get; private set; }
    public event EventHandler OnInvulnerabilityStart = delegate { };
    public event EventHandler OnInvulnerabilityEnd = delegate { };

    private GameObject _reticle;
    private List<GameObject> _protectionRings;

    private void Awake()
    {
        RegisterWithSceneReference();
    }

    private void Start()
    {
        Respawning = false;
        _protectionRings = new List<GameObject>();
    }

    private void Update()
    {
        if (_reticle != null && Input.GetKeyDown(KeyCode.Space))
        {
            SceneReference.PlayerSpawner.Spawn(_reticle.transform);
            Destroy(_reticle);
            SceneReference.LifeManager.DecreaseLifeCount();
            SceneReference.WeaponManager.InitiateCooldown(RespawnWeaponCooldownMs);
            StartInvulnerability();
            Respawning = false;
        }
    }

    private void StartInvulnerability()
    {
        Invulnerability = true;
        OnInvulnerabilityStart(this, null);
        StartCoroutine(InvulnerabilityCoroutine());
        CreateProtectionRings();
    }

    private void CreateProtectionRings()
    {
        foreach (var player in SceneReference.PlayerSpawner.GetAllPlayers())
        {
            var protectionRing =
                (GameObject)
                    Instantiate(PrefabReference.ProtectionRing,
                        player.transform.position, Quaternion.identity);
            protectionRing.transform.parent = player.transform;
            _protectionRings.Add(protectionRing);
        }
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        yield return new WaitForSeconds(TimeHelper.MillisecondsToSeconds(RespawnInvulnerabilityMs));
        StopInvulnerability();
    }

    private void StopInvulnerability()
    {
        Invulnerability = false;
        DestroyProtectionRings();
        OnInvulnerabilityEnd(this, null);
    }

    private void DestroyProtectionRings()
    {
        for (var i = 0; i < _protectionRings.Count; ++i)
        {
            Destroy(_protectionRings[i]);
        }
        _protectionRings = new List<GameObject>();
    }

    private void RegisterWithSceneReference()
    {
        SceneReference.RespawnManager = this;
    }

    public void ShowReticle()
    {
        if (_reticle == null)
        {
            Respawning = true;
            _reticle =
                (GameObject)
                    Instantiate(PrefabReference.Reticle, ReticleSpawnPoint.position, ReticleSpawnPoint.rotation);
        }
    }


}