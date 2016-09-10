using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour
{

    public float Speed = 10.0f;
    public float AngularSpeed = 2.0f;
    public float Boost = 1.0f;
    public bool RecentlyPressedThrust = false;
    private Rigidbody _rigidbody;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(transform.forward * Input.GetAxis("Vertical") * Speed * Boost);
        _rigidbody.AddTorque(transform.up * Input.GetAxis("Horizontal") * AngularSpeed);
    }
}
