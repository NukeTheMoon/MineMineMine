using PigeonCoopToolkit.Effects.Trails;
using ReptilianCabal.MineMineMine;
using UnityEngine;

public class DelayedDestruction : MonoBehaviour
{

	private Weapon _weaponType;

	private void Start()
	{
		_weaponType = SceneReference.MissileSpawner.WeaponTypeOfMissileId[gameObject.GetInstanceID()];
	}


	public void InitiateDestruction()
	{
		switch (_weaponType)
		{
			case Weapon.PulseEmitter:
				PulseEmitterBehaviour();
				break;
			default:
				break;
		}
	}

	private void PulseEmitterBehaviour()
	{
		CapsuleCollider _collider = GetComponent<CapsuleCollider>();
		Rigidbody _rigidbody = GetComponent<Rigidbody>();
		MeshRenderer _mesh = GetComponentInChildren<MeshRenderer>();
		Trail trail = GetComponentInChildren<Trail>();


		Instantiate(PrefabReference.ExplosionSmall, transform.position, Random.rotation);

		if (_collider != null) _collider.enabled = false;

		if (_rigidbody != null) _rigidbody.velocity = Vector3.zero;

		if (_mesh != null) _mesh.enabled = false;

		if (trail != null) StartCoroutine(gameObject.DestroyAfterTime(trail.TrailData.Lifetime));
		else Destroy(gameObject);

		SceneReference.ShotManager.DestroyMissilesFromSameShot(gameObject);
	}
}
