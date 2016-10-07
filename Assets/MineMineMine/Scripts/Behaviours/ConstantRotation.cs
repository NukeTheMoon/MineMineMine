using UnityEngine;

public class ConstantRotation : MonoBehaviour
{

    public float RotationSpeed;

    private void Update()
    {
        transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
    }
}
