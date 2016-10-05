using UnityEngine;
using System.Collections;

public class UIGroupReference : MonoBehaviour
{

    [SerializeField]
    private GameObject _upgradeMenu;
    [SerializeField]
    private GameObject _gameOverMenu;
    [SerializeField]
    private GameObject _message;

    public static GameObject UpgradeMenu;
    public static GameObject GameOverMenu;
    public static GameObject Message;

    private void Awake()
    {
        UpgradeMenu = _upgradeMenu;
        GameOverMenu = _gameOverMenu;
        Message = _message;
    }
}
