using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public PointsText pointsPrefab;
    public Tilemap tilemap;

    public Bounds LevelBounds { get; private set; }

    private IDictionary<string, int> points;

    public void Awake()
    {
        points = new Dictionary<string, int>();
        Instance = this;
        Camera camera = Camera.main;
        Vector3 center = camera.transform.position;
        center.z = -5;
        Vector3 topRight = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        topRight.z = 5;
        LevelBounds = new Bounds(camera.transform.position, (topRight - center) * 2);
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
}
