using UnityEngine;
using System.Collections;

public class UIAnimatorReference : MonoBehaviour
{
    [SerializeField]
    private Animator _menuCoverAnimator;
    [SerializeField]
    private Animator _upgradeMenuAnimator;
    [SerializeField]
    private Animator _gameOverMenuAnimator;
    [SerializeField]
    private Animator _messageAnimator;

    public static Animator MenuCoverAnimator;
    public static Animator UpgradeMenuAnimator;
    public static Animator GameOverMenuAnimator;
    public static Animator MessageAnimator;

    private void Awake()
    {
        MenuCoverAnimator = _menuCoverAnimator;
        UpgradeMenuAnimator = _upgradeMenuAnimator;
        GameOverMenuAnimator = _gameOverMenuAnimator;
        MessageAnimator = _messageAnimator;
    }
}
