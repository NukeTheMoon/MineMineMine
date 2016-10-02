using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.MineMineMine.Scripts.Managers
{
    public class MissileSpawnManager : MonoBehaviour
    {
        public Transform World;

        public Dictionary<int, Weapon> WeaponTypeOfMissileId { get; private set; }


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
            SceneReference.MissileSpawnManager = this;
        }

        public void Shoot()
        {
            if (SceneReference.WeaponManager.CanFire())
            {
                Guid shotId = Guid.NewGuid();
                GameObject[] missileSpawnPoints = GameObject.FindGameObjectsWithTag("MissileSpawnPoint");
                Weapon currentWeapon = SceneReference.WeaponManager.CurrentWeapon;
                for (var i = 0; i < missileSpawnPoints.Length; ++i)
                {
                    Transform missileSpawnPoint = missileSpawnPoints[i].GetComponent<Transform>();
                    switch (currentWeapon)
                    {
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
                SceneReference.MeterManager.ExpendMeter(currentWeapon);

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
}
