using UnityEngine;
using System.Collections;
using System.Globalization;
using TMPro;

public class YieldAmountField : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = SceneReference.ScorekeepingManager.TotalScore.ToString();
        SceneReference.ScorekeepingManager.ScoreChanged += ScorekeepingManager_ScoreChanged;
    }

    private void ScorekeepingManager_ScoreChanged(object sender, System.EventArgs e)
    {
        _text.text = SceneReference.ScorekeepingManager.TotalScore.ToString("n0", CultureInfo.InvariantCulture);
    }
}
