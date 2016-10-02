using UnityEngine;
using System.Collections;

public class UIAnimationEventsReference : MonoBehaviour
{
    [SerializeField]
    private MenuCoverAnimationEvents _menuCoverAnimationEvents;

    public static MenuCoverAnimationEvents MenuCoverAnimationEvents;

    private void Start()
    {
        MenuCoverAnimationEvents = _menuCoverAnimationEvents;
    }

}
