using UnityEngine;
using System.Collections;
using Assets.MineMineMine.Scripts.Managers;

public class SceneReference : MonoBehaviour
{

    public static PlayerSpawnManager PlayerSpawnManager
    {
        get { return _playerSpawnManager; }
        set
        {
            if (_playerSpawnManager == null) _playerSpawnManager = value;
        }
    }

    public static MissileSpawnManager MissileSpawnManager
    {
        get { return _missileSpawnManager; }
        set
        {
            if (_missileSpawnManager == null) _missileSpawnManager = value;
        }
    }

    public static AsteroidSpawnManager AsteroidSpawnManager
    {
        get { return _asteroidSpawnManager; }
        set
        {
            if (_asteroidSpawnManager == null) _asteroidSpawnManager = value;
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

    public static ScorekeepingManager ScorekeepingManager
    {
        get { return _scorekeepingManager; }
        set
        {
            if (_scorekeepingManager == null) _scorekeepingManager = value;
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

    public static MeterManager MeterManager
    {
        get { return _meterManager; }
        set
        {
            if (_meterManager == null) _meterManager = value;
        }
    }

    public static ShieldManager ShieldManager
    {
        get { return _shieldManager; }
        set
        {
            if (_shieldManager == null)
                _shieldManager = value;
        }
    }

    public static InputMappingManager InputMappingManager
    {
        get { return _inputMappingManager; }
        set
        {
            if (_inputMappingManager == null)
                _inputMappingManager = value;
        }
    }

    public static FixedRotationManager FixedRotationManager
    {
        get
        {
            return _fixedRotationManager;

        }
        set
        {
            if (_fixedRotationManager == null)
                _fixedRotationManager = value;
        }
    }

    public static UpgradeMenuManager UpgradeMenuManager
    {
        get
        {
            return _upgradeMenuManager;

        }
        set
        {
            if (_upgradeMenuManager == null)
                _upgradeMenuManager = value;
        }
    }

    public static PlayerMovementManager PlayerMovementManager
    {
        get
        {
            return _playerMovementManager;

        }
        set
        {
            if (_playerMovementManager == null)
                _playerMovementManager = value;
        }
    }

    public static UIManager UIManager
    {
        get
        {
            return _uiManager;

        }
        set
        {
            if (_uiManager == null)
                _uiManager = value;
        }
    }

    private static PlayerSpawnManager _playerSpawnManager;
    private static MissileSpawnManager _missileSpawnManager;
    private static AsteroidSpawnManager _asteroidSpawnManager;
    private static LifeManager _lifeManager;
    private static ScorekeepingManager _scorekeepingManager;
    private static ShotManager _shotManager;
    private static AsteroidDivisionManager _asteroidDivisionManager;
    private static PowerupManager _powerupManager;
    private static WeaponManager _weaponManager;
    private static RespawnManager _respawnManager;
    private static MeterManager _meterManager;
    private static ShieldManager _shieldManager;
    private static InputMappingManager _inputMappingManager;
    private static FixedRotationManager _fixedRotationManager;
    private static UpgradeMenuManager _upgradeMenuManager;
    private static PlayerMovementManager _playerMovementManager;
    private static UIManager _uiManager;
}
