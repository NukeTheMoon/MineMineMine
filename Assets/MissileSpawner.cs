using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MissileSpawner : MonoBehaviour
{
    public Transform World;
    public GameObject MissilePrefab;
    public float MissileSpeed = 1000.0f;    

    private GameObject[] _missileSpawnPoints;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
            Guid shotId = Guid.NewGuid();
            _missileSpawnPoints = GameObject.FindGameObjectsWithTag("MissileSpawnPoint");
            for (var i=0; i<_missileSpawnPoints.Length; ++i)
            {
                Transform missileSpawnPoint = _missileSpawnPoints[i].GetComponent<Transform>();
                var missile = (GameObject) Instantiate(MissilePrefab, missileSpawnPoint.transform.position, missileSpawnPoint.transform.parent.rotation);
                ShotManager.RegisterShot(shotId, missile);
	            missile.transform.parent = World;
                PropelForward(missile, missileSpawnPoint);
            }
	    }
	}

    void PropelForward(GameObject missile, Transform missileSpawnPoint)
    {
        var direction = missileSpawnPoint.transform.parent.forward;
        var rigidbody = missile.GetComponent<Rigidbody>();
        rigidbody.AddForce(direction * MissileSpeed);
        rigidbody.drag = 0;
    }
}
