using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RotateTowardsMovement : MonoBehaviour
{

	private Rigidbody _rigidbody;

	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (_rigidbody.velocity != Vector3.zero)
		{
			transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
		}
	}
}
