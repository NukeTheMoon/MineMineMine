using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class LifeManager : MonoBehaviour {

    public int StartingLives;
    public Text DebugTextField;
    public PlayerSpawner PlayerSpawner;

    public static bool PlayerAlive { get; set; }

    private int _livesLeft;

    void Start()
    {
        PlayerAlive = true;
        _livesLeft = StartingLives;
        UpdateDebugTextField();
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
            PlayerSpawner.Spawn();
            --_livesLeft;
            UpdateDebugTextField();
        }
    }
}
