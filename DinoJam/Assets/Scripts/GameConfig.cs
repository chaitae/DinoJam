using UnityEngine.SceneManagement;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Data/GameConfig")]
public class GameConfig: ScriptableObject
{
    [Serializable]
    public struct LevelData
    {
        public string sceneName;
        public int minPoints;
    }

    public LevelData[] levels;
}
