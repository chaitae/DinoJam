using UnityEngine;
using TMPro;

public class LevelButton : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int levelIndex;

    public void OnPressed()
    {
        TheGame.Instance.StartLevel(levelIndex);
    }
}
