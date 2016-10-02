using UnityEngine;
using System.Collections;

public class PlayerMovementManager : MonoBehaviour
{
    public float Speed = 5.0f;
    public float AngularSpeed = 0.35f;
    public float BoostMultiplier = 2.0f;
    public int DoubleTapTimeWindowMs = 100;
    public int BoostForce = 250;
    public int BoostParticleIntensification = 5;
    [Range(0, 90)]
    public float MaxTurnBankDeg = 70;
    public float BankDegsInSecond = 150;

    private void Awake()
    {
        RegisterWithSceneReference();
    }

    private void RegisterWithSceneReference()
    {
        SceneReference.PlayerMovementManager = this;
    }

}
