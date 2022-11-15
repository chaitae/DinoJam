using UnityEngine;
using TMPro;

public class SummaryScreen : MonoBehaviour
{
    public TextMeshProUGUI scoresText;
    public TextMeshProUGUI totalText;

    public void OnRetryClicked()
    {
        TheGame.Instance.RetryLevel();
    }

    public void OnLevelSelectClicked()
    {
        TheGame.Instance.ShowLevelSelect();
    }
}
