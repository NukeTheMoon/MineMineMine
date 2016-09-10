using UnityEngine;
using System.Collections;
using ReptilianCabal.MineMineMine;

public class SelfDestructAfterParticleDuration : MonoBehaviour
{

    private ParticleSystem _particleSystem;

    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        StartCoroutine(gameObject.DestroyAfterSeconds(_particleSystem.duration));
    }



}
