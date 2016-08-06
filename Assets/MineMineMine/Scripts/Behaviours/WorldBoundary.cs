using UnityEngine;

public class WorldBoundary : MonoBehaviour
{
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == TagsReference.MISSILE)
		{
			SceneReference.ShotManager.ProtectedMissileIds.Add(other.gameObject.GetInstanceID());
			Destroy(other.gameObject);
		}

		if (other.gameObject.tag == TagsReference.PLAYER)
		{
			SceneReference.PlayerSpawnManager.RegenerateClonesAroundOppositeOfExiting(other.gameObject.GetInstanceID());
		}
	}
}
