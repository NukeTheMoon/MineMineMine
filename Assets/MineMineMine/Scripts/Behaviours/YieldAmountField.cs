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
        SceneReference.ScorekeepingManager.OnScoreChanged += ScorekeepingManagerOnScoreChanged;
    }

    private void ScorekeepingManagerOnScoreChanged(object sender, System.EventArgs e)
    {
        _text.text = SceneReference.ScorekeepingManager.CurrentScore.ToString("n0", CultureInfo.InvariantCulture);
    }
}
