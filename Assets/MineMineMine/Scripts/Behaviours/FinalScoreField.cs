using UnityEngine;
using System.Collections;
using System.Globalization;
using Assets.MineMineMine.Scripts.Managers;
using TMPro;

public class FinalScoreField : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        UpdateText();
        SceneReference.ScorekeepingManager.OnScoreChanged += ScorekeepingManager_OnScoreChanged;
    }

    private void ScorekeepingManager_OnScoreChanged(object sender, System.EventArgs e)
    {
        UpdateText();
    }

    private void UpdateText()
    {
        _text.text = SceneReference.ScorekeepingManager.TotalScore.ToString("n0", CultureInfo.InvariantCulture);
    }
}
