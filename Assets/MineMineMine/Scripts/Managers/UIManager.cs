using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    private void Awake()
    {
        RegisterWithSceneReference();
    }

    private void RegisterWithSceneReference()
    {
        SceneReference.UIManager = this;
    }

    public void DisplayUpgradeMenu()
    {
        UIAnimatorReference.MenuCoverAnimator.SetBool("IsVisible", true);
        UIAnimationEventsReference.MenuCoverAnimationEvents.OnSlideInFinished += OnSlideInFinished_DisplayUpgradeMenu;
    }

    private void OnSlideInFinished_DisplayUpgradeMenu(object sender, System.EventArgs e)
    {
        UIAnimatorReference.UpgradeMenuAnimator.SetBool("IsVisible", true);
        UIAnimationEventsReference.MenuCoverAnimationEvents.OnSlideInFinished -=
            OnSlideInFinished_DisplayUpgradeMenu;
    }

    public void HideUpgradeMenu()
    {
        UIAnimatorReference.MenuCoverAnimator.SetBool("IsVisible", false);
        UIAnimatorReference.UpgradeMenuAnimator.SetBool("IsVisible", false);
    }
}
