using UnityEngine;
using System.Collections;

public class PowerupDropper : MonoBehaviour {

    private bool _isQuitting;

    private void OnApplicationQuit()
    {
        _isQuitting = true;
    }

    private void OnDestroy()
    {
        if (!_isQuitting && !SceneReference.PowerupManager.Cooldown)
        {
            var diceRoll = UnityEngine.Random.Range(0.0f, 1.0f);
            if (SceneReference.ScoreKeeper.Score == 0 || diceRoll <= SceneReference.PowerupManager.DropChance)
            {
                SceneReference.PowerupManager.InitiateCooldown();
                Instantiate(PrefabReference.Powerup, transform.position, Quaternion.identity);
            }
        }
    }

}
