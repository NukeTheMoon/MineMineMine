using UnityEngine;

public class UIAnimationEventsReference : MonoBehaviour
{
    [SerializeField]
    private MenuCoverAnimationEvents _menuCoverAnimationEvents;
    [SerializeField]
    private UpgradeMenuAnimationEvents _upgradeMenuAnimationEvents;
    [SerializeField]
    private MessageAnimationEvents _messageAnimationEvents;

    public static MenuCoverAnimationEvents MenuCoverAnimationEvents;
    public static UpgradeMenuAnimationEvents UpgradeMenuAnimationEvents;
    public static MessageAnimationEvents MessageAnimationEvents;

    private void Start()
    {
        MenuCoverAnimationEvents = _menuCoverAnimationEvents;
        UpgradeMenuAnimationEvents = _upgradeMenuAnimationEvents;
        MessageAnimationEvents = _messageAnimationEvents;
    }

}
