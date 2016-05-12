using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerSpawner : MonoBehaviour {

    public Transform PlayerSpawnPoint;
    public GameObject PlayerPrefab;
    public float HorizontalOffset; // default to 18.0
    public float VerticalOffset; // default to 10.5

    public const string CENTRAL_PLAYER = "CentralPlayer";
    public const string NORTH_PLAYER_LOOP_CLONE = "NorthPlayerLoopClone";
    public const string EAST_PLAYER_LOOP_CLONE = "EastPlayerLoopClone";
    public const string SOUTH_PLAYER_LOOP_CLONE = "SouthPlayerLoopClone";
    public const string WEST_PLAYER_LOOP_CLONE = "WestPlayerLoopClone";

    private GameObject _centralPlayer;
    private GameObject _northClone;
    private GameObject _eastClone;
    private GameObject _southClone;
    private GameObject _westClone;
    private List<GameObject> _clones;

	void Start () {

        Spawn();

    }

    public void Spawn()
    {
        _centralPlayer = (GameObject)Instantiate(PlayerPrefab, PlayerSpawnPoint.position, PlayerSpawnPoint.rotation);
        _centralPlayer.name = CENTRAL_PLAYER;
        _clones = new List<GameObject>();
        CreateClones();
    }

    private void CreateClones()
    {

        var northClonePosition = new Vector3(_centralPlayer.transform.position.x, _centralPlayer.transform.position.y, _centralPlayer.transform.position.z + VerticalOffset);
        var eastClonePosition = new Vector3(_centralPlayer.transform.position.x + HorizontalOffset, _centralPlayer.transform.position.y, _centralPlayer.transform.position.z);
        var southClonePosition = new Vector3(_centralPlayer.transform.position.x, _centralPlayer.transform.position.y, _centralPlayer.transform.position.z - VerticalOffset);
        var westClonePosition = new Vector3(_centralPlayer.transform.position.x - HorizontalOffset, _centralPlayer.transform.position.y, _centralPlayer.transform.position.z);


        _clones.Add(_northClone = (GameObject)Instantiate(PlayerPrefab, northClonePosition, _centralPlayer.transform.rotation));
        _clones.Add(_eastClone = (GameObject)Instantiate(PlayerPrefab, eastClonePosition, _centralPlayer.transform.rotation));
        _clones.Add(_southClone = (GameObject)Instantiate(PlayerPrefab, southClonePosition, _centralPlayer.transform.rotation));
        _clones.Add(_westClone = (GameObject)Instantiate(PlayerPrefab, westClonePosition, _centralPlayer.transform.rotation));

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
        _centralPlayer = newCentralPlayer;
        _centralPlayer.name = CENTRAL_PLAYER;
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

    void Update () {
	
	}
}
