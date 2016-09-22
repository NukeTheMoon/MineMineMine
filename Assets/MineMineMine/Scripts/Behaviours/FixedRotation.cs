using UnityEngine;
using System.Collections;

public class FixedRotation : MonoBehaviour
{

    private Quaternion _originalRotation;

    private void Start()
    {
        _originalRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        transform.rotation = _originalRotation;
    }
}
