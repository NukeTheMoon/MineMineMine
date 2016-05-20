using UnityEngine;
using System.Collections;

public class AbsoluteDestroyer : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            Destroy(other.gameObject);
        }
    }

}
