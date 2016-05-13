using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Powerup : MonoBehaviour {

    private Weapon _weapon;
    private TextMesh _debugName;

    void Start()
    {
        PickPowerupType();
        SetDebugName();
    }

    private void PickPowerupType()
    {
        _weapon = (Weapon)UnityEngine.Random.Range(0, Enum.GetNames(typeof(Weapon)).Length);
        if (_weapon == SceneReference.WeaponManager.CurrentWeapon)
        {
            PickPowerupType();
        }
    }

    private void SetDebugName()
    {
        _debugName = GetComponentInChildren<TextMesh>();
        switch (_weapon)
        {
            case (Weapon.PulseEmitter):
                _debugName.text = "PULSE EMITTER";
                break;
            case (Weapon.Scattershot):
                _debugName.text = "SCATTERSHOT";
                break;
        }
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == TagsReference.PLAYER)
        {
            SceneReference.WeaponManager.ChangeWeapon(_weapon);
            Destroy(gameObject);
        }
    }

}
