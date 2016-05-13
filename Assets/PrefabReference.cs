﻿using UnityEngine;
using System.Collections;

public class PrefabReference : MonoBehaviour {


    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _pulseMissile;
    [SerializeField]
    private GameObject _scattershotMissile;
    [SerializeField]
    private GameObject _asteroid;
    [SerializeField]
    private GameObject _yield;
    [SerializeField]
    private GameObject _powerup;

    public static GameObject Player;
    public static GameObject PulseMissile;
    public static GameObject ScattershotMissile;
    public static GameObject Asteroid;
    public static GameObject Yield;
    public static GameObject Powerup;

    void Awake()
    {
        Player = _player;
        PulseMissile = _pulseMissile;
        ScattershotMissile = _scattershotMissile;
        Asteroid = _asteroid;
        Yield = _yield;
        Powerup = _powerup;
    }

}