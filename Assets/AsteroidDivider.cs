using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AsteroidDivider : MonoBehaviour {

    public GameObject AsteroidPrefab;
    public int Divisor = 2;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == TagsReference.MISSILE)
        {
            for (var i=0; i<Divisor; ++i)
            {
                //var smallerAsteroid = (GameObject) Instantiate(AsteroidPrefab, transform.position, Quaternion.identity); // why (clone)(clone)??
                //smallerAsteroid.GetComponent<AsteroidDivider>().Shrink(smallerAsteroid.transform.localScale);

            }
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }

    public void Shrink(Vector3 previousScale)
    {
        transform.localScale = previousScale / 2;

    }
}
