using UnityEngine;
using System.Collections;
using System;

public class RespawnManager : MonoBehaviour {

    public Transform ReticleSpawnPoint;
    public int RespawnWeaponCooldownMs;

    public bool Respawning { get; private set; }

    private GameObject _reticle;

    private void Awake()
    {
        RegisterWithSceneReference();
    }

    private void Start()
    {
        Respawning = false;
    }

    private void Update()
    {
        if (_reticle != null && Input.GetKeyDown(KeyCode.Space))
        {
            SceneReference.PlayerSpawner.Spawn(_reticle.transform);
            Destroy(_reticle);
            SceneReference.LifeManager.DecreaseLifeCount();
            SceneReference.WeaponManager.InitiateCooldown(RespawnWeaponCooldownMs);
            Respawning = false;
        }
    }

    private void RegisterWithSceneReference()
    {
        SceneReference.RespawnManager = this;
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
