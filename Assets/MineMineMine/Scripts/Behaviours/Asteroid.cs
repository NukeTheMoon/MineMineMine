using UnityEngine;

public class Asteroid : MonoBehaviour
{

	private bool _quitting;

	private void Start()
	{
		_quitting = false;
	}

	private void Update()
	{

	}

	private void OnApplicationQuit()
	{
		_quitting = true;
	}

	private void OnDestroy()
	{
		if (_quitting) return;

		Explode();
	}

	private void Explode()
	{
		GameObject explosion =
			(GameObject)Instantiate(PrefabReference.ExplosionMedium, transform.position, Random.rotation);
		explosion.transform.Translate(new Vector3(0, 5, 0), null);
	}
}
