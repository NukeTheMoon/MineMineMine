using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{

    public float Speed = 10.0f;
    public float AngularSpeed = 2.0f;
    public float BoostMultiplier = 10.0f;
    public int DoubleTapTimeWindowMs = 300;
    public int BoostForce = 5000;

    private Text _debugText;

    private Rigidbody _rigidbody;
    private float _firstThrustTapTimeMs;
    private bool _firstThrustTapped = false;
    private bool _thrustReleased = false;
    private bool _boosted = false;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _debugText = GameObject.Find("DebugText").GetComponent<Text>();
    }

    private void Update()
    {
        CheckForThrustDoubleTap();
        _debugText.text =
            "W = " + Input.GetKey(KeyCode.W) +
            "\n_firstThrustTapped = " + _firstThrustTapped +
            "\n_thrustReleased = " + _thrustReleased +
            "\n_boosted = " + _boosted +
            "\nTimeWindow = " + TimeHelper.WithinDoubleTapTimeWindow(_firstThrustTapTimeMs, DoubleTapTimeWindowMs);
    }

    private void CheckForThrustDoubleTap()
    {
        if (!TimeHelper.WithinDoubleTapTimeWindow(_firstThrustTapTimeMs, DoubleTapTimeWindowMs))
        {
            _firstThrustTapped = false;
            _thrustReleased = false;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (_thrustReleased && !_boosted && _firstThrustTapped)
            {
                Boost();
                _boosted = true;
                //_firstThrustTapped = false;
                //_thrustReleased = false;
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
    }



    private void Boost()
    {
        Debug.Log("Boost! " + TimeHelper.SecondsToMilliseconds(Time.time));
        _rigidbody.AddForce(transform.forward * BoostForce);
    }


    private void FixedUpdate()
    {
        _rigidbody.AddForce(transform.forward * Input.GetAxis("Vertical") * Speed);
        _rigidbody.AddTorque(transform.up * Input.GetAxis("Horizontal") * AngularSpeed);
    }
}
