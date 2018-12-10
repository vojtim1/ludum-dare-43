using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Biome {
    //public BiomeTypes biomeType;
    public string biomeName;
    [Header("Trees settings")]
    public List<TreeForGenerator> trees;

    [Header("Tiles settings")]
    public Tile grass;
    public List<TileForGenerator> tiles;

    [Header("Size settings")]
    public int minWidth;
    public int maxWidth;
}
