using System.Collections.Generic;
using UnityEngine;

namespace Assets.MineMineMine.Scripts.Managers
{
	public class AsteroidDivisionManager : MonoBehaviour
	{

		public int Divisor = 2;
		[Range(0, 6)]
		public int MinimumDivisions = 3;
		[Range(0, 6)]
		public int MaximumDivisions = 6;
		[Range(0, 6)]
		public int Divisions = 6;
		public float PositionJitter = 1;
		public float Force = 100;
		public Dictionary<int, int> DivisionReference = new Dictionary<int, int>();

		private void Awake()
		{
			RegisterWithSceneReference();
		}

		public void RegisterWithSceneReference()
		{
			SceneReference.AsteroidDivisionManager = this;
		}
	}
}
