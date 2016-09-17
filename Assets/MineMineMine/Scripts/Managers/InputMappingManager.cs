using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using TeamUtility.IO;

public class InputMappingManager : MonoBehaviour
{

    public InputScheme CurrentScheme { get; private set; }

    private float _rightTriggerDeadzone;
    private float _leftTriggerDeadzone;
    private float _rightStickHorizontalDeadzone;
    private float _rightStickVerticalDeadzone;
    private float _leftStickHorizontalDeadzone;
    private float _leftStickVerticalDeadzone;
    private float _dpadHorizontalDeadzone;
    private float _dpadVerticalDeadzone;

    private void Awake()
    {
        RegisterWithSceneReference();
    }

    private void RegisterWithSceneReference()
    {
        SceneReference.InputMappingManager = this;
    }

    private void Start()
    {
        CurrentScheme = InputScheme.Keyboard;
        _rightTriggerDeadzone =
            InputManager.PlayerOneConfiguration.axes.First(item => item.name == "Right Trigger").deadZone;
        _leftTriggerDeadzone =
            InputManager.PlayerOneConfiguration.axes.First(item => item.name == "Left Trigger").deadZone;
        _rightStickHorizontalDeadzone =
            InputManager.PlayerOneConfiguration.axes.First(item => item.name == "Right Stick Horizontal").deadZone;
        _rightStickVerticalDeadzone =
            InputManager.PlayerOneConfiguration.axes.First(item => item.name == "Right Stick Vertical").deadZone;
        _leftStickHorizontalDeadzone =
            InputManager.PlayerOneConfiguration.axes.First(item => item.name == "Left Stick Horizontal").deadZone;
        _leftStickVerticalDeadzone =
            InputManager.PlayerOneConfiguration.axes.First(item => item.name == "Left Stick Vertical").deadZone;
        _dpadHorizontalDeadzone =
            InputManager.PlayerOneConfiguration.axes.First(item => item.name == "DPAD Horizontal").deadZone;
        _dpadVerticalDeadzone =
            InputManager.PlayerOneConfiguration.axes.First(item => item.name == "DPAD Vertical").deadZone;
    }

    private void Update()
    {
        CheckForControllerChange();
    }

    private void CheckForControllerChange()
    {
        if (!InputManager.AnyInput()) return;
        if (AnyKeyboardInput())
        {
            InputManager.SetInputConfiguration("KeyboardAndMouse", PlayerID.One);
            CurrentScheme = InputScheme.Keyboard;
        }
        if (AnyGamepadInput())
        {
            InputManager.SetInputConfiguration("XBox_360_Windows", PlayerID.One);
            CurrentScheme = InputScheme.Gamepad;
        }
    }

    private bool AnyGamepadInput()
    {
        return InputManager.GetButton("Right Bumper") ||
               InputManager.GetButton("Left Bumper") ||
               InputManager.GetButton("Button A") ||
               InputManager.GetButton("Button B") ||
               InputManager.GetButton("Button X") ||
               InputManager.GetButton("Button Y") ||
               InputManager.GetButton("Start") ||
               InputManager.GetButton("Back") ||
               InputManager.GetAxis("Right Trigger") > _rightTriggerDeadzone ||
               InputManager.GetAxis("Right Trigger") < -_rightTriggerDeadzone ||
               InputManager.GetAxis("Left Trigger") > _leftTriggerDeadzone ||
               InputManager.GetAxis("Left Trigger") < -_leftTriggerDeadzone ||
               InputManager.GetAxis("Right Stick Horizontal") > _rightStickHorizontalDeadzone ||
               InputManager.GetAxis("Right Stick Horizontal") < -_rightStickHorizontalDeadzone ||
               InputManager.GetAxis("Right Stick Vertical") > _rightStickVerticalDeadzone ||
               InputManager.GetAxis("Right Stick Vertical") < -_rightStickVerticalDeadzone ||
               InputManager.GetAxis("Left Stick Horizontal") > _leftStickHorizontalDeadzone ||
               InputManager.GetAxis("Left Stick Horizontal") < -_leftStickHorizontalDeadzone ||
               InputManager.GetAxis("Left Stick Vertical") > _leftStickVerticalDeadzone ||
               InputManager.GetAxis("Left Stick Vertical") < -_leftStickVerticalDeadzone ||
               InputManager.GetAxis("DPAD Horizontal") > _dpadHorizontalDeadzone ||
               InputManager.GetAxis("DPAD Horizontal") < -_dpadHorizontalDeadzone ||
               InputManager.GetAxis("DPAD Vertical") > _dpadVerticalDeadzone ||
               InputManager.GetAxis("DPAD Vertical") < -_dpadVerticalDeadzone;

    }

    private bool AnyKeyboardInput()
    {
        return InputManager.GetKey(KeyCode.W) ||
               InputManager.GetKey(KeyCode.S) ||
               InputManager.GetKey(KeyCode.A) ||
               InputManager.GetKey(KeyCode.D) ||
               InputManager.GetKey(KeyCode.UpArrow) ||
               InputManager.GetKey(KeyCode.DownArrow) ||
               InputManager.GetKey(KeyCode.LeftArrow) ||
               InputManager.GetKey(KeyCode.RightArrow) ||
               InputManager.GetKey(KeyCode.Alpha1) ||
               InputManager.GetKey(KeyCode.Alpha2) ||
               InputManager.GetKey(KeyCode.Alpha3) ||
               InputManager.GetKey(KeyCode.LeftShift) ||
               InputManager.GetKey(KeyCode.Space) ||
               InputManager.GetKey(KeyCode.Return) ||
               InputManager.GetKey(KeyCode.Escape);
    }

    public float GetThrust()
    {
        return (CurrentScheme == InputScheme.Keyboard) ? Convert.ToSingle(InputManager.GetKey(KeyCode.W)
            || InputManager.GetKey(KeyCode.UpArrow)) : InputManager.GetAxis("Right Trigger");
    }

    public float GetReverse()
    {
        return (CurrentScheme == InputScheme.Keyboard) ? Convert.ToSingle(InputManager.GetKey(KeyCode.S)
            || InputManager.GetKey(KeyCode.DownArrow)) : InputManager.GetAxis("Left Trigger");
    }

    public float GetTurnRight()
    {
        return (CurrentScheme == InputScheme.Keyboard) ? Convert.ToSingle(InputManager.GetKey(KeyCode.D)
            || InputManager.GetKey(KeyCode.RightArrow)) : Mathf.Max(0, InputManager.GetAxis("Right Stick Horizontal"));
    }

    public float GetTurnLeft()
    {
        return (CurrentScheme == InputScheme.Keyboard) ? Convert.ToSingle(InputManager.GetKey(KeyCode.A)
            || InputManager.GetKey(KeyCode.LeftArrow)) : Mathf.Abs(Mathf.Min(0, InputManager.GetAxis("Right Stick Horizontal")));
    }

    public bool GetShoot()
    {
        return (CurrentScheme == InputScheme.Keyboard) ? InputManager.GetKey(KeyCode.Space) : InputManager.GetButton("Right Bumper");
    }

    public bool GetShield()
    {
        return (CurrentScheme == InputScheme.Keyboard) ? InputManager.GetKeyDown(KeyCode.LeftShift) : InputManager.GetButtonDown("Left Bumper");
    }

    public bool GetShieldRelease()
    {
        return (CurrentScheme == InputScheme.Keyboard) ? InputManager.GetKeyUp(KeyCode.LeftShift) : InputManager.GetButtonUp("Left Bumper");
    }

    public bool GetSelectPulse()
    {
        return (CurrentScheme == InputScheme.Keyboard) ? InputManager.GetKey(KeyCode.Alpha1) : InputManager.GetAxis("DPAD Horizontal") < 0;
    }

    public bool GetSelectScattershot()
    {
        return (CurrentScheme == InputScheme.Keyboard) ? InputManager.GetKey(KeyCode.Alpha2) : InputManager.GetAxis("DPAD Vertical") > 0;
    }

    public bool GetSelectRailgun()
    {
        return (CurrentScheme == InputScheme.Keyboard) ? InputManager.GetKey(KeyCode.Alpha3) : InputManager.GetAxis("DPAD Horizontal") > 0;
    }

    public float GetMoveReticleHorizontal()
    {
        if (CurrentScheme == InputScheme.Keyboard)
        {
            return Convert.ToSingle(InputManager.GetKey(KeyCode.RightArrow) || InputManager.GetKey(KeyCode.D)) -
                Convert.ToSingle(InputManager.GetKey(KeyCode.LeftArrow) || InputManager.GetKey(KeyCode.A));
        }
        return Mathf.Clamp(InputManager.GetAxis("Right Stick Horizontal") + InputManager.GetAxis("Left Stick Horizontal"), -1, 1);
    }

    public float GetMoveReticleVertical()
    {
        if (CurrentScheme == InputScheme.Keyboard)
        {
            return Convert.ToSingle(InputManager.GetKey(KeyCode.UpArrow) || InputManager.GetKey(KeyCode.W)) -
                Convert.ToSingle(InputManager.GetKey(KeyCode.DownArrow) || InputManager.GetKey(KeyCode.S));
        }
        return -Mathf.Clamp(InputManager.GetAxis("Right Stick Vertical") + InputManager.GetAxis("Left Stick Vertical"), -1, 1);
    }

    public bool GetReticleConfirm()
    {
        return (CurrentScheme == InputScheme.Keyboard)
            ? InputManager.GetKey(KeyCode.Space)
            : InputManager.GetButton("Button A") || InputManager.GetButton("Right Bumper");
    }
}
