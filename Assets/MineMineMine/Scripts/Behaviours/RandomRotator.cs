using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class RandomRotator : MonoBehaviour
{
	public float Tumble;
	private Rigidbody _rigidbody;

	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
		SetRandomRotation();
		ApplyRandomSpin();
	}

	private void ApplyRandomSpin()
	{
		_rigidbody.angularVelocity = Random.insideUnitSphere * Tumble;
	}

	private void SetRandomRotation()
	{
		transform.rotation = Random.rotation;
	}
}
