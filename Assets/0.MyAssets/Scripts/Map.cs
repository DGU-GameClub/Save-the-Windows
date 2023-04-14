using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector3 currentTilePos;

    Vector3Int preTilePos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = tilemap.WorldToCell(mousePos);
        TileBase tile = tilemap.GetTile(cellPos);

        if (tile != null)
        {
            if (cellPos != preTilePos)
            {
                tilemap.SetTileFlags(preTilePos, TileFlags.None);
                tilemap.SetColor(preTilePos, Color.white);
            }

            tilemap.SetTileFlags(cellPos, TileFlags.None);
            tilemap.SetColor(cellPos, new Color(1, 1, 1, 0.7f));

            preTilePos = cellPos;
            currentTilePos = tilemap.CellToWorld(cellPos);
        }
        else
        {
            if (preTilePos != null && tilemap.GetTile(preTilePos) != null)
            {
                tilemap.SetTileFlags(preTilePos, TileFlags.None);
                tilemap.SetColor(preTilePos, Color.white);
            }
            preTilePos = cellPos;
        }
    }

    public Vector3 GetCoordTileUnderMouse() {
        return currentTilePos;
    }
}
