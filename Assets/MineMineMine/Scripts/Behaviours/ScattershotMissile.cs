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

                // lifetime appears to be reset back to startLifetime  after calling SetParticles(), so it's lerped to always
                // be in sync with width expansion

                _particles[i].lifetime = Mathf.Lerp(_particleSystem.startLifetime, 0, _expansionProgress);
            }
        }
        else
        {
            GetParticles();
        }
        _particleSystem.SetParticles(_particles, _particleCount);
    }

    private void GetParticles()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
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
