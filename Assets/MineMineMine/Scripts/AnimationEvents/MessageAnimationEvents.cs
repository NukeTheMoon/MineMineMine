using System;
using UnityEngine;
using System.Collections;

public class MessageAnimationEvents : MonoBehaviour
{
    public event EventHandler OnCycleFinished;

    private void CycleFinished()
    {
        if (OnCycleFinished != null) OnCycleFinished.Invoke(this, null);
    }
}
