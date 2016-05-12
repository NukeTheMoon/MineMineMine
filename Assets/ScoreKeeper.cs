using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public static int Score { get; set; }
    public AsteroidSpawner AsteroidSpawner;
    public Text ScoreDebugTextField;

    private static int _maxScoreForWave;
    private static int _asteroidHitValue = 100;
    private static bool _scoreDebugFieldNeedsUpdating;

    void Start()
    {
        Score = 0;
        _maxScoreForWave = CalculateMaxScoreForWave();
        UpdateScoreDebugTextField();
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
        for (var i=0; i <= AsteroidDivider.Divisions; ++i)
        {
            totalAsteroidFragments += (int)Math.Pow(AsteroidDivider.Divisor, i);
        }
        return AsteroidSpawner.MaxAsteroidCount * totalAsteroidFragments * _asteroidHitValue;
    }

    public static void AsteroidHit()
    {
        Score += _asteroidHitValue;
        _scoreDebugFieldNeedsUpdating = true;
    }

    void UpdateScoreDebugTextField()
    {
        ScoreDebugTextField.text = "Score: " + Score + "/" + _maxScoreForWave;
    }

}
