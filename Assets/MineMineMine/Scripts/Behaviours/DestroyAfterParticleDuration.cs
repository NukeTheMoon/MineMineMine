using System.Collections;
using UnityEngine;

public class DestroyAfterParticleDuration : MonoBehaviour
{

	private ParticleSystem _particle;

	private void Start()
	{
		_particle = GetComponent<ParticleSystem>();
		StartCoroutine(DestructionCountdown());

	}

	private IEnumerator DestructionCountdown()
	{
		yield return new WaitForSeconds(_particle.duration);
		Destroy(gameObject);
	}
}
