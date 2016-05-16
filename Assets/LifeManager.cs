using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class LifeManager : MonoBehaviour {

    public int StartingLives;
    public Text DebugTextField;

    public bool PlayerAlive { get; set; }

    private int _livesLeft;

    private void Awake()
    {
        RegisterWithSceneReference();

    }

    private void Start()
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
        return _livesLeft <= 0;
    }

    public void Die()
    {
        if (NoLivesLeft())
        {
            PlayerAlive = false;
        }
        else
        {
            SceneReference.RespawnManager.ShowReticle();
        }
    }

    public void DecreaseLifeCount()
    {
        --_livesLeft;
        UpdateDebugTextField();
    }
}
