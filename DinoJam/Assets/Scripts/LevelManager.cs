using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Tilemap tilemap;

    public Bounds levelBounds { get; private set; }

    public void Awake()
    {
        instance = this;
        Camera camera = Camera.main;
        Vector3 center = camera.transform.position;
        center.z = -5;
        Vector3 topRight = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        topRight.z = 5;
        levelBounds = new Bounds(camera.transform.position, (topRight - center) * 2);
    }
}
