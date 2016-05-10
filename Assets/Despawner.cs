using UnityEngine;

public class Despawner : MonoBehaviour {

    public PlayerSpawner PlayerSpawner;
    public AsteroidSpawner AsteroidSpawner;

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == TagsReference.MISSILE)
        {
            ShotManager.ProtectedMissileIds.Add(other.gameObject.GetInstanceID());
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == TagsReference.PLAYER)
        {
            PlayerSpawner.RegenerateClonesAroundOppositeOfExiting(other.gameObject.GetInstanceID());
        }
    }
}
