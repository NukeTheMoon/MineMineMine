using UnityEngine;

public class AsteroidBouncer : MonoBehaviour
{
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == TagsReference.ASTEROID)
		{
			PropelTowardsPlayer(other);
		}
	}

	private void PropelTowardsPlayer(Collider other)
	{
		var rigidbody = other.gameObject.GetComponent<Rigidbody>();
		rigidbody.velocity = Vector3.zero;
		var direction = SceneReference.PlayerSpawnManager.GetCentralPlayer().transform.position -
						other.gameObject.transform.position;
		rigidbody.AddForce(direction * SceneReference.AsteroidSpawnManager.AsteroidSpeed);
	}
}
