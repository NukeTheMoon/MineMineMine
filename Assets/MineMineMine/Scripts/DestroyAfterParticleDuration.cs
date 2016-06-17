using UnityEngine;
using System.Collections;

public class DestroyAfterParticleDuration : MonoBehaviour
{

    private ParticleSystem _particle;

    private void Start()
    {
        _particle = GetComponent<ParticleSystem>();
        StartCoroutine(DestructionCountdown());

    }

    private void Update ()
	{
	}

    private IEnumerator DestructionCountdown()
    {
        yield return new WaitForSeconds(_particle.duration);
        Destroy(gameObject);
    }
}
