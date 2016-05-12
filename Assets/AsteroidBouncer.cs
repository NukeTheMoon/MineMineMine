using UnityEngine;
using System.Collections;

public class AsteroidBouncer : MonoBehaviour {

    public PlayerSpawner PlayerSpawner;
    public AsteroidSpawner AsteroidSpawner;

    void OnTriggerExit(Collider other)
    {
        if (LifeManager.PlayerAlive)
        {
            if (other.gameObject.tag == TagsReference.ASTEROID)
            {
                var rigidbody = other.gameObject.GetComponent<Rigidbody>();
                rigidbody.velocity = Vector3.zero;
                var direction = PlayerSpawner.GetCentralPlayer().transform.position - other.gameObject.transform.position;
                rigidbody.AddForce(direction * AsteroidSpawner.AsteroidSpeed);
            }
        }
    }
}
