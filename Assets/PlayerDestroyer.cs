using UnityEngine;
using System.Collections;

public class PlayerDestroyer : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == PlayerSpawner.CENTRAL_PLAYER)
        {
            foreach (var player in GameObject.FindGameObjectsWithTag(TagsReference.PLAYER))
            {
                Destroy(player);
            }
            SceneReference.LifeManager.LoseLife();
            SceneReference.WeaponManager.ResetWeapon();
        }
    }

}
