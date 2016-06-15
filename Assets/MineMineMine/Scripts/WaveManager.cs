using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public int WaveOfMaximumAsteroidAmount;
    public int WaveOfMaximumAsteroidDivisions;
    public Text DebugText;

    private int _asteroidLimitPerWave;
    private int _waveNo;

    private void Start()
    {
        SceneReference.ScoreKeeper.OnMaxScoreReached += ScoreKeeper_OnMaxScoreReached;
        _asteroidLimitPerWave = SceneReference.AsteroidSpawner.MaxAsteroidCount;
        _waveNo = 0;
        StartNextWave();
    }

    private void ScoreKeeper_OnMaxScoreReached(object sender, System.EventArgs e)
    {
        StartNextWave();
    }

    private void UpdateDebugText()
    {
        DebugText.text = "Wave: " + _waveNo;
    }

    public void StartNextWave()
    {
        ++_waveNo;
        SetDifficulty(_waveNo);
        SceneReference.ScoreKeeper.Reset();
        SceneReference.AsteroidSpawner.StartSpawning();
        UpdateDebugText();
    }

    private void SetDifficulty(int difficulty)
    {
        SceneReference.AsteroidSpawner.MaxAsteroidCount = 
            (int)Mathf.Ceil(Mathf.Lerp(SceneReference.AsteroidSpawner.MinAsteroidCount,
                _asteroidLimitPerWave, difficulty / (float)WaveOfMaximumAsteroidDivisions));
        SceneReference.AsteroidDivisionManager.Divisions =
            (int)Mathf.Ceil(Mathf.Lerp(SceneReference.AsteroidDivisionManager.MinimumDivisions,
                SceneReference.AsteroidDivisionManager.MaximumDivisions, difficulty/(float)WaveOfMaximumAsteroidDivisions));
    }

}
