using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public enum LevelState
    {
        Pregame,
        Game,
        Postgame
    }

    public static LevelManager Instance { get; private set; }

    public PointsText pointsPrefab;
    public Tilemap tilemap;
    public float timeForRoundToEnd;
    public GameObject sceneCamera;

    public Bounds LevelBounds { get; private set; }
    public LevelState State { get; private set; }

    private IDictionary<string, int> points;
    private float inactivityTimer;

    public void Awake()
    {
        Destroy(sceneCamera);
        State = LevelState.Pregame;
        points = new Dictionary<string, int>();
        Instance = this;
        Camera camera = TheGame.Instance.camera;
        Vector3 center = camera.transform.position;
        center.z = -5;
        Vector3 topRight = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        topRight.z = 5;
        LevelBounds = new Bounds(camera.transform.position, (topRight - center) * 2);
    }

    public void ResetInactivityTimer()
    {
        inactivityTimer = 0;
    }

    public void FixedUpdate()
    {
        if(State != LevelState.Game)
            return;
        inactivityTimer += Time.fixedDeltaTime;
        if(inactivityTimer > timeForRoundToEnd)
            RoundOver();
    }

    public void UserStarted()
    {
        inactivityTimer = 0;
        State = LevelState.Game;
    }

    public void RegisterPoints(Vector2 pointLocation, string key, int value)
    {
        if(value <= 0)
            return;
        if(!points.ContainsKey(key))
        {
            points.Add(key, 0);
        }
        points[key] += value;
        PointsText pointsText = Instantiate(pointsPrefab, pointLocation, Quaternion.identity);
        pointsText.text.text = $"+{value}";
    }

    private void RoundOver()
    {
        State = LevelState.Postgame;
        TheGame.Instance.LevelComplete(points);
    }
}
