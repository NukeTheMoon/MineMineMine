using UnityEngine;
using UnityEngine.UI;

namespace Assets.MineMineMine.Scripts.Managers
{
    public class LifeManager : MonoBehaviour
    {

        public int StartingLives = 2;

        public bool PlayerAlive { get; set; }

        public int LivesLeft { get; private set; }

        private void Awake()
        {
            RegisterWithSceneReference();
        }

        private void Start()
        {
            PlayerAlive = true;
            LivesLeft = StartingLives;
        }

        private void RegisterWithSceneReference()
        {
            SceneReference.LifeManager = this;
        }


        public bool NoLivesLeft()
        {
            return LivesLeft < 0;
        }

        public void Die()
        {
            SpawnTeleportBlink();
            if (NoLivesLeft())
            {
                PlayerAlive = false;
                SceneReference.UIManager.DisplayGameOverMenu();
            }
            else
            {
                SceneReference.UIManager.DisplayUpgradeMenu();
            }
        }

        private void SpawnTeleportBlink()
        {
            var blinkSpawnPoint = SceneReference.PlayerSpawnManager.GetCentralPlayer();
            Instantiate(PrefabReference.TeleportBlink, blinkSpawnPoint.transform.position, PrefabReference.TeleportBlink.transform.rotation);
        }


        public void DecreaseLifeCount()
        {
            --LivesLeft;
        }
    }
}
