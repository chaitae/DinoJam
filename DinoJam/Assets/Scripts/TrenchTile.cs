using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TrenchTile", menuName = "Tiles/TrenchTile")]
public class TrenchTile : Tile
{
    public LavaTile lavaTile;
    public float flowDelaySeconds;
}
