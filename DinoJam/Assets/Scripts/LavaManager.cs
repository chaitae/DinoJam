using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LavaManager: MonoBehaviour
{
    private static readonly Vector3Int[] POSSIBLE_NEIGHBORS = new Vector3Int[]
    {
        new Vector3Int(1, 0, 0),
        new Vector3Int(-1, 0, 0),
        new Vector3Int(0, 1, 0),
        new Vector3Int(0, -1, 0)
    };

    public Tilemap tilemap;
    [Space]
    public Tile trenchTile;
    public Tile lavaTile;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            StartLava(new Vector3Int(Mathf.FloorToInt(worldPosition.x), Mathf.FloorToInt(worldPosition.y), 0));
        }
    }

    public void StartLava(Vector3Int position)
    {
        TrenchTile trenchTile = tilemap.GetTile<TrenchTile>(position);
        if(trenchTile == null)
        {
            return;
        }

        tilemap.SetTile(position, lavaTile);
        StartCoroutine(TriggerNeighbors(trenchTile.flowDelaySeconds, position));
    }

    private IEnumerator TriggerNeighbors(float waitTime, Vector3Int position)
    {
        yield return new WaitForSeconds(waitTime);
        foreach(Vector3Int possibleNeighbor in POSSIBLE_NEIGHBORS)
        {
            StartLava(position + possibleNeighbor);
        }
    }
}
