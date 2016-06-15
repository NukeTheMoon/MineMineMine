using UnityEngine;
using System.Collections;

public class AsteroidBouncer : MonoBehaviour {
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == TagsReference.ASTEROID)
        {
            var rigidbody = other.gameObject.GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
            var direction = SceneReference.PlayerSpawner.GetCentralPlayer().transform.position - other.gameObject.transform.position;
            rigidbody.AddForce(direction * SceneReference.AsteroidSpawner.AsteroidSpeed);
        }
    }
}
