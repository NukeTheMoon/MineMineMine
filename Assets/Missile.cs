using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{

    void Start()
    {

    }
	
	void Update ()
	{

	}

    void OnDestroy()
    {
        SceneReference.ShotManager.DestroyMissilesFromSameShot(gameObject);
    }
}
