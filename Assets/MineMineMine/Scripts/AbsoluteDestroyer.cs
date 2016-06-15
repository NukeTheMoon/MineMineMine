using UnityEngine;
using System.Collections;

public class AbsoluteDestroyer : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("ABSOLUTE DESTRUCTION: " + other.gameObject.name);
        Destroy(other.gameObject);
        
    }

}
