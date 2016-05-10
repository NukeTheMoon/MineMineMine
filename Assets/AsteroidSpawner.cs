using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidSpawner : MonoBehaviour {

    public Transform AsteroidSpawnBoundaryA;
    public Transform AsteroidSpawnBoundaryB;
    public Transform AsteroidSpawnBoundaryC;
    public Transform AsteroidSpawnBoundaryD;
    public GameObject AsteroidPrefab;
    public PlayerSpawner PlayerSpawner;
    public float AsteroidSpeed;
    public float StartDelaySeconds;
    public float IntervalSeconds;
    public int MaxAsteroidCount;

    private int _asteroidsSpawnedCount;

    public void Start()
    {
        InvokeRepeating("Spawn", StartDelaySeconds, IntervalSeconds);
    }

    public void Spawn()
    {
        if (_asteroidsSpawnedCount++ < MaxAsteroidCount)
        {
            var spawnEdge = Random.Range(0, 4);
            var spawnVerticalPosition = 0.0f;
            var spawnHorizontalPosition = 0.0f;

            switch (spawnEdge)
            {
                case 0: // along AB
                    spawnVerticalPosition = AsteroidSpawnBoundaryA.position.z;
                    spawnHorizontalPosition = Random.Range(AsteroidSpawnBoundaryA.position.x, AsteroidSpawnBoundaryB.position.x);
                    break;
                case 1: // along BC
                    spawnVerticalPosition = Random.Range(AsteroidSpawnBoundaryC.position.z, AsteroidSpawnBoundaryB.position.z);
                    spawnHorizontalPosition = AsteroidSpawnBoundaryB.position.x;
                    break;
                case 2: // along CD
                    spawnVerticalPosition = AsteroidSpawnBoundaryC.position.z;
                    spawnHorizontalPosition = Random.Range(AsteroidSpawnBoundaryD.position.x, AsteroidSpawnBoundaryC.position.x);
                    break;
                case 3: // along DA
                    spawnVerticalPosition = Random.Range(AsteroidSpawnBoundaryD.position.z, AsteroidSpawnBoundaryA.position.z);
                    spawnHorizontalPosition = AsteroidSpawnBoundaryD.position.x;
                    break;
                default:
                    Debug.LogError("Spawning asteroid along an edge that doesn't exist");
                    break;
            }

            var asteroid = (GameObject)Instantiate(AsteroidPrefab, new Vector3(spawnHorizontalPosition, PlayerSpawner.PlayerSpawnPoint.position.y, spawnVerticalPosition), Quaternion.identity);
            PropelForward(asteroid);
        }
    }

    public void SpawnSmaller(Vector3 position)
    {
        var smallerAsteroid = (GameObject)Instantiate(AsteroidPrefab, position, Quaternion.identity); // why (clone)(clone)??
    }

    void PropelForward(GameObject asteroid)
    {
        var direction = PlayerSpawner.GetCentralPlayer().transform.position - asteroid.transform.position;
        var rigidbody = asteroid.GetComponent<Rigidbody>();
        rigidbody.AddForce(direction * AsteroidSpeed);
        rigidbody.drag = 0;
    }

}
