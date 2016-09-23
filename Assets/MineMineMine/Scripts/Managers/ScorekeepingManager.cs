using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MineMineMine.Scripts.Managers
{
    public class ScorekeepingManager : MonoBehaviour
    {
        public int WaveScore { get; private set; }
        public int TotalScore { get; private set; }
        public int AsteroidHitValue = 10;
        public int MaximumScoreTolerance;

        private int _maxScoreForWave;

        public event EventHandler OnMaxScoreReached;
        public event EventHandler ScoreChanged;

        private void Awake()
        {
            RegisterWithSceneReference();
        }

        private void Start()
        {
            TotalScore = 0;
            WaveScore = 0;
            Reset();
        }

        private void RegisterWithSceneReference()
        {
            SceneReference.ScorekeepingManager = this;
        }

        private int CalculateMaxScoreForWave()
        {
            int totalAsteroidFragments = 0;
            for (var i = 0; i <= SceneReference.AsteroidDivisionManager.Divisions; ++i)
            {
                totalAsteroidFragments += (int)Math.Pow(SceneReference.AsteroidDivisionManager.Divisor, i);
            }
            return SceneReference.AsteroidSpawnManager.MaxAsteroidCount * totalAsteroidFragments * AsteroidHitValue;
        }

        public void AsteroidHit()
        {
            WaveScore += AsteroidHitValue;
            TotalScore += AsteroidHitValue;
            if (ScoreChanged != null)
            {
                ScoreChanged.Invoke(this, null);
            }
            if (WaveScore >= _maxScoreForWave - MaximumScoreTolerance && OnMaxScoreReached != null)
            {
                OnMaxScoreReached.Invoke(this, null);
            }
        }

        public void Reset()
        {
            TotalScore += WaveScore;
            WaveScore = 0;
            _maxScoreForWave = CalculateMaxScoreForWave();
        }

    }
}
