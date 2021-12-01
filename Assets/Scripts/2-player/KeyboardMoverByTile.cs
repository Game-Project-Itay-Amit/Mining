﻿using UnityEngine;
using UnityEngine.Tilemaps;


/**
 * This component allows the player to move by clicking the arrow keys,
 * but only if the new position is on an allowed tile.
 */
public class KeyboardMoverByTile: KeyboardMover {
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTiles allowedTiles = null;
    [SerializeField] TileBase breaker = null;
    [SerializeField] TileBase changer = null;

    private TileBase TileOnPosition(Vector3 worldPosition) {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }

    void Update()  {
        Vector3 newPosition = NewPosition();
        Vector3Int np = new Vector3Int (0,0,0);
        np.x = Mathf.FloorToInt(newPosition.x);
        np.y = Mathf.FloorToInt(newPosition.y);
        np.z = Mathf.FloorToInt(newPosition.z);
        Debug.Log(newPosition + " , " + np);

        TileBase tileOnNewPosition = TileOnPosition(newPosition);
        if (tileOnNewPosition == breaker && Input.GetKeyDown(KeyCode.X))
        {
            new WaitForSeconds(1f);
            tilemap.SetTile(np, changer);
        }
        else if (allowedTiles.Contain(tileOnNewPosition))
        {
            transform.position = newPosition;
        }
        else
        {
            Debug.Log("You cannot walk on " + tileOnNewPosition + "!");
        }
    }
}
