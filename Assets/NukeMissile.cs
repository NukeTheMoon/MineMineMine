using UnityEngine;
using System.Collections;

public class NukeMissile : MonoBehaviour {

    private float _initialLocalScaleX;
    private float _expansionProgress;

    private void Start()
    {
        _initialLocalScaleX = transform.localScale.x;
        _expansionProgress = 0.0f;
        StartCoroutine(ExpiryCoroutine());
    }

    private IEnumerator ExpiryCoroutine()
    {
        yield return new WaitForSeconds(TimeHelper.MillisecondsToSeconds(SceneReference.WeaponManager.NukeLifeMs));
        Destroy(gameObject);
    }

    private void Expand()
    {
        _expansionProgress += Time.deltaTime / TimeHelper.MillisecondsToSeconds(SceneReference.WeaponManager.NukeLifeMs);
        transform.localScale = 
            new Vector3(
                Mathf.Lerp(_initialLocalScaleX, SceneReference.WeaponManager.NukeExpansion, _expansionProgress), 
                Mathf.Lerp(_initialLocalScaleX, SceneReference.WeaponManager.NukeExpansion, _expansionProgress), 
                Mathf.Lerp(_initialLocalScaleX, SceneReference.WeaponManager.NukeExpansion, _expansionProgress));
    }

    private void Update()
    {
        Expand();
    }

}
