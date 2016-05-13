using UnityEngine;
using System.Collections;

public class RailgunMissile : MonoBehaviour {


    void Start()
    {
        StartCoroutine(ExpiryCoroutine());
    }

    private IEnumerator ExpiryCoroutine()
    {
        yield return new WaitForSeconds(TimeHelper.MillisecondsToSeconds(SceneReference.WeaponManager.RailgunLifeMs));
        Destroy(gameObject);
    }
}
