using UnityEngine;

public class UpgradeMenuContinueButton : MonoBehaviour
{
    public void OnContinueButtonPressed()
    {
        SceneReference.UIManager.HideUpgradeMenu();
        SceneReference.RespawnManager.ShowReticle();
    }
}
