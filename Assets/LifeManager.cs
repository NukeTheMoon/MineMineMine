using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class LifeManager : MonoBehaviour {

    public int StartingLives;
    public Text DebugTextField;

    public bool PlayerAlive { get; set; }

    private int _livesLeft;

    void Awake()
    {
        RegisterWithSceneReference();

    }

    void Start()
    {
        PlayerAlive = true;
        _livesLeft = StartingLives;
        UpdateDebugTextField();
    }

    private void RegisterWithSceneReference()
    {
        SceneReference.LifeManager = this;
    }

    private void UpdateDebugTextField()
    {
        DebugTextField.text = "Lives left: " + _livesLeft;
    }

    public bool NoLivesLeft()
    {
        return _livesLeft < 0;
    }

    public void LoseLife()
    {
        if (NoLivesLeft())
        {
            PlayerAlive = false;
        }
        else
        {
            SceneReference.PlayerSpawner.Spawn();
            --_livesLeft;
            UpdateDebugTextField();
        }
    }
}
