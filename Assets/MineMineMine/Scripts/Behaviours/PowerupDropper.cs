using UnityEngine;

public class PowerupDropper : MonoBehaviour
{

    private bool _isQuitting;

    private void OnApplicationQuit()
    {
        _isQuitting = true;
    }

    private void OnDestroy()
    {
        if (!_isQuitting && !SceneReference.PowerupManager.Cooldown)
        {
            AttemptDropPowerup();
        }
    }

    private void AttemptDropPowerup()
    {
        var diceRoll = UnityEngine.Random.Range(0.0f, 1.0f);
        if (DropCriteriaSatisfied(diceRoll))
        {
            SceneReference.PowerupManager.InitiateCooldown();
            Instantiate(PrefabReference.Powerup, transform.position, Quaternion.identity);
        }
    }

    private static bool DropCriteriaSatisfied(float diceRoll)
    {
        return (SceneReference.ScorekeepingManager.WaveScore == 0 || diceRoll <= SceneReference.PowerupManager.DropChance) && SceneReference.PowerupManager.PowerupsEnabled;
    }
}
