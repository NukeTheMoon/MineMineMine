using UnityEngine;
using System.Collections;

public class CenterPropeller : MonoBehaviour
{

    public float Force;

	private void Start ()
	{
	    PropelTowardsCenter();
	}

    private void PropelTowardsCenter()
    {
        var direction = SceneReference.PlayerSpawner.GetCentralPlayer().transform.position - transform.position;
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(direction * Force);
        rigidbody.drag = 0;
    }
    
}
