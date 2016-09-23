using PigeonCoopToolkit.Effects.Trails;
using System.Collections;
using UnityEngine;

public class Yield : MonoBehaviour
{

    public float Speed;
    public float DistanceToStop;
    public float ExplosionLightStrength;

    private static Transform _target;
    private Rigidbody _rigidbody;
    private Trail _trail;
    private ParticleSystem _explosion;
    private bool _approaching;

    private void PlayerSpawner_OnCentralPlayerChanged(object sender, System.EventArgs e)
    {
        UpdateTarget();
    }

    private void Start()
    {
        UpdateTarget();
        _rigidbody = GetComponent<Rigidbody>();
        _trail = GetComponent<Trail>();
        _explosion = transform.GetChild(1).GetComponent<ParticleSystem>();
        _approaching = true;
        SceneReference.PlayerSpawnManager.OnCentralPlayerChanged += PlayerSpawner_OnCentralPlayerChanged;
    }

    private void UpdateTarget()
    {
        if (_target == null)
        {
            _target = SceneReference.PlayerSpawnManager.GetCentralPlayer().GetComponent<Transform>();
        }
    }

    private void FixedUpdate()
    {
        if (_rigidbody != null && _approaching)
        {
            ApproachTarget();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == TagsReference.PLAYER)
        {
            PlayerHit();
        }
    }

    private void PlayerHit()
    {
        GameObject meshObject = transform.GetChild(0).gameObject;

        _approaching = false;
        _rigidbody.isKinematic = true;
        _explosion.Play();
        SceneReference.ScorekeepingManager.AsteroidHit();
        Destroy(meshObject);
        StartCoroutine(DestroyAfterTrailLifetime());
    }

    private IEnumerator DestroyAfterTrailLifetime()
    {
        yield return new WaitForSeconds(_trail.TrailData.Lifetime);
        Destroy(gameObject);
    }

    private void LimitSpeed()
    {
        float currentSpeed = Vector3.Magnitude(_rigidbody.velocity);
        if (currentSpeed > Speed / 100)

        {
            float brakeSpeed = currentSpeed - Speed / 100;

            Vector3 normalizedVelocity = _rigidbody.velocity.normalized;
            Vector3 brakeVelocity = normalizedVelocity * brakeSpeed;

            _rigidbody.AddForce(-brakeVelocity);
        }
    }

    private void ApproachTarget()
    {
        transform.LookAt(_target);
        _rigidbody.AddRelativeForce(Vector3.forward * Speed * Time.deltaTime, ForceMode.Force);
    }

}
