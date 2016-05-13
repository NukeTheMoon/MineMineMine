using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public int Score { get; set; }
    public Text ScoreDebugTextField;

    private int _maxScoreForWave;
    private int _asteroidHitValue = 100;
    private bool _scoreDebugFieldNeedsUpdating;

    void Awake()
    {
        RegisterWithSceneReference();

    }

    void Start()
    {
        Score = 0;
        _maxScoreForWave = CalculateMaxScoreForWave();
        UpdateScoreDebugTextField();
    }

    private void RegisterWithSceneReference()
    {
        SceneReference.ScoreKeeper = this;
    }

    void Update()
    {
        if (_scoreDebugFieldNeedsUpdating)
        {
            UpdateScoreDebugTextField();
            _scoreDebugFieldNeedsUpdating = false;
        }
    }

    private int CalculateMaxScoreForWave()
    {
        int totalAsteroidFragments = 0;
        for (var i=0; i <= SceneReference.AsteroidDivisionManager.Divisions; ++i)
        {
            totalAsteroidFragments += (int)Math.Pow(SceneReference.AsteroidDivisionManager.Divisor, i);
        }
        return SceneReference.AsteroidSpawner.MaxAsteroidCount * totalAsteroidFragments * _asteroidHitValue;
    }

    public void AsteroidHit()
    {
        Score += _asteroidHitValue;
        _scoreDebugFieldNeedsUpdating = true;
    }

    void UpdateScoreDebugTextField()
    {
        ScoreDebugTextField.text = "Score: " + Score + "/" + _maxScoreForWave;
    }

}
