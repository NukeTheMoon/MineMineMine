using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public int WaveOfMaximumAsteroidAmount;
    public int WaveOfMaximumAsteroidDivisions;

    private int _asteroidLimitPerWave;
    private int _waveNo;

    private void Start()
    {
        SceneReference.ScorekeepingManager.OnMaxScoreReached += ScoreKeeper_OnMaxScoreReached;
        _asteroidLimitPerWave = SceneReference.AsteroidSpawnManager.MaxAsteroidCount;
        _waveNo = 0;
        StartNextWave();
    }

    private void ScoreKeeper_OnMaxScoreReached(object sender, System.EventArgs e)
    {
        StartNextWave();
    }


    public void StartNextWave()
    {
        ++_waveNo;
        SetDifficulty(_waveNo);
        SceneReference.ScorekeepingManager.Reset();
        SceneReference.AsteroidSpawnManager.StartSpawning();
    }

    private void SetDifficulty(int difficulty)
    {
        SceneReference.AsteroidSpawnManager.MaxAsteroidCount =
            (int)Mathf.Ceil(Mathf.Lerp(SceneReference.AsteroidSpawnManager.MinAsteroidCount,
                _asteroidLimitPerWave, difficulty / (float)WaveOfMaximumAsteroidDivisions));
        SceneReference.AsteroidDivisionManager.Divisions =
            (int)Mathf.Ceil(Mathf.Lerp(SceneReference.AsteroidDivisionManager.MinimumDivisions,
                SceneReference.AsteroidDivisionManager.MaximumDivisions, difficulty / (float)WaveOfMaximumAsteroidDivisions));
    }

}
