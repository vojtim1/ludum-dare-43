using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TreeForGenerator {
    //[EnumFlag]public BiomeTypes biomes;
    public string name;
    public int width;
    public int height;
    [SerializeField]
    List<Tile> tiles;

    public Tile GetTile(int i)
    {
        return tiles[i];
    }
}
