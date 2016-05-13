using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerSpawner : MonoBehaviour {

    public Transform PlayerSpawnPoint;
    public float HorizontalOffset; // temporary default to 18.0
    public float VerticalOffset; // temporary default to 10.5

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

    void Awake()
    {
        RegisterWithSceneReference();

    }

	void Start () {

        Spawn();

    }

    private void RegisterWithSceneReference()
    {
        SceneReference.PlayerSpawner = this;
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
        ChangeCentralPlayer((GameObject)Instantiate(PrefabReference.Player, PlayerSpawnPoint.position, PlayerSpawnPoint.rotation));
        _clones = new List<GameObject>();
        CreateClones();
    }

    private void CreateClones()
    {

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
        for (var i=0; i<_clones.Count; ++i)
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
        for (var i=0; i<_clones.Count; ++i)
        {
            if (_clones[i].GetInstanceID() != protectedClone.GetInstanceID())
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
        return _centralPlayer;
    }

    

}
