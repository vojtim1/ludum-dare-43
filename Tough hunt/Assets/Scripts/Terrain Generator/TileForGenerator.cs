using UnityEngine.Tilemaps;
using UnityEngine;

[System.Serializable]
public class TileForGenerator {
    [Range(0,1)]
    public float min;
    [Range(0,1)]
    public float max;
    public Tile tile;

    public bool IsInRange(float f)
    {
        if (f >= min && f <= max)
            return true;
        else return false;
    }
}
