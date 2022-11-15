using UnityEngine;
using System.Text;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TheGame : MonoBehaviour
{
    public static TheGame Instance { get; private set; }
    private static readonly int SUMMARY_CHAR_WIDTH = 30;

    public Camera camera;
    public LevelSelect levelSelect;
    public SummaryScreen summaryScreen;
    [Space]
    public GameConfig gameConfig;

    private int currentLevel;
    private int totalPoints;

    public void Awake()
    {
        Instance = this;
        currentLevel = -1;
        ShowLevelSelect();
        levelSelect.SetLevelButtons(gameConfig.levels);
    }

    public void StartLevel(int levelIndex)
    {
        if(levelIndex < 0 || levelIndex >= gameConfig.levels.Length)
            return;
        DestroyCurrentLevel();
        GameConfig.LevelData levelData = gameConfig.levels[levelIndex];
        currentLevel = levelIndex;
        SceneManager.LoadSceneAsync(levelData.sceneName, LoadSceneMode.Additive);
        DisableUI();
    }

    public void RetryLevel()
    {
        DestroyCurrentLevel();
        StartLevel(currentLevel);
    }

    public void LevelComplete(IDictionary<string, int> points)
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach(var pair in points)
        {
            totalPoints += pair.Value;
            int charCount = pair.Key.Length + $":{pair.Value}".Length;
            stringBuilder.Append($"{pair.Key}:".PadRight(SUMMARY_CHAR_WIDTH - charCount, ' '));
            stringBuilder.Append($"{pair.Value}\n");
        }
        summaryScreen.scoresText.text = stringBuilder.ToString();
        summaryScreen.totalText.text = $"Total: {totalPoints}";
        ShowSummary();
    }

    public void ShowLevelSelect()
    {
        levelSelect.gameObject.SetActive(true);
        summaryScreen.gameObject.SetActive(false);
    }

    public void ShowSummary()
    {
        levelSelect.gameObject.SetActive(false);
        summaryScreen.gameObject.SetActive(true);
    }

    private void DisableUI()
    {
        levelSelect.gameObject.SetActive(false);
        summaryScreen.gameObject.SetActive(false);
    }

    private void DestroyCurrentLevel()
    {
        if(currentLevel < 0)
            return;
        SceneManager.UnloadSceneAsync(gameConfig.levels[currentLevel].sceneName);
    }
}
