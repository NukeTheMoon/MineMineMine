using UnityEngine;
using System.Collections;

public class ConstantRotation : MonoBehaviour
{

    public float RotationSpeed;

	void Start () {
	
	}
	
	void Update () {
	    transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
	}
}
