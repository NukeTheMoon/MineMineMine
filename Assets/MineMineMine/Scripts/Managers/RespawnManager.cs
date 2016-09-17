using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.MineMineMine.Scripts.Managers
{
    public class RespawnManager : MonoBehaviour
    {

        public Transform ReticleSpawnPoint;
        public int RespawnWeaponCooldownMs;
        public int RespawnInvulnerabilityMs;
        public float RepulsionRadius;
        public float RepulsionForce;

        public bool Respawning { get; private set; }
        public bool RespawnGracePeriod { get; private set; }

        private GameObject _reticle;

        private void Awake()
        {
            RegisterWithSceneReference();
        }

        private void RegisterWithSceneReference()
        {
            SceneReference.RespawnManager = this;
        }

        private void Start()
        {
            Respawning = false;
            SceneReference.ShieldManager.OnInvulnerabilityEnd += ShieldManager_OnInvulnerabilityEnd;
        }

        private void ShieldManager_OnInvulnerabilityEnd(object sender, EventArgs e)
        {
            RespawnGracePeriod = false;
        }

        public void Respawn()
        {
            SceneReference.PlayerSpawnManager.Spawn(_reticle.transform);
            Destroy(_reticle);
            SceneReference.LifeManager.DecreaseLifeCount();
            SceneReference.WeaponManager.StartGlobalCooldown(RespawnWeaponCooldownMs);
            SceneReference.ShieldManager.StartInvulnerabilityTimed(RespawnInvulnerabilityMs);
            RespawnGracePeriod = true;
            Respawning = false;
        }

        public void ShowReticle()
        {
            if (_reticle == null)
            {
                Respawning = true;
                _reticle =
                    (GameObject)
                        Instantiate(PrefabReference.Reticle, ReticleSpawnPoint.position, ReticleSpawnPoint.rotation);
            }
        }
    }
}
