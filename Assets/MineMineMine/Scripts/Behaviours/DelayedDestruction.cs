using System.Collections.Generic;
using System.Linq;
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

    // InitiateDestruction() should be called instead of Destroy() by ShotManager in the event of a destructive collision when the 
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
            CapsuleCollider collider = GetComponent<CapsuleCollider>();
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            List<MeshRenderer> meshes = GetComponentsInChildren<MeshRenderer>().ToList();

            if (collider != null) collider.enabled = false;
            if (rigidbody != null) rigidbody.velocity = Vector3.zero;
            if (meshes.Count > 0)
            {
                for (var i = 0; i < meshes.Count; ++i)
                {
                    meshes[i].enabled = false;
                }
            }

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
            BoxCollider collider = GetComponent<BoxCollider>();
            Rigidbody rigidbody = GetComponent<Rigidbody>();

            if (collider != null) collider.enabled = false;
            if (rigidbody != null) rigidbody.velocity = Vector3.zero;

            StartCoroutine(gameObject.DestroyAfterSeconds(particleSystem.subEmitters.birth0.duration));
        }
        else Destroy(gameObject);

        SceneReference.ShotManager.DestroyMissilesFromSameShot(missileId);
    }
}
