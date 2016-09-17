using UnityEngine;
using System.Collections;
using Assets.MineMineMine.Scripts.Managers;

public class PlayerDestroyer : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == PlayerSpawnManager.CENTRAL_PLAYER && !SceneReference.ShieldManager.Invulnerability)
        {
            SceneReference.PlayerSpawnManager.SetLastKnownPlayerPosition(other.gameObject.transform);
            foreach (GameObject player in GameObject.FindGameObjectsWithTag(TagsReference.PLAYER))
            {
                Destroy(player);
            }
            SceneReference.LifeManager.Die();
            SceneReference.WeaponManager.ResetWeapon();
        }
    }

}
