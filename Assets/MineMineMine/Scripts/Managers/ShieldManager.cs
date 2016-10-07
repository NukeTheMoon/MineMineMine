using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShieldManager : MonoBehaviour
{
    public bool Invulnerability { get; private set; }
    public float RadiusMultiplier = 1.0f;
    private List<GameObject> _shields;

    public event EventHandler OnInvulnerabilityStart = delegate { };
    public event EventHandler OnInvulnerabilityEnd = delegate { };

    private void Awake()
    {
        RegisterWithSceneReference();
    }

    private void Start()
    {
        _shields = new List<GameObject>();

    }

    private void RegisterWithSceneReference()
    {
        SceneReference.ShieldManager = this;
    }

    private void Update()
    {
        MeterBurn();
        if (!SceneReference.MeterManager.HaveEnoughMeter(MeterAction.Shield))
        {
            StopInvulnerability();
        }
    }

    private void MeterBurn()
    {
        if (Invulnerability && !SceneReference.RespawnManager.RespawnGracePeriod)
        {
            SceneReference.MeterManager.ExpendMeter(MeterAction.Shield);
        }
    }

    public void StartInvulnerability()
    {
        Invulnerability = true;
        OnInvulnerabilityStart(this, null);
        CreateProtectionRings();
    }

    public void StartInvulnerabilityTimed(float time)
    {
        StartInvulnerability();
        StartCoroutine(InvulnerabilityCoroutine(time));
    }

    private IEnumerator InvulnerabilityCoroutine(float time)
    {
        yield return new WaitForSeconds(TimeHelper.MillisecondsToSeconds(time));
        StopInvulnerability();
    }

    private void CreateProtectionRings()
    {
        foreach (var player in SceneReference.PlayerSpawnManager.GetAllPlayers())
        {
            var protectionRing =
                (GameObject)
                    Instantiate(PrefabReference.ProtectionRing,
                        player.transform.position, Quaternion.identity);
            protectionRing.transform.parent = player.transform;
            protectionRing.transform.localScale = new Vector3(RadiusMultiplier, RadiusMultiplier, RadiusMultiplier);
            _shields.Add(protectionRing);
        }
    }

    public void StopInvulnerability()
    {
        Invulnerability = false;
        DestroyProtectionRings();
        OnInvulnerabilityEnd(this, null);
    }

    private void DestroyProtectionRings()
    {
        for (var i = 0; i < _shields.Count; ++i)
        {
            Destroy(_shields[i]);
        }
        _shields = new List<GameObject>();
    }

}
