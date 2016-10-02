using UnityEngine;
using System.Collections;
using UnityEditor.Animations;
using UnityEngine.EventSystems;

public class UpgradeMenuContinueButton : MonoBehaviour, IPointerUpHandler
{
    public void OnPointerUp(PointerEventData eventData)
    {
        SceneReference.UIManager.HideUpgradeMenu();
        SceneReference.RespawnManager.ShowReticle();
    }
}
