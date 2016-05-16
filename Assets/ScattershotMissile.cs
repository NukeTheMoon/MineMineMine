using UnityEngine;
using System.Collections;
using System;

public class ScattershotMissile : MonoBehaviour {

    private float _initialLocalScaleX;
    private float _expansionProgress;

    private void Start () {
        _initialLocalScaleX = transform.localScale.x;
        _expansionProgress = 0.0f;
        StartCoroutine(ExpiryCoroutine());
    }

    private IEnumerator ExpiryCoroutine()
    {
        yield return new WaitForSeconds(TimeHelper.MillisecondsToSeconds(SceneReference.WeaponManager.ScattershotLifeMs));
        Destroy(gameObject);
    }

    private void Expand()
    {
        _expansionProgress += Time.deltaTime / TimeHelper.MillisecondsToSeconds(SceneReference.WeaponManager.ScattershotLifeMs);
        transform.localScale = new Vector3(Mathf.Lerp(_initialLocalScaleX, SceneReference.WeaponManager.ScattershotExpansion, _expansionProgress), transform.localScale.y, transform.localScale.z);
    }

    private void Update () {
        Expand();
	}

}
