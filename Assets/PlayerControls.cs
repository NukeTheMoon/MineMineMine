using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour
{

    public float Speed = 10.0f;
    public float AngularSpeed = 2.0f;
    private Rigidbody _rigidbody;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        _rigidbody.AddForce(transform.forward * Input.GetAxis("Vertical") * Speed);
        _rigidbody.AddTorque(transform.up * Input.GetAxis("Horizontal") * AngularSpeed);
    }
}
