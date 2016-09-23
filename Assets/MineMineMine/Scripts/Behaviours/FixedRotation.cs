using UnityEngine;
using System.Collections;
using ReptilianCabal.MineMineMine;

public class FixedRotation : MonoBehaviour
{
    private Quaternion _originalRotation;
    private string _path;

    private void Start()
    {
        _path = gameObject.GetPath();
        SceneReference.FixedRotationManager.TryAddOriginalRotation(_path, transform.rotation);
        _originalRotation = SceneReference.FixedRotationManager.GetOriginalRotation(_path);
    }

    private void LateUpdate()
    {
        transform.rotation = _originalRotation;
    }
}
