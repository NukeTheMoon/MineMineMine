using UnityEngine;
using System.Collections;

public class PlayerDestroyer : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == PlayerSpawner.CENTRAL_PLAYER)
        {
            SceneReference.PlayerSpawner.SetLastKnownPlayerPosition(other.gameObject.transform);
            foreach (var player in GameObject.FindGameObjectsWithTag(TagsReference.PLAYER))
            {
                Destroy(player);
            }
            SceneReference.LifeManager.Die();
            SceneReference.WeaponManager.ResetWeapon();
        }
    }

}
