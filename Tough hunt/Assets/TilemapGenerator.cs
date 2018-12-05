using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class TilemapGenerator : MonoBehaviour {
    public int width;
    public int height;
    public float scaleX;
    public float scaleY;
    public int xOrg;
    public int yOrg;
    public List<TileForGenerator> tilesForGenerator;
    public Tile grass;

    private int[] grassY;
    private Tilemap tilemap;
    private Texture2D noiseTex;

	// Use this for initialization
	void Start () {
        grassY = new int[width];
        for (int i = 0; i < width; i++)
        {
            grassY[i] = -1;
        }
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
        tilemap.ClearAllTiles();
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
        }
        /*
        }
        for (int y = height; y >= 0; y--)
        {
            for (int x = 0; x < width; x++)
            {
                if(grassX[x] == -1)
                {
                    if (GetTile(Mathf.PerlinNoise((xOrg + x) / (width * scale), (yOrg + y) / (height * scale))))
                    {
                        if (GetTile(Mathf.PerlinNoise((xOrg + x) / (width * scale), (yOrg + y) / (height * scale))).name.Contains("Dirt"))
                        {
                            tilemap.SetTile(new Vector3Int(x, y, 0), grass);
                            grassX[x] = y;
                            continue;
                        }
                        else continue;
                    }
                }
                tilemap.SetTile(new Vector3Int(x, y, 0), GetTile(Mathf.PerlinNoise((xOrg + x) / (width * scale), (yOrg + y) / (height * scale))));
            }
        }
        */
    }
    
    Tile GetTile(float f)
    {
        foreach(TileForGenerator tfg in tilesForGenerator)
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
}
