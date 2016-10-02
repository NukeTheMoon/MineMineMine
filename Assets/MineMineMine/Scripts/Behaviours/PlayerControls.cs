using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TeamUtility.IO;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{

    private Text _debugText;

    private Rigidbody _rigidbody;
    private List<ParticleSystem> _particleSystems;
    private List<float> _originalStartSizes;
    private GameObject _meshChild;
    private float _firstThrustTapTimeMs;
    private bool _firstThrustTapped = false;
    private bool _thrustReleased = false;
    private bool _boosted = false;
    private float _particleDiminishIterator;
    private float _currentBank;
    private float _liftImbalanceTolerance = 0.1f;
    private int _constantParticleSystemsCount = 4;

    private void Start()
    {
        GetParticleSystems();
        _rigidbody = GetComponent<Rigidbody>();
        _debugText = GameObject.Find("DebugText").GetComponent<Text>();
        _meshChild = GetComponentInChildren<MeshFilter>().gameObject;
    }

    private void Update()
    {
        if (CheckForShoot())
        {
            SceneReference.MissileSpawnManager.Shoot();
        };
        if (CheckForThrustDoubleTap())
        {
            Boost();
            IntensifyEngineParticles();
        }
        if (CheckForShieldPress())
        {
            ActivateShield();
            _particleSystems.Add(transform.Find(PrefabReference.Shield.name + "(Clone)").GetComponentInChildren<ParticleSystem>());
            _originalStartSizes.Add(_particleSystems.Last().startSize);
        }
        if (CheckForShieldRelease())
        {
            DeactivateShield();
            _particleSystems.Remove(_particleSystems.Last());
            _originalStartSizes.Remove(_originalStartSizes.Last());
        }
        if (!ParticlesAtOriginalIntensity())
        {
            DiminishParticles();
        }
        Bank();
    }

    private bool ParticlesAtOriginalIntensity()
    {
        return _particleDiminishIterator >= 1;
    }

    private void DeactivateShield()
    {
        SceneReference.ShieldManager.StopInvulnerability();
    }

    private bool CheckForShieldRelease()
    {
        if (SceneReference.InputMappingManager.GetShieldRelease() && SceneReference.ShieldManager.Invulnerability)
        {
            return true;
        }
        return false;
    }

    private void ActivateShield()
    {
        SceneReference.ShieldManager.StartInvulnerability();
    }

    private bool CheckForShieldPress()
    {
        if (SceneReference.InputMappingManager.GetShield() && !SceneReference.ShieldManager.Invulnerability)
        {
            return true;
        }
        return false;
    }

    private bool CheckForShoot()
    {
        if (SceneReference.InputMappingManager.GetShoot())
        {
            return true;
        }
        return false;
    }

    private bool CheckForThrustDoubleTap()
    {
        if (!TimeHelper.WithinDoubleTapTimeWindow(_firstThrustTapTimeMs, SceneReference.PlayerMovementManager.DoubleTapTimeWindowMs))
        {
            _firstThrustTapped = false;
            _thrustReleased = false;
        }
        if (SceneReference.InputMappingManager.GetThrust() > 0)
        {
            if (CanBoost())
            {
                _boosted = true;
                return true;
            }
            else
            {
                if (!_boosted)
                {
                    _firstThrustTapTimeMs = TimeHelper.SecondsToMilliseconds(Time.time);
                    _firstThrustTapped = true;
                }
            }
        }
        else
        {
            _boosted = false;
            if (_firstThrustTapped)
            {
                _thrustReleased = true;
            }
        }
        return false;
    }

    private bool CanBoost()
    {
        return _thrustReleased && !_boosted && _firstThrustTapped && SceneReference.MeterManager.HaveEnoughMeter(MeterAction.Boost);
    }

    private void GetParticleSystems()
    {
        _particleSystems = GetComponentsInChildren<ParticleSystem>().ToList();
        _originalStartSizes = _particleSystems.Select(system => system.startSize).ToList();
    }

    private void Boost()
    {
        _rigidbody.AddForce(transform.forward * SceneReference.PlayerMovementManager.BoostForce);
        SceneReference.MeterManager.ExpendMeter(MeterAction.Boost);
    }

    private void CheckForConstantParticleSystems()
    {
        if (_particleSystems.Count < _constantParticleSystemsCount)
        {
            GetParticleSystems();
        }
    }

    private void IntensifyEngineParticles()
    {
        CheckForConstantParticleSystems();
        for (int i = 0; i < _particleSystems.Count; ++i)
        {
            if (_particleSystems[i] != null)
                _particleSystems[i].startSize = _originalStartSizes[i] * SceneReference.PlayerMovementManager.BoostParticleIntensification;
        }
        _particleDiminishIterator = 0;
    }

    private void DiminishParticles()
    {
        CheckForConstantParticleSystems();
        _particleDiminishIterator += Time.deltaTime;
        for (int i = 0; i < _particleSystems.Count; ++i)
        {
            if (_particleSystems[i] != null)
                _particleSystems[i].startSize = Mathf.Lerp(
                _originalStartSizes[i] * SceneReference.PlayerMovementManager.BoostParticleIntensification,
                _originalStartSizes[i],
                _particleDiminishIterator);
        }
    }

    private void FixedUpdate()
    {
        Thrust();
        Turn();
    }

    private void Thrust()
    {
        float thrustForce = SceneReference.InputMappingManager.GetThrust() -
                            SceneReference.InputMappingManager.GetReverse();
        _rigidbody.AddForce(transform.forward * thrustForce * SceneReference.PlayerMovementManager.Speed); // TODO: * fixedDeltaTime !
    }

    private void Turn()
    {
        float turnForce = SceneReference.InputMappingManager.GetTurnRight() -
                          SceneReference.InputMappingManager.GetTurnLeft();
        _rigidbody.AddTorque(Vector3.up * turnForce * SceneReference.PlayerMovementManager.AngularSpeed); // TODO: * fixedDeltaTime !
    }

    private void Bank()
    {
        float liftImbalance = SceneReference.InputMappingManager.GetTurnRight() -
                              SceneReference.InputMappingManager.GetTurnLeft();
        if (Mathf.Abs(liftImbalance) > _liftImbalanceTolerance)
        {
            BankTowardMax(liftImbalance);
        }
        else
        {
            BankTowardLevel();
        }
    }

    private void BankTowardMax(float liftImbalance)
    {
        if (_meshChild.transform.rotation.eulerAngles.z < SceneReference.PlayerMovementManager.MaxTurnBankDeg * Math.Abs(liftImbalance) ||
            _meshChild.transform.rotation.eulerAngles.z > 360 - SceneReference.PlayerMovementManager.MaxTurnBankDeg * Mathf.Abs(liftImbalance))
        {
            _meshChild.transform.Rotate(Vector3.forward, -liftImbalance * SceneReference.PlayerMovementManager.BankDegsInSecond * Time.deltaTime);
        }
    }

    private void BankTowardLevel()
    {
        float zAngleBeforeRotation = _meshChild.transform.rotation.eulerAngles.z;
        float rotationAngle = (_meshChild.transform.rotation.eulerAngles.z > 180 ? 1 : -1) * SceneReference.PlayerMovementManager.BankDegsInSecond *
                                Time.deltaTime;
        _meshChild.transform.Rotate(Vector3.forward, rotationAngle);
        float zAngleAfterRotation = _meshChild.transform.rotation.eulerAngles.z;
        if (OverlappedCircle(rotationAngle, zAngleBeforeRotation, zAngleAfterRotation))
        {
            RotateToZero();
        }
    }

    private static bool OverlappedCircle(float rotationAngle, float angleBeforeRotation, float angleAfterRotation)
    {
        return (Mathf.Sign(rotationAngle) > 0 && angleBeforeRotation - angleAfterRotation > 0) ||
               (Mathf.Sign(rotationAngle) < 0 && angleBeforeRotation - angleAfterRotation < 0);
    }

    private void RotateToZero()
    {
        _meshChild.transform.Rotate(Vector3.forward, -_meshChild.transform.rotation.eulerAngles.z);
    }
}
