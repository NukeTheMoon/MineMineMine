﻿using UnityEngine;
using System.Collections;
using System;

public class PowerupManager : MonoBehaviour {


    [Range(0,1)]
    public float DropChance;
    public int CooldownDurationSeconds;
    public bool Cooldown { get; protected set; }

    void Awake()
    {
        RegisterWithSceneReference();
    }

    void Start()
    {
        Cooldown = false;
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

    private void RegisterWithSceneReference()
    {
        SceneReference.PowerupManager = this;
    }
}