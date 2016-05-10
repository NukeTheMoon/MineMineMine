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
        ShotManager.DestroyMissilesFromSameShot(gameObject);
    }
}
