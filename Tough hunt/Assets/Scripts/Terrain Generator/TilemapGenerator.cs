using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class TilemapGenerator : MonoBehaviour {
    [Header("Tilemap & noise settings")]
    public int width;
    public int height;
    public float scaleX;
    public float scaleY;
    public int xOrg;
    public int yOrg;

    [Header("Biomes")]
    public List<Biome> biomes;
    private Biome actualBiome;
    private int requiredBiomeWidth;
    private Biome[] biomeOnX;

    [Header("Other tilemaps")]
    public Tilemap background;

    [Header("Tiles for generator")]
    public List<TileForGenerator> tilesForGenerator;

    [Header("Unique tiles")]
    public Tile grass;

    [Header("Trees")]
    public List<TreeForGenerator> treesForGenerator;

    private int[] grassY;
    private Tilemap tilemap;
    private Texture2D noiseTex;

	// Use this for initialization
	void Start () {
        tilemap = GetComponent<Tilemap>();
        noiseTex = new Texture2D(width, height);
        GenerateTilemap();
	}
	
	// Update is called once per frame
	void Update () {
        //GenerateTilemap();
    }

    public void GenerateTilemap()
    {
        grassY = new int[width];
        for (int i = 0; i < width; i++)
        {
            grassY[i] = -1;
        }
        biomeOnX = new Biome[width];

        SetRandomBiome();
        tilemap.ClearAllTiles();
        background.ClearAllTiles();
        for (int i = 0; i < width; i++)
        {
            grassY[i] = -1;
        }
        for(int x = 0; x < width; x++)
        {
            for (int y = height; y > 0; y--)
            {
                if(grassY[x] != -1)
                {
                    if (y >= grassY[x])
                        continue;
                    tilemap.SetTile(new Vector3Int(x, y, 0), GetTile(Mathf.PerlinNoise((xOrg + x) / (width * scaleX), (yOrg + y) / (height * scaleY))));
                    continue;
                }
                if (GetTile(Mathf.PerlinNoise((xOrg + x) / (width * scaleX), (yOrg + y) / (height * scaleY))).name.Contains("Dirt"))
                {
                    if (x != 0)
                    {
                        if (y - grassY[x - 1] < 2)
                        {
                            tilemap.SetTile(new Vector3Int(x, y, 0), grass);
                            grassY[x] = y;
                            continue;
                        }
                        else
                        {
                            grassY[x] = grassY[x - 1] + 1;
                            tilemap.SetTile(new Vector3Int(x, grassY[x], 0), grass);
                            continue;
                        }
                    }
                    else
                    {
                        tilemap.SetTile(new Vector3Int(x, y, 0), grass);
                        grassY[x] = y;
                        continue;
                    }
                }
                if (x != 0)
                {
                    //print(grassY[x - 1]);
                    if (grassY[x - 1] - FindNearestDirt(x, y) > 1)
                    {
                        grassY[x] = grassY[x - 1] - 1;
                        tilemap.SetTile(new Vector3Int(x, grassY[x], 0), grass);
                        continue;
                    }
                    else
                    {
                        //print("X: " + x + " Y: " + y);
                        //print(grassY[x - 1] - FindNearestDirt(x, y));
                        grassY[x] = grassY[x - 1];
                        //print(grassY[x]);
                        tilemap.SetTile(new Vector3Int(x, grassY[x], 0), grass);
                        continue;
                    }
                }
            }
            requiredBiomeWidth--;
            biomeOnX[x] = actualBiome;
            if (requiredBiomeWidth <= 0)
                SetRandomBiome();
        }
        GenerateTrees();
    }
    
    Tile GetTile(float f)
    {
        foreach(TileForGenerator tfg in actualBiome.tiles)
        {
            if (tfg.IsInRange(f))
                return tfg.tile;
            else continue;
        }
        return null;
    }

    int FindNearestDirt(int x, int y)
    {
        while(!GetTile(Mathf.PerlinNoise((xOrg + x) / (width * scaleX), (yOrg + y) / (height * scaleY))).name.Contains("Dirt"))
        {
            y--;
        }
        return y;
    }

    void GenerateTrees()
    {
        for (int x = 0; x < width; x++)
        {
            int treeType = Random.Range(0, biomeOnX[x].trees.Count);
            if(IsFlat(x, biomeOnX[x].trees[treeType].width))
            {
                SetTree(x, treeType);
            }
        }
    }

    bool IsFlat(int startTile, int tileCount)
    {
        for (int x = startTile; x < startTile+tileCount; x++)
        {
            if (!(x + 1 < width))
                return false;
            if ((x - 3 < 0))
                return false;
            if(grassY[x] != grassY[x+1])
            {
                return false;
            }
        }
        return true;
    }

    void SetTree(int x, int treeType)
    {
        TreeForGenerator tree = biomeOnX[x].trees[treeType];
        //if ((tree.biomes == BiomeTypes.Plains))
        //    return;
        if (background.GetTile(new Vector3Int(x, grassY[x] + 1, 0)) != null)
            return;
        for (int treeY = 0; treeY < tree.height; treeY++)
        {
            for (int treeX = 0; treeX < tree.width; treeX++)
            {
                Vector3Int pos = new Vector3Int(x + treeX, grassY[x] + treeY + 1, 0);
                if (background.GetTile(pos) == null)
                {
                    int tileID = treeX + treeY * tree.width;
                    if (tree.GetTile(tileID))
                        background.SetTile(pos, tree.GetTile(tileID));
                }
            }
        }
    }

    void SetRandomBiome()
    {
        int randomBiome = Random.Range(0, biomes.Count);
        actualBiome = biomes[randomBiome];
        requiredBiomeWidth = Random.Range(actualBiome.minWidth, actualBiome.maxWidth);
        grass = actualBiome.grass;
    }
}
