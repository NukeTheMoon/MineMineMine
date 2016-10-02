using UnityEngine;
using System.Collections;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI Message;

    private void Awake()
    {
        RegisterWithSceneReference();
    }

    private void Start()
    {
        UIGroupReference.UpgradeMenu.SetActive(false);
        UIGroupReference.GameOverMenu.SetActive(false);
        UIGroupReference.Message.SetActive(false);
    }

    private void RegisterWithSceneReference()
    {
        SceneReference.UIManager = this;
    }

    public void DisplayUpgradeMenu()
    {
        UIGroupReference.UpgradeMenu.SetActive(true);
        UIAnimatorReference.MenuCoverAnimator.SetBool("IsVisible", true);
        UIAnimationEventsReference.MenuCoverAnimationEvents.OnSlideInFinished += OnSlideInFinished_DisplayUpgradeMenu;
    }

    private void OnSlideInFinished_DisplayUpgradeMenu(object sender, System.EventArgs e)
    {
        UIAnimatorReference.UpgradeMenuAnimator.SetBool("IsVisible", true);
        UIAnimationEventsReference.MenuCoverAnimationEvents.OnSlideInFinished -=
            OnSlideInFinished_DisplayUpgradeMenu;
        SceneReference.SelectedButtonManager.SelectButton(SceneReference.SelectedButtonManager.UpgradeMenuDefaultSelect);
    }

    public void HideUpgradeMenu()
    {
        UIAnimatorReference.MenuCoverAnimator.SetBool("IsVisible", false);
        UIAnimatorReference.UpgradeMenuAnimator.SetBool("IsVisible", false);
        UIAnimationEventsReference.UpgradeMenuAnimationEvents.OnDisappearFinished += OnDisappearFinished_DisableUpgradeMenu;
    }

    private void OnDisappearFinished_DisableUpgradeMenu(object sender, System.EventArgs e)
    {
        UIAnimationEventsReference.UpgradeMenuAnimationEvents.OnDisappearFinished -=
            OnDisappearFinished_DisableUpgradeMenu;
        UIGroupReference.UpgradeMenu.SetActive(false);
    }

    public void DisplayGameOverMenu()
    {
        UIAnimatorReference.MenuCoverAnimator.SetBool("IsVisible", true);
        UIAnimationEventsReference.MenuCoverAnimationEvents.OnSlideInFinished += OnSlideInFinished_DisplayGameOverMenu;

    }

    private void OnSlideInFinished_DisplayGameOverMenu(object sender, System.EventArgs e)
    {
        UIAnimationEventsReference.MenuCoverAnimationEvents.OnSlideInFinished -= OnSlideInFinished_DisplayGameOverMenu;
        UIGroupReference.GameOverMenu.SetActive(true);
        UIAnimatorReference.GameOverMenuAnimator.SetBool("IsVisible", true);
        SceneReference.SelectedButtonManager.SelectButton(SceneReference.SelectedButtonManager.GameOverMenuDefaultSelect);
    }

    public void DisplayMessage(string text)
    {
        UIGroupReference.Message.SetActive(true);
        Message.text = text;
        UIAnimatorReference.MessageAnimator.SetTrigger("Cycle");
        UIAnimationEventsReference.MessageAnimationEvents.OnCycleFinished += OnCycleFinished_DisableMessage;
    }

    private void OnCycleFinished_DisableMessage(object sender, System.EventArgs e)
    {
        UIAnimationEventsReference.MessageAnimationEvents.OnCycleFinished -= OnCycleFinished_DisableMessage;
        UIGroupReference.Message.SetActive(false);
    }
}
