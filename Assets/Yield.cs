using UnityEngine;
using System.Collections;
using System;

public class Yield : MonoBehaviour {

    public float Speed;
    public float DistanceToStop;

    private static Transform _target;
    private Rigidbody _rigidbody;

    private void Start()
    {
        UpdateTarget();
        _rigidbody = GetComponent<Rigidbody>();
        SceneReference.PlayerSpawner.OnCentralPlayerChanged += PlayerSpawner_OnCentralPlayerChanged;
    }

    private void UpdateTarget()
    {
        if (_target == null)
        {
            _target = SceneReference.PlayerSpawner.GetCentralPlayer().GetComponent<Transform>();
        }
    }

    private void PlayerSpawner_OnCentralPlayerChanged(object sender, System.EventArgs e)
    {
        UpdateTarget();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (_rigidbody != null)
        {
            ApproachTarget();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == TagsReference.PLAYER)
        {
            SceneReference.ScoreKeeper.AsteroidHit();
            Destroy(gameObject);
        }
    }

    private void LimitSpeed()
    {
        float currentSpeed = Vector3.Magnitude(_rigidbody.velocity);
        if (currentSpeed > Speed / 100)

        {
            float brakeSpeed = currentSpeed - Speed / 100;

            Vector3 normalisedVelocity = _rigidbody.velocity.normalized;
            Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;

            _rigidbody.AddForce(-brakeVelocity);
        }
    }

    private void ApproachTarget()
    {
        var direction = Vector3.zero;
        transform.LookAt(_target);
        _rigidbody.AddRelativeForce(Vector3.forward * Speed * Time.deltaTime, ForceMode.Force);
    }

    private void StopForces()
    {
        _rigidbody.drag = 1000f;
    }

    private bool OutsideStoppingDistanceFromTarget()
    {
        return Vector3.Distance(transform.position, _target.position) > DistanceToStop;
    }
}
