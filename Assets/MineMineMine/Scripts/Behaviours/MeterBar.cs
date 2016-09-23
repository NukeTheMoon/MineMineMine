using UnityEngine;
using System.Collections;

public class MeterBar : MonoBehaviour
{

    public float AnchorXMinAtFull;
    public float AnchorXMaxAtFull;
    public float AnchorXMinAtDepleted;
    public float AnchorXMaxAtDepleted;

    private RectTransform _rect;
    private float _initialAnchorYMin;
    private float _initialAnchorYMax;

    private void Start()
    {
        _rect = GetComponent<RectTransform>();
        _initialAnchorYMin = _rect.anchorMin.y;
        _initialAnchorYMax = _rect.anchorMax.y;
    }

    private void Update()
    {
        ResizeBar();
    }

    private void ResizeBar()
    {
        float newAnchorXMin = Mathf.Lerp(AnchorXMinAtDepleted, AnchorXMinAtFull,
            SceneReference.MeterManager.GetCurrentMeter() / SceneReference.MeterManager.MaximumMeter);
        float newAnchorXMax = Mathf.Lerp(AnchorXMaxAtDepleted, AnchorXMaxAtFull,
            SceneReference.MeterManager.GetCurrentMeter() / SceneReference.MeterManager.MaximumMeter);
        _rect.anchorMin = new Vector2(newAnchorXMin, _initialAnchorYMin);
        _rect.anchorMax = new Vector2(newAnchorXMax, _initialAnchorYMax);

    }

}
