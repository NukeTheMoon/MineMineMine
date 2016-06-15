using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MissileSpawner : MonoBehaviour
{
    public Transform World;

    public Dictionary<int, Weapon> WeaponTypeOfMissileId { get; private set; }

    private GameObject[] _missileSpawnPoints;


    private void Awake()
    {
        RegisterWithSceneReference();

    }

    private void Start()
    {
        WeaponTypeOfMissileId = new Dictionary<int, Weapon>();
    }

    private void RegisterWithSceneReference()
    {
        SceneReference.MissileSpawner = this;
    }

    private void Update () {
	    if (Input.GetKeyDown(KeyCode.Space) && SceneReference.WeaponManager.CanFire())
	    {
            Guid shotId = Guid.NewGuid();
            _missileSpawnPoints = GameObject.FindGameObjectsWithTag("MissileSpawnPoint");
            for (var i=0; i<_missileSpawnPoints.Length; ++i)
            {
                Transform missileSpawnPoint = _missileSpawnPoints[i].GetComponent<Transform>();
                switch (SceneReference.WeaponManager.CurrentWeapon) {
                    case (Weapon.PulseEmitter):
                        SpawnPulseMissile(shotId, missileSpawnPoint);
                        break;
                    case (Weapon.Scattershot):
                        SpawnScattershotMissile(shotId, missileSpawnPoint);
                        break;
                    case (Weapon.Railgun):
                        SpawnRailgunMissile(shotId, missileSpawnPoint);
                        break;
                    default:
                        SpawnPulseMissile(shotId, missileSpawnPoint);
                        break;
                }             
            }
            SceneReference.WeaponManager.ConsumeAmmo();
            SceneReference.WeaponManager.InitiateCooldown();
	    }

	}

    public void SpawnRailgunMissile(Guid shotId, Transform missileSpawnPoint)
    {
        var missile = (GameObject)Instantiate(PrefabReference.RailgunMissile, missileSpawnPoint.transform.position, missileSpawnPoint.transform.parent.rotation);
        WeaponTypeOfMissileId.Add(missile.GetInstanceID(), Weapon.Railgun);
        SceneReference.ShotManager.RegisterShot(shotId, missile);
        missile.transform.parent = World;
    }

    public void SpawnScattershotMissile(Guid shotId, Transform missileSpawnPoint)
    {
        var missile = (GameObject)Instantiate(PrefabReference.ScattershotMissile, missileSpawnPoint.transform.position, missileSpawnPoint.transform.parent.rotation);
        WeaponTypeOfMissileId.Add(missile.GetInstanceID(), Weapon.Scattershot);
        SceneReference.ShotManager.RegisterShot(shotId, missile);
        missile.transform.parent = World;
        PropelForward(missile, missileSpawnPoint, SceneReference.WeaponManager.ScattershotForce, SceneReference.WeaponManager.ScattershotDrag);
    }

    public void SpawnPulseMissile(Guid shotId, Transform missileSpawnPoint)
    {
        var missile = (GameObject)Instantiate(PrefabReference.PulseMissile, missileSpawnPoint.transform.position, missileSpawnPoint.transform.parent.rotation);
        WeaponTypeOfMissileId.Add(missile.GetInstanceID(), Weapon.PulseEmitter);
        SceneReference.ShotManager.RegisterShot(shotId, missile);
        missile.transform.parent = World;
        PropelForward(missile, missileSpawnPoint, SceneReference.WeaponManager.PulseForce, SceneReference.WeaponManager.PulseDrag);
    }


    private void PropelForward(GameObject missile, Transform missileSpawnPoint, float force, float drag)
    {
        var direction = missileSpawnPoint.transform.parent.forward;
        var rigidbody = missile.GetComponent<Rigidbody>();
        rigidbody.AddForce(direction * force);
        rigidbody.drag = drag;
    }
}
