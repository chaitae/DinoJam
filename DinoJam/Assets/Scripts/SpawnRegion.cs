using UnityEngine;

public class SpawnRegion : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnData
    {
        public GameObject spawnThing;
        public int count;
    }

    public SpawnData[] spawnData;
    public Collider2D spawnArea;

    public void Awake()
    {
        Bounds bounds = spawnArea.bounds;
        foreach(SpawnData data in spawnData)
        {
            for(int i = 0; i < data.count; i++)
            {
                Vector2 position = new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
                GameObject spawnThing = Instantiate(data.spawnThing);
                spawnThing.transform.position = position;
                spawnThing.transform.parent = transform;
            }
        }
    }
}
