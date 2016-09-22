using System;
using UnityEngine;
using System.Collections;
using TMPro;

public class WeaponSelectBlink : MonoBehaviour
{
    public Material OutlineStrokeMaterial;
    public Material OutlineFillMaterial;
    public Material IconWhiteMaterial;
    public Material IconBlackMaterial;
    public int BlinksCount;
    public float BlinkIntervalMs;
    public Weapon AffectedWeapon;

    [NonSerialized]
    public bool Active;

    private Renderer _outlineRenderer;
    private Renderer _iconRenderer;
    private TextMeshPro _text;
    private Animator _menuAnimator;
    private bool _inverted;
    private int _blinkCounter;
    private bool _blinking;

    private void Start()
    {
        _outlineRenderer = transform.Find("Outline").GetComponent<MeshRenderer>();
        _iconRenderer = transform.Find("Icon").GetComponent<MeshRenderer>();
        _text = transform.Find("Name").GetComponent<TextMeshPro>();
        _menuAnimator = transform.parent.GetComponent<Animator>();
        _inverted = false;
        _blinkCounter = 0;
        Active = false;
        SetInitialState();
    }

    private void SetInitialState()
    {
        switch (SceneReference.WeaponManager.CurrentWeapon)
        {
            case Weapon.PulseEmitter:
                if (AffectedWeapon == Weapon.PulseEmitter)
                {
                    StartBlinking();
                }
                break;
            case Weapon.Scattershot:
                if (AffectedWeapon == Weapon.Scattershot)
                {
                    StartBlinking();
                }
                break;
            case Weapon.Railgun:
                if (AffectedWeapon == Weapon.Railgun)
                {
                    StartBlinking();
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Update()
    {
        if (SceneReference.InputMappingManager.GetSelectPulse())
        {
            if (AffectedWeapon == Weapon.PulseEmitter)
            {
                if (!_blinking)
                    StartBlinking();
            }
            else
            {
                StopBlinking();
                SetWhite();
            }
        }
        if (SceneReference.InputMappingManager.GetSelectScattershot())
        {
            if (AffectedWeapon == Weapon.Scattershot)
            {
                if (!_blinking)
                    StartBlinking();
            }
            else
            {
                StopBlinking();
                SetWhite();
            }
        }
        if (SceneReference.InputMappingManager.GetSelectRailgun())
        {
            if (AffectedWeapon == Weapon.Railgun)
            {
                if (!_blinking)
                    StartBlinking();
            }
            else
            {
                StopBlinking();
                SetWhite();
            }
        }
    }

    public void SetWhite()
    {
        _outlineRenderer.material = OutlineStrokeMaterial;
        _iconRenderer.material = IconWhiteMaterial;
        _text.color = Color.white;
        _inverted = false;
    }

    public void SetBlack()
    {
        _outlineRenderer.material = OutlineFillMaterial;
        _iconRenderer.material = IconBlackMaterial;
        _text.color = Color.black;
        _inverted = true;
    }

    public void InvertColors()
    {
        if (_inverted)
        {
            SetWhite();
        }
        else
        {
            SetBlack();
        }
    }

    public void StartBlinking()
    {
        _blinkCounter = 0;
        InvokeRepeating("Blink", 0.001f, TimeHelper.MillisecondsToSeconds(BlinkIntervalMs));
        _blinking = true;
        ShowMenu();
    }

    private void ShowMenu()
    {
        _menuAnimator.SetBool("IsVisible", true);
    }

    private void HideMenu()
    {
        _menuAnimator.SetBool("IsVisible", false);
    }

    public void StopBlinking()
    {
        CancelInvoke("Blink");
        _blinking = false;
    }

    private void Blink()
    {
        if (_blinkCounter < BlinksCount)
        {
            InvertColors();
            if (_inverted)
            {
                _blinkCounter++;
            }
        }
        else
        {
            StopBlinking();
            HideMenu();
        }
    }
}
