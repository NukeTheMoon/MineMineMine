using System;
using UnityEngine;
using System.Collections;

public class UpgradeMenuAnimationEvents : MonoBehaviour
{
    public event EventHandler OnDisappearFinished;

    private void TriggerOnDisappearFinished()
    {
        if (OnDisappearFinished != null) OnDisappearFinished.Invoke(this, null);
    }
}
