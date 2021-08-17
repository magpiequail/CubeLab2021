using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapColor : MonoBehaviour
{
    public Tilemap tilemap;


    public Color currentTileColor;

    BoundsInt bounds;
    //TileBase[] allTiles;

    Character[] charactersArray;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        charactersArray = FindObjectsOfType<Character>();

    }

    // Start is called before the first frame update
    void Start()
    {
        bounds = tilemap.cellBounds;
        //allTiles = tilemap.GetTilesBlock(bounds);
    }

    // Update is called once per frame
    void Update()
    {
        if(tilemap != null)
        {
            for (int i = 0; i < charactersArray.Length; i++)
            {
                //tilemap.RefreshAllTiles();
                //tmc.x = tmc.tilemap.WorldToCell(nextPos).x;
                //tmc.y = tmc.tilemap.WorldToCell(nextPos).y;
                Vector3Int v3Int = new Vector3Int(tilemap.WorldToCell(charactersArray[i].nextPos).x, tilemap.WorldToCell(charactersArray[i].nextPos).y, 0);
                tilemap.SetTileFlags(v3Int, TileFlags.None);
                tilemap.SetColor(v3Int, currentTileColor);

            }
        }
        

    }

    public void ColorTiles() //this function refreshes tiles
    {
        /*for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    tile.SetTileFlags(, TileFlags.None);
                    tilemap.SetColor(v3Int, currentTileColor);
                }
                else
                {
                    
                }
            }
        }*/
        tilemap.RefreshAllTiles();
    }

}

