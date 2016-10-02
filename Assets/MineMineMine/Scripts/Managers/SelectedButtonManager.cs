using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SelectedButtonManager : MonoBehaviour
{
    public EventSystem EventSystem;
    public GameObject UpgradeMenuDefaultSelect;
    public GameObject GameOverMenuDefaultSelect;

    private void Awake()
    {
        RegisterWithSceneReference();
    }

    private void RegisterWithSceneReference()
    {
        SceneReference.SelectedButtonManager = this;
    }

    public void SelectButton(GameObject buttonGameObject)
    {
        EventSystem.current.SetSelectedGameObject(buttonGameObject);
    }

}
