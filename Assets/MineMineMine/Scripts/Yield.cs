﻿using UnityEngine;
using System.Collections;
using System;
using PigeonCoopToolkit.Effects.Trails;

public class Yield : MonoBehaviour {

    public float Speed;
    public float DistanceToStop;

    private static Transform _target;
    private Rigidbody _rigidbody;
    private Trail _trail;
    private ParticleSystem _explosion;
    private bool _approaching;

    private void Start()
    {
        UpdateTarget();
        _rigidbody = GetComponent<Rigidbody>();
        _trail = GetComponent<Trail>();
        _explosion = transform.GetChild(1).GetComponent<ParticleSystem>();
        _approaching = true;
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
        SceneReference.ScoreKeeper.AsteroidHit();
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

            Vector3 normalisedVelocity = _rigidbody.velocity.normalized;
            Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;

            _rigidbody.AddForce(-brakeVelocity);
        }
    }

    private void ApproachTarget()
    {
        transform.LookAt(_target);
        _rigidbody.AddRelativeForce(Vector3.forward * Speed * Time.deltaTime, ForceMode.Force);
    }
   
}