using UnityEngine;
using System.Collections;

public class SceneReference : MonoBehaviour {

    public static PlayerSpawner PlayerSpawner { get; set; }
    public static MissileSpawner MissileSpawner { get; set; }
    public static AsteroidSpawner AsteroidSpawner { get; set; }
    public static LifeManager LifeManager { get; set; }
    public static ScoreKeeper ScoreKeeper { get; set; }
    public static ShotManager ShotManager { get; set; }
    public static AsteroidDivisionManager AsteroidDivisionManager { get; set; }
    public static PowerupManager PowerupManager { get; set; }
    public static WeaponManager WeaponManager { get; set; }

}
