using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public Transform scrollParent;
    public LevelButton levelButtonPrefab;

    public void SetLevelButtons(GameConfig.LevelData[] levelData)
    {
        for(int i = 0; i < levelData.Length; i++)
        {
            LevelButton levelButton = Instantiate(levelButtonPrefab);
            levelButton.levelIndex = i;
            levelButton.text.text = $"{levelButton.levelIndex + 1}";
            levelButton.transform.parent = scrollParent;
        }
    }
}
