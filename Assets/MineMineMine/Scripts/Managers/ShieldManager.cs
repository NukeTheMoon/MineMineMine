using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShieldManager : MonoBehaviour
{
    public bool Invulnerability { get; private set; }
    private List<GameObject> _protectionRings;

    public event EventHandler OnInvulnerabilityStart = delegate { };
    public event EventHandler OnInvulnerabilityEnd = delegate { };

    private void Awake()
    {
        RegisterWithSceneReference();
    }

    private void Start()
    {
        _protectionRings = new List<GameObject>();

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
            _protectionRings.Add(protectionRing);
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
        for (var i = 0; i < _protectionRings.Count; ++i)
        {
            Destroy(_protectionRings[i]);
        }
        _protectionRings = new List<GameObject>();
    }

}
