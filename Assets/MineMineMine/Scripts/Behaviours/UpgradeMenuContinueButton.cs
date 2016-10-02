using UnityEngine;
using System.Collections;
using UnityEditor.Animations;
using UnityEngine.EventSystems;

public class UpgradeMenuContinueButton : MonoBehaviour
{
    public void OnContinueButtonPressed()
    {
        SceneReference.UIManager.HideUpgradeMenu();
        SceneReference.RespawnManager.ShowReticle();
    }
}
