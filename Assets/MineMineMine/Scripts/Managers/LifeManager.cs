﻿using UnityEngine;
using UnityEngine.UI;

namespace Assets.MineMineMine.Scripts.Managers
{
	public class LifeManager : MonoBehaviour
	{

		public int StartingLives = 3;
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
			DebugTextField.text = "Emergency teleports left: " + _livesLeft;
		}

		public bool NoLivesLeft()
		{
			return _livesLeft <= 0;
		}

		public void Die()
		{
			if (NoLivesLeft())
			{
				SpawnNuke();
				PlayerAlive = false;
			}
			else
			{
				SceneReference.RespawnManager.ShowReticle();
			}
		}

		private void SpawnNuke()
		{
			var nukeSpawnPoint = SceneReference.PlayerSpawnManager.GetCentralPlayer();
			Instantiate(PrefabReference.Nuke, nukeSpawnPoint.transform.position, PrefabReference.Nuke.transform.rotation);
		}


		public void DecreaseLifeCount()
		{
			--_livesLeft;
			UpdateDebugTextField();
		}
	}
}
