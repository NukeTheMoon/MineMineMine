using UnityEngine;
using System.Collections;

public class UIAnimatorReference : MonoBehaviour
{
    [SerializeField]
    private Animator _menuCoverAnimator;
    [SerializeField]
    private Animator _upgradeMenuAnimator;

    public static Animator MenuCoverAnimator;
    public static Animator UpgradeMenuAnimator;

    private void Start()
    {
        MenuCoverAnimator = _menuCoverAnimator;
        UpgradeMenuAnimator = _upgradeMenuAnimator;
    }
}
