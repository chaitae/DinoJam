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

    private Tilemap tilemap => LevelManager.Instance.tilemap;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TriggerLava(new Vector3Int(Mathf.FloorToInt(worldPosition.x), Mathf.FloorToInt(worldPosition.y), 0));
        }
    }

    public void TriggerLava(Vector3Int position)
    {
        TrenchStartTile trenchTile = tilemap.GetTile<TrenchStartTile>(position);
        if(trenchTile == null)
        {
            return;
        }

        LevelManager.Instance.UserStarted();
        tilemap.SetTile(position, trenchTile.lavaTile);
        StartCoroutine(TriggerNeighbors(trenchTile.flowDelaySeconds, position));
    }

    public void StartLava(Vector3Int position)
    {
        TrenchTile trenchTile = tilemap.GetTile<TrenchTile>(position);
        if(trenchTile == null)
        {
            return;
        }

        LevelManager.Instance.ResetInactivityTimer();
        tilemap.SetTile(position, trenchTile.lavaTile);
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
