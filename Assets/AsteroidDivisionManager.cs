using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AsteroidDivisionManager : MonoBehaviour {

    public int Divisor = 2;
    [Range(0, 6)]
    public int Divisions = 6;
    public float PositionJitter = 1;
    public float Force = 100;
    public Dictionary<int, int> DivisionReference = new Dictionary<int, int>();


    void Awake()
    {
        RegisterWithSceneReference();
    }

    private void RegisterWithSceneReference()
    {
        SceneReference.AsteroidDivisionManager = this;
    }
}
