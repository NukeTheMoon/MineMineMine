using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.MineMineMine.Scripts.Managers
{
    public class PlayerSpawnManager : MonoBehaviour
    {

        public Transform PlayerSpawnPoint;
        public float HorizontalOffset = 18; // temporary default to 18.0
        public float VerticalOffset = 10.5f; // temporary default to 10.5

        public const string CENTRAL_PLAYER = "CentralPlayer";
        public const string NORTH_PLAYER_LOOP_CLONE = "NorthPlayerLoopClone";
        public const string EAST_PLAYER_LOOP_CLONE = "EastPlayerLoopClone";
        public const string SOUTH_PLAYER_LOOP_CLONE = "SouthPlayerLoopClone";
        public const string WEST_PLAYER_LOOP_CLONE = "WestPlayerLoopClone";

        public event EventHandler OnCentralPlayerChanged = delegate { };

        private GameObject _centralPlayer;
        private GameObject _northClone;
        private GameObject _eastClone;
        private GameObject _southClone;
        private GameObject _westClone;
        private List<GameObject> _clones;
        private GameObject _lastKnownPosition;

        private void Awake()
        {
            RegisterWithSceneReference();

        }

        private void Start()
        {
            Spawn();
        }

        private void SpawnTeleportBlink(Transform transform)
        {
            GameObject teleportBlink = (GameObject)Instantiate(PrefabReference.TeleportBlink, transform.position, transform.rotation);
            teleportBlink.transform.Translate(0, 1, 0); // move particle effect above player
        }

        private void RegisterWithSceneReference()
        {
            SceneReference.PlayerSpawnManager = this;
        }

        private void ChangeCentralPlayer(GameObject newCentralPlayer)
        {
            _centralPlayer = newCentralPlayer;
            _centralPlayer.name = CENTRAL_PLAYER;
            if (OnCentralPlayerChanged != null)
            {
                OnCentralPlayerChanged(this, null);
            }
        }

        public void Spawn()
        {
            SpawnTeleportBlink(PlayerSpawnPoint);
            ChangeCentralPlayer((GameObject)Instantiate(PrefabReference.Player, PlayerSpawnPoint.position, PlayerSpawnPoint.rotation));
            CreateClones();
        }

        public void Spawn(Transform transform)
        {
            SpawnTeleportBlink(transform);
            ChangeCentralPlayer((GameObject)Instantiate(PrefabReference.Player, new Vector3(transform.position.x, PlayerSpawnPoint.position.y, transform.position.z), transform.rotation));
            CreateClones();
        }

        private void CreateClones()
        {
            _clones = new List<GameObject>();

            var northClonePosition = new Vector3(_centralPlayer.transform.position.x, _centralPlayer.transform.position.y, _centralPlayer.transform.position.z + VerticalOffset);
            var eastClonePosition = new Vector3(_centralPlayer.transform.position.x + HorizontalOffset, _centralPlayer.transform.position.y, _centralPlayer.transform.position.z);
            var southClonePosition = new Vector3(_centralPlayer.transform.position.x, _centralPlayer.transform.position.y, _centralPlayer.transform.position.z - VerticalOffset);
            var westClonePosition = new Vector3(_centralPlayer.transform.position.x - HorizontalOffset, _centralPlayer.transform.position.y, _centralPlayer.transform.position.z);


            _clones.Add(_northClone = (GameObject)Instantiate(PrefabReference.Player, northClonePosition, _centralPlayer.transform.rotation));
            _clones.Add(_eastClone = (GameObject)Instantiate(PrefabReference.Player, eastClonePosition, _centralPlayer.transform.rotation));
            _clones.Add(_southClone = (GameObject)Instantiate(PrefabReference.Player, southClonePosition, _centralPlayer.transform.rotation));
            _clones.Add(_westClone = (GameObject)Instantiate(PrefabReference.Player, westClonePosition, _centralPlayer.transform.rotation));

            _northClone.name = NORTH_PLAYER_LOOP_CLONE;
            _eastClone.name = EAST_PLAYER_LOOP_CLONE;
            _southClone.name = SOUTH_PLAYER_LOOP_CLONE;
            _westClone.name = WEST_PLAYER_LOOP_CLONE;

            MatchCloneVelocityToCentralPlayer();

        }

        private void MatchCloneVelocityToCentralPlayer()
        {
            for (var i = 0; i < _clones.Count; ++i)
            {
                _clones[i].GetComponent<Rigidbody>().velocity = _centralPlayer.GetComponent<Rigidbody>().velocity;
                _clones[i].GetComponent<Rigidbody>().angularVelocity = _centralPlayer.GetComponent<Rigidbody>().angularVelocity;
            }
        }

        public void RegenerateClonesAroundOppositeOfExiting(int exitId)
        {

            var newCentralPlayer = GetOpposingClone(exitId);
            DestroyClonesOtherThan(newCentralPlayer);
            Destroy(_centralPlayer);
            ChangeCentralPlayer(newCentralPlayer);
            CreateClones();
        }

        private void DestroyClonesOtherThan(GameObject protectedClone)
        {
            for (var i = 0; i < _clones.Count; ++i)
            {
                if (_clones[i].GetInstanceID() != protectedClone.GetInstanceID() && _clones[i] != null)
                {
                    Destroy(_clones[i]);
                }
            }
            _clones = new List<GameObject>();
        }

        public GameObject GetOpposingClone(int instanceId)
        {
            var northid = _northClone.GetInstanceID();
            var eastid = _eastClone.GetInstanceID();
            var southid = _southClone.GetInstanceID();
            var westid = _westClone.GetInstanceID();

            if (instanceId == _northClone.GetInstanceID())
            {
                return _southClone;
            }
            if (instanceId == _eastClone.GetInstanceID())
            {
                return _westClone;
            }
            if (instanceId == _southClone.GetInstanceID())
            {
                return _northClone;
            }
            if (instanceId == _westClone.GetInstanceID())
            {
                return _eastClone;
            }

            Debug.LogError("Could not retrieve opposing player clone.");
            return null;
        }

        public GameObject GetCentralPlayer()
        {
            if (_centralPlayer == null || _centralPlayer.Equals(null) || SceneReference.LifeManager.NoLivesLeft())
            {
                if (_lastKnownPosition == null)
                {
                    Debug.LogError("Trying to access last known position when it is not set!");
                }
                return _lastKnownPosition;
            }
            return _centralPlayer;
        }

        public List<GameObject> GetAllPlayers()
        {
            var players = new List<GameObject>();
            players.Add(_centralPlayer);
            players.AddRange(_clones);
            return players;
        }

        private void ClearLastKnownPosition()
        {
            if (_lastKnownPosition != null)
            {
                Destroy(_lastKnownPosition);
            }
        }


        public void SetLastKnownPlayerPosition(Transform lastKnownTransform)
        {
            ClearLastKnownPosition();
            _lastKnownPosition = (GameObject)Instantiate(PrefabReference.LastKnownPosition, lastKnownTransform.position, lastKnownTransform.rotation);
        }
    }
}
