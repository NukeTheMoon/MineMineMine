using UnityEngine;
using System.Collections;

public class PrefabReference : MonoBehaviour {  

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _pulseMissile;
    [SerializeField] private GameObject _scattershotMissile;
    [SerializeField] private GameObject _railgunMissile;
    [SerializeField] private GameObject _asteroid;
    [SerializeField] private GameObject _yield;
    [SerializeField] private GameObject _powerup;
    [SerializeField] private GameObject _reticle;
    [SerializeField] private GameObject _lastKnownPosition;
    [SerializeField] private GameObject _protectionRing;

    public static GameObject Player;
    public static GameObject PulseMissile;
    public static GameObject ScattershotMissile;
    public static GameObject RailgunMissile;
    public static GameObject Asteroid;
    public static GameObject Yield;
    public static GameObject Powerup;
    public static GameObject Reticle;
    public static GameObject LastKnownPosition;
    public static GameObject ProtectionRing;

    private void Awake()
    {
        Player = _player;
        PulseMissile = _pulseMissile;
        ScattershotMissile = _scattershotMissile;
        RailgunMissile = _railgunMissile;
        Asteroid = _asteroid;
        Yield = _yield;
        Powerup = _powerup;
        Reticle = _reticle;
        LastKnownPosition = _lastKnownPosition;
        ProtectionRing = _protectionRing;
    }

}
