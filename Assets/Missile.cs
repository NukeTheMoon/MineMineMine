using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{
    void OnDestroy()
    {
        SceneReference.ShotManager.DestroyMissilesFromSameShot(gameObject);
    }
}
