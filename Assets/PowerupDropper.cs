using UnityEngine;
using System.Collections;

public class PowerupDropper : MonoBehaviour {

    private bool _isQuitting;

    void OnApplicationQuit()
    {
        _isQuitting = true;
    }

    void OnDestroy()
    {
        if (!_isQuitting && !SceneReference.PowerupManager.Cooldown)
        {
            var diceRoll = UnityEngine.Random.Range(0.0f, 1.0f);
            if (diceRoll <= SceneReference.PowerupManager.DropChance)
            {
                SceneReference.PowerupManager.InitiateCooldown();
                Instantiate(PrefabReference.Powerup, transform.position, Quaternion.identity);
            }
        }
    }

}
