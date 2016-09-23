using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FixedRotationManager : MonoBehaviour
{

    private Dictionary<string, Quaternion> _originalRotations;

    private void Start()
    {
        _originalRotations = new Dictionary<string, Quaternion>();
        RegisterWithSceneReference();
    }

    public bool TryAddOriginalRotation(string name, Quaternion originalRotation)
    {
        if (_originalRotations.ContainsKey(name))
        {
            return false;
        }
        _originalRotations.Add(name, originalRotation);
        return true;
    }

    public Quaternion GetOriginalRotation(string name)
    {
        return _originalRotations[name];
    }

    private void RegisterWithSceneReference()
    {
        SceneReference.FixedRotationManager = this;
    }
}
