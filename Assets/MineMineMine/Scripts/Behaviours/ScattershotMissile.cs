using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScattershotMissile : MonoBehaviour
{
    private float _initialWidth;
    private float _expansionProgress;
    private DelayedDestruction _delayedDestruction;
    private ParticleSystem _particleSystem;
    private ParticleSystem.Particle[] _particles;
    private List<Vector3> _initialParticlePositions;
    private int _particleCount;

    private void Start()
    {
        _initialWidth = transform.localScale.x;
        _expansionProgress = 0.0f;
        _particleCount = 0;
        _delayedDestruction = GetComponent<DelayedDestruction>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        StartCoroutine(ExpiryCoroutine());
    }


    private IEnumerator ExpiryCoroutine()
    {
        yield return new WaitForSeconds(TimeHelper.MillisecondsToSeconds(SceneReference.WeaponManager.ScattershotLifeMs));
        _delayedDestruction.InitiateDestruction();
    }

    private void Expand()
    {
        _expansionProgress += Time.deltaTime / TimeHelper.MillisecondsToSeconds(SceneReference.WeaponManager.ScattershotLifeMs);
        transform.localScale = new Vector3(
            Mathf.Lerp(
                _initialWidth,
                SceneReference.WeaponManager.ScattershotTargetWidth,
                _expansionProgress),
            transform.localScale.y,
            transform.localScale.z);
        if (_particleCount > 0)
        {
            for (int i = 0; i < _particleCount; ++i)
            {
                Vector3 formerPosition = _particles[i].position;
                _particles[i].position = new Vector3(
                    ProportionHelper.LinearOutput(
                        _initialWidth,
                        transform.localScale.x,
                        _initialParticlePositions[i].x),
                    formerPosition.y,
                    formerPosition.z);

                // lifetime appears to be reset back to startLifetime after calling SetParticles(), so it's lerped to always
                // be in sync with width expansion

                _particles[i].lifetime = Mathf.Lerp(_particleSystem.startLifetime, 0, _expansionProgress);
            }
        }
        else
        {
            AdjustBurstAmount();
            GetParticles();
        }
        _particleSystem.SetParticles(_particles, _particleCount);
    }

    private void AdjustBurstAmount()
    {
        ParticleSystem.Burst[] bursts = new ParticleSystem.Burst[_particleSystem.emission.burstCount];
        _particleSystem.emission.GetBursts(bursts); // the parameter for GetBursts() is actually out

        // referenceWidth and desiredBurstAmountForReferenceWidth are arbitrary, require manual adjustment if particle effect is changed
        const int referenceWidth = 8;
        const int desiredBurstAmountForReferenceWidth = 30;

        short desiredBurstAmount = (short)ProportionHelper.LinearOutput(
            referenceWidth,
            SceneReference.WeaponManager.ScattershotTargetWidth,
            desiredBurstAmountForReferenceWidth);
        bursts[0].maxCount = desiredBurstAmount;
        bursts[0].minCount = desiredBurstAmount;
        _particleSystem.emission.SetBursts(bursts);
    }

    private void GetParticles()
    {
        _particles = new ParticleSystem.Particle[_particleSystem.maxParticles];
        _particleCount = _particleSystem.GetParticles(_particles); // the parameter for GetParticles() is actually out
        _initialParticlePositions = new List<Vector3>();
        for (int i = 0; i < _particleCount; ++i)
        {
            _initialParticlePositions.Add(_particles[i].position);
        }
    }

    private void Update()
    {
        Expand();
    }

}
