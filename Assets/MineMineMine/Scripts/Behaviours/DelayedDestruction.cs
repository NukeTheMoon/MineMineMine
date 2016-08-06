using PigeonCoopToolkit.Effects.Trails;
using ReptilianCabal.MineMineMine;
using UnityEngine;

public class DelayedDestruction : MonoBehaviour
{

	private Weapon _weaponType;

	private void Start()
	{
		_weaponType = SceneReference.MissileSpawnManager.WeaponTypeOfMissileId[gameObject.GetInstanceID()];
	}


	public void InitiateDestruction()
	{
		switch (_weaponType)
		{
			case Weapon.PulseEmitter:
				InitiatePulseEmitterDelayedDestruction();
				break;
			default:
				break;
		}
	}

	private void InitiatePulseEmitterDelayedDestruction()
	{

		// Pulse emitter missile gameObjects need to be destroyed with a delay, because of the trail they leave. If they were destroyed immediately, 
		// the trail would also suddenly vanish. So  instead they are first hidden, and only destroyed after the trail decays on its own.

		Trail trail = GetComponentInChildren<Trail>();

		Instantiate(PrefabReference.ExplosionSmall, transform.position, Random.rotation);

		if (trail != null)
		{
			CapsuleCollider _collider = GetComponent<CapsuleCollider>();
			Rigidbody _rigidbody = GetComponent<Rigidbody>();
			MeshRenderer _mesh = GetComponentInChildren<MeshRenderer>();

			if (_collider != null) _collider.enabled = false;
			if (_rigidbody != null) _rigidbody.velocity = Vector3.zero;
			if (_mesh != null) _mesh.enabled = false;

			StartCoroutine(gameObject.DestroyAfterTime(trail.TrailData.Lifetime));
		}
		else Destroy(gameObject);

		SceneReference.ShotManager.DestroyMissilesFromSameShot(gameObject);
	}
}
