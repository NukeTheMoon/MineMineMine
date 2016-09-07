using PigeonCoopToolkit.Effects.Trails;
using ReptilianCabal.MineMineMine;
using UnityEngine;

public class DelayedDestruction : MonoBehaviour
{
    // Some missile gameObjects need to be destroyed with a delay, because of the trail or other visual effects they leave. If they 
    // were destroyed immediately, the trail would also suddenly vanish. So instead they are first hidden, and only destroyed after 
    // the trail decays on its own.

    private Weapon _weaponType;

    private void Start()
    {
        _weaponType = SceneReference.MissileSpawnManager.WeaponTypeOfMissileId[gameObject.GetInstanceID()];
    }

    // InitiateDestruction() will be called instead of Destroy() by ShotManager in the event of a destructive collision when the 
    // object has DelayedDestruction attached to it.

    public void InitiateDestruction()
    {
        switch (_weaponType)
        {
            case Weapon.PulseEmitter:
                InitiatePulseEmitterDelayedDestruction();
                break;
            case Weapon.Scattershot:
                InitiateScattershotDelayedDestruction();
                break;
            default:
                break;
        }
    }

    private void InitiatePulseEmitterDelayedDestruction()
    {
        int missileId = gameObject.GetInstanceID();
        Trail trail = GetComponentInChildren<Trail>();

        if (trail != null)
        {
            CapsuleCollider _collider = GetComponent<CapsuleCollider>();
            Rigidbody _rigidbody = GetComponent<Rigidbody>();
            MeshRenderer _mesh = GetComponentInChildren<MeshRenderer>();

            if (_collider != null) _collider.enabled = false;
            if (_rigidbody != null) _rigidbody.velocity = Vector3.zero;
            if (_mesh != null) _mesh.enabled = false;

            StartCoroutine(gameObject.DestroyAfterSeconds(trail.TrailData.Lifetime));
        }
        else Destroy(gameObject);

        SceneReference.ShotManager.DestroyMissilesFromSameShot(missileId);
    }

    private void InitiateScattershotDelayedDestruction()
    {
        int missileId = gameObject.GetInstanceID();
        ParticleSystem particleSystem = GetComponentInChildren<ParticleSystem>();

        if (particleSystem != null)
        {
            BoxCollider _collider = GetComponent<BoxCollider>();
            Rigidbody _rigidbody = GetComponent<Rigidbody>();

            if (_collider != null) _collider.enabled = false;
            if (_rigidbody != null) _rigidbody.velocity = Vector3.zero;

            StartCoroutine(gameObject.DestroyAfterSeconds(particleSystem.subEmitters.birth0.duration));
        }
        else Destroy(gameObject);

        SceneReference.ShotManager.DestroyMissilesFromSameShot(missileId);
    }
}
