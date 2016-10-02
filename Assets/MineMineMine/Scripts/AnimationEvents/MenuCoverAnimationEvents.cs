using System;
using UnityEngine;

public class MenuCoverAnimationEvents : MonoBehaviour
{
    public event EventHandler OnSlideInFinished;

    private void TriggerOnSlideInFinished()
    {
        if (OnSlideInFinished != null) OnSlideInFinished.Invoke(this, null);
    }
}
