using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MineMineMine.Scripts.Managers
{
	public class ScorekeepingManager : MonoBehaviour {

		public int Score { get; set; }
		public Text ScoreDebugTextField;

		private int _maxScoreForWave;
		private int _asteroidHitValue = 100;
		private int _totalScore;

		public event EventHandler OnMaxScoreReached;

		private void Awake()
		{
			RegisterWithSceneReference();
		}

		private void Start()
		{
			_totalScore = 0;
			Score = 0;
			Reset();
		}

		private void RegisterWithSceneReference()
		{
			SceneReference.ScorekeepingManager = this;
		}

		private int CalculateMaxScoreForWave()
		{
			int totalAsteroidFragments = 0;
			for (var i=0; i <= SceneReference.AsteroidDivisionManager.Divisions; ++i)
			{
				totalAsteroidFragments += (int)Math.Pow(SceneReference.AsteroidDivisionManager.Divisor, i);
			}
			return SceneReference.AsteroidSpawnManager.MaxAsteroidCount * totalAsteroidFragments * _asteroidHitValue;
		}

		public void AsteroidHit()
		{
			Score += _asteroidHitValue;
			UpdateScoreDebugTextField();
			if (Score >= _maxScoreForWave && OnMaxScoreReached != null)
			{
				OnMaxScoreReached.Invoke(this, null);
			}
		}

		public void Reset()
		{
			_totalScore += Score;
			Score = 0;
			_maxScoreForWave = CalculateMaxScoreForWave();
			UpdateScoreDebugTextField();
		}

		private void UpdateScoreDebugTextField()
		{
			ScoreDebugTextField.text = "Score: " + Score + "/" + _maxScoreForWave;
		}

	}
}
