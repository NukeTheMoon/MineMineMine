using UnityEngine;
using System.Collections;

public class PrefabReference : MonoBehaviour {


    [SerializeField]
    private GameObject _player;
    [SerializeField]
    public GameObject _missile;
    [SerializeField]
    public GameObject _asteroid;
    [SerializeField]
    public GameObject _yield;

    public static GameObject Player;
    public static GameObject Missile;
    public static GameObject Asteroid;
    public static GameObject Yield;

    void Awake()
    {
        Player = _player;
        Missile = _missile;
        Asteroid = _asteroid;
        Yield = _yield;
    }

}
