using UnityEngine;

public class Despawner : MonoBehaviour {

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == TagsReference.MISSILE)
        {
            SceneReference.ShotManager.ProtectedMissileIds.Add(other.gameObject.GetInstanceID());
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == TagsReference.PLAYER)
        {
            SceneReference.PlayerSpawner.RegenerateClonesAroundOppositeOfExiting(other.gameObject.GetInstanceID());
        }
    }
}
