using UnityEngine;
using System.Collections;

public class SceneReference : MonoBehaviour {

    public static PlayerSpawner PlayerSpawner
    {
        get { return _playerSpawner; }
        set
        {
            if (_playerSpawner == null) _playerSpawner = value;
        }
    }

    public static MissileSpawner MissileSpawner
    {
        get { return _missileSpawner; }
        set
        {
            if (_missileSpawner == null) _missileSpawner = value;
        }
    }

    public static AsteroidSpawner AsteroidSpawner
    {
        get { return _asteroidSpawner; }
        set
        {
            if (_asteroidSpawner == null) _asteroidSpawner = value;
        }
    }

    public static LifeManager LifeManager
    {
        get { return _lifeManager; }
        set
        {
            if (_lifeManager == null) _lifeManager = value;
        }
    }

    public static ScoreKeeper ScoreKeeper
    {
        get { return _scoreKeeper; }
        set
        {
            if (_scoreKeeper == null) _scoreKeeper = value;
        }
    }

    public static ShotManager ShotManager
    {
        get { return _shotManager; }
        set
        {
            if (_shotManager == null) _shotManager = value;
        }
    }

    public static AsteroidDivisionManager AsteroidDivisionManager
    {
        get { return _asteroidDivisionManager; }
        set
        {
            if (_asteroidDivisionManager == null) _asteroidDivisionManager = value;
        }
    }

    public static PowerupManager PowerupManager
    {
        get { return _powerupManager; }
        set
        {
            if (_powerupManager == null) _powerupManager = value;
        }
    }

    public static WeaponManager WeaponManager
    {
        get { return _weaponManager; }
        set
        {
            if (_weaponManager == null) _weaponManager = value;
        }
    }

    public static RespawnManager RespawnManager
    {
        get { return _respawnManager; }
        set
        {
            if (_respawnManager == null) _respawnManager = value;
        }
    }

    private static PlayerSpawner _playerSpawner;
    private static MissileSpawner _missileSpawner;
    private static AsteroidSpawner _asteroidSpawner;
    private static LifeManager _lifeManager;
    private static ScoreKeeper _scoreKeeper;
    private static ShotManager _shotManager;
    private static AsteroidDivisionManager _asteroidDivisionManager;
    private static PowerupManager _powerupManager;
    private static WeaponManager _weaponManager;
    private static RespawnManager _respawnManager;
}
