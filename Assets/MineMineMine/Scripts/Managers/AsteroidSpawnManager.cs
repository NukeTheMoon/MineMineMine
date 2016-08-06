using UnityEngine;

namespace Assets.MineMineMine.Scripts.Managers
{
	public class AsteroidSpawnManager : MonoBehaviour
	{

		public Transform AsteroidSpawnBoundaryA;
		public Transform AsteroidSpawnBoundaryB;
		public Transform AsteroidSpawnBoundaryC;
		public Transform AsteroidSpawnBoundaryD;
		public float AsteroidSpeed = 5;
		public float StartDelaySeconds = 3;
		public float IntervalSeconds = 10;
		public int MinAsteroidCount = 2;
		public int MaxAsteroidCount = 6;

		private int _asteroidsSpawnedCount;

		private void Awake()
		{
			RegisterWithSceneReference();

		}

		private void RegisterWithSceneReference()
		{
			SceneReference.AsteroidSpawnManager = this;
		}

		public void StartSpawning()
		{
			_asteroidsSpawnedCount = 0;
			InvokeRepeating("Spawn", StartDelaySeconds, IntervalSeconds);
		}

		private void Spawn()
		{
			if (_asteroidsSpawnedCount++ < MaxAsteroidCount)
			{
				var spawnEdge = UnityEngine.Random.Range(0, 4);
				var spawnVerticalPosition = 0.0f;
				var spawnHorizontalPosition = 0.0f;

				switch (spawnEdge)
				{
					case 0: // along AB
						spawnVerticalPosition = AsteroidSpawnBoundaryA.position.z;
						spawnHorizontalPosition = UnityEngine.Random.Range(AsteroidSpawnBoundaryA.position.x,
							AsteroidSpawnBoundaryB.position.x);
						break;
					case 1: // along BC
						spawnVerticalPosition = UnityEngine.Random.Range(AsteroidSpawnBoundaryC.position.z,
							AsteroidSpawnBoundaryB.position.z);
						spawnHorizontalPosition = AsteroidSpawnBoundaryB.position.x;
						break;
					case 2: // along CD
						spawnVerticalPosition = AsteroidSpawnBoundaryC.position.z;
						spawnHorizontalPosition = UnityEngine.Random.Range(AsteroidSpawnBoundaryD.position.x,
							AsteroidSpawnBoundaryC.position.x);
						break;
					case 3: // along DA
						spawnVerticalPosition = UnityEngine.Random.Range(AsteroidSpawnBoundaryD.position.z,
							AsteroidSpawnBoundaryA.position.z);
						spawnHorizontalPosition = AsteroidSpawnBoundaryD.position.x;
						break;
					default:
						Debug.LogError("Spawning asteroid along an edge that doesn't exist");
						break;
				}

				var asteroid =
					(GameObject)
						Instantiate(PrefabReference.Asteroid,
							new Vector3(spawnHorizontalPosition, SceneReference.PlayerSpawnManager.PlayerSpawnPoint.position.y,
								spawnVerticalPosition), Quaternion.identity);
				PropelForward(asteroid);
			}
			else
			{
				CancelInvoke("Spawn");
			}
		}

		public void SpawnSmaller(Vector3 position)
		{
			var smallerAsteroid = (GameObject)Instantiate(PrefabReference.Asteroid, position, Quaternion.identity); // why (clone)(clone)??
		}

		private void PropelForward(GameObject asteroid)
		{
			var direction = SceneReference.PlayerSpawnManager.GetCentralPlayer().transform.position - asteroid.transform.position;
			var rigidbody = asteroid.GetComponent<Rigidbody>();
			rigidbody.AddForce(direction * AsteroidSpeed);
			rigidbody.drag = 0;
		}

	}
}
