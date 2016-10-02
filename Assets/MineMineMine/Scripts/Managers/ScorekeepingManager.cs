using System;
using TeamUtility.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MineMineMine.Scripts.Managers
{
    public class ScorekeepingManager : MonoBehaviour
    {
        public int WaveScore { get; private set; }
        public int TotalScore { get; private set; }
        public int CurrentScore { get; private set; }
        public int AsteroidHitValue = 10;
        public int MaximumScoreTolerance;

        private int _maxScoreForWave;

        public event EventHandler OnMaxScoreReached;
        public event EventHandler OnScoreChanged;

        private void Awake()
        {
            RegisterWithSceneReference();
        }

        private void RegisterWithSceneReference()
        {
            SceneReference.ScorekeepingManager = this;
        }

        private void Start()
        {
            TotalScore = 0;
            WaveScore = 0;
            CurrentScore = 0;
            ResetWaveScore();
        }

        private void Update()
        {
            if (InputManager.GetKeyUp(KeyCode.Y))
            {
                MoneyCheat();
            }
        }

        private void MoneyCheat()
        {
            WaveScore += 100000;
            TotalScore += 100000;
            CurrentScore += 100000;
            OnScoreChanged.Invoke(this, null);
        }

        public bool TrySpend(int amount)
        {
            if (amount <= CurrentScore)
            {
                CurrentScore -= amount;
                OnScoreChanged.Invoke(this, null);
                return true;
            }
            return false;
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
            CurrentScore += AsteroidHitValue;
            if (OnScoreChanged != null)
            {
                OnScoreChanged.Invoke(this, null);
            }
            if (WaveScore >= _maxScoreForWave - MaximumScoreTolerance && OnMaxScoreReached != null)
            {
                OnMaxScoreReached.Invoke(this, null);
            }
        }

        public void ResetWaveScore()
        {
            WaveScore = 0;
            _maxScoreForWave = CalculateMaxScoreForWave();
        }

    }
}
