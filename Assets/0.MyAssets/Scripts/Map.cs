using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector3 currentTilePos;

    Vector3Int preTilePos;
    public Vector3[] tilePosArr;

    private void Awake()
    {
        List<Vector3> worldPositions = new List<Vector3>();
        for (int x = 0; x < tilemap.size.x; x++)
        {
            for (int y = 0; y < tilemap.size.y; y++)
            {
                Vector3Int cellPos = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(cellPos);
                if (tile != null)
                {
                    Vector3 worldPos = tilemap.CellToWorld(cellPos);
                    worldPositions.Add(worldPos);
                    Debug.Log(worldPos);
                }
            }
        }
        tilePosArr = worldPositions.ToArray();
    }

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
            tilemap.SetColor(cellPos, new Color(1,1,1,0.7f));

            preTilePos = cellPos;

            currentTilePos = tilemap.CellToWorld(cellPos);
        }
    }

    public Vector3 GetCoordTileUnderMouse() {
        return currentTilePos;
    }


}
