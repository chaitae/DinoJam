using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class FancyPlayButton
{
    private static string editingScenePath;

    [MenuItem("CLICK HERE/Play TheGame Scene")]
    public static void RunGame()
    {
        EditorApplication.playModeStateChanged += LoadEditingScene;
        string gamePath = AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("t:scene TheGame").FirstOrDefault());
        if (string.IsNullOrWhiteSpace(gamePath))
        {
            Debug.LogError("Can't find TheGame scene");
            return;
        }

        if (EditorApplication.isPlaying)
        {
            EditorApplication.ExitPlaymode();
            return;
        }

        if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            return;

        string activeSceneName = EditorSceneManager.GetActiveScene().name;
        editingScenePath = AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("t:scene " + activeSceneName).FirstOrDefault());

        EditorSceneManager.OpenScene(gamePath);
        EditorApplication.EnterPlaymode();
    }

    private static void LoadEditingScene(PlayModeStateChange stateChange)
    {
        if(stateChange != PlayModeStateChange.ExitingPlayMode)
            return;
        EditorApplication.playModeStateChanged -= LoadEditingScene;
        if(string.IsNullOrEmpty(editingScenePath))
            return;

        EditorApplication.update += OnUpdate;
    }

    private static void OnUpdate()
    {
        if (EditorApplication.isPlaying || EditorApplication.isPaused || 
                EditorApplication.isCompiling || EditorApplication.isPlayingOrWillChangePlaymode)
            return;

        EditorApplication.update -= OnUpdate;
        EditorSceneManager.OpenScene(editingScenePath);

    }
}
