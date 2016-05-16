using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{
    private void OnDestroy()
    {
        SceneReference.ShotManager.DestroyMissilesFromSameShot(gameObject);
    }
}
