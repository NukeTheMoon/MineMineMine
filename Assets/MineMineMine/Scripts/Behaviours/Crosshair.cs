using System;
using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour
{

    public Material PulseCrosshairMaterial;
    public Material ScattershotCrosshairMaterial;
    public Material RailgunCrosshairMaterial;

    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        InitializeCrosshair();
    }

    private void InitializeCrosshair()
    {
        switch (SceneReference.WeaponManager.CurrentWeapon)
        {
            case Weapon.PulseEmitter:
                _renderer.material = PulseCrosshairMaterial;
                break;
            case Weapon.Scattershot:
                _renderer.material = ScattershotCrosshairMaterial;
                break;
            case Weapon.Railgun:
                _renderer.material = RailgunCrosshairMaterial;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Update()
    {
        if (SceneReference.InputMappingManager.GetSelectPulse())
        {
            _renderer.material = PulseCrosshairMaterial;
        }
        else if (SceneReference.InputMappingManager.GetSelectScattershot())
        {
            _renderer.material = ScattershotCrosshairMaterial;
        }
        else if (SceneReference.InputMappingManager.GetSelectRailgun())
        {
            _renderer.material = RailgunCrosshairMaterial;
        }
    }
}
