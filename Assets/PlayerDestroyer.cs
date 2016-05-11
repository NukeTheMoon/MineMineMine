using UnityEngine;
using System.Collections;

public class PlayerDestroyer : MonoBehaviour {

    private LifeManager _lifeManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == TagsReference.PLAYER)
        {
            _lifeManager = GameObject.FindGameObjectWithTag(TagsReference.LOGIC).GetComponent<LifeManager>();
            foreach (var player in GameObject.FindGameObjectsWithTag(TagsReference.PLAYER))
            {
                Destroy(player);
            }
            _lifeManager.LoseLife();
        }
    }

}
