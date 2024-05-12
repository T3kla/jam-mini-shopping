using System.Collections.Generic;
using Data;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Belt belt = null;

    public Tile tilePrefab = null;
    public List<RoundData> rounds = null;

    private Tile[,] _tiles = null;
    private RoundData _currentRound = null;

    private void Awake()
    {
        LoadRound(0);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
            if (FoobCard.selected && Tile.selected)
                Debug.Log("WOOO");
    }

    private void LoadRound(int round)
    {
        if (rounds.Count <= round) return;

        if (_tiles != null)
            foreach (var tile in _tiles)
                Destroy(tile);

        var data = rounds[round];
        _currentRound = data;

        var width = data.boardTiles.x;
        var height = data.boardTiles.y;
        var tileSize = data.boardSize.x;
        var tileScale = data.boardSize.y;

        _tiles = new Tile[width, height];

        for (var x = 0; x < width; x++)
        for (var y = 0; y < height; y++)
        {
            var posX = x * tileSize - (width - 1) * tileSize / 2;
            var posY = y * tileSize - (height - 1) * tileSize / 2;
            var pos = new Vector3(posX, posY, 0);
            var tile = Instantiate(tilePrefab, pos, Quaternion.identity);
            tile.Board = this;
            tile.Position = new Vector2Int(x, y);
            tile.transform.localScale = new Vector3(tileScale, tileScale, tileScale);
            tile.transform.parent = transform;
            _tiles[x, y] = tile;
        }

        belt.StartDealingCards(data.foobs, data.beltSpeed);
    }

    public void UpdateTileColors()
    {
        foreach (var t in _tiles)
            t.SpriteRenderer.color = Color.white;

        var tile = Tile.selected;
        var foob = FoobCard.selected;

        if (!tile || !foob)
            return;

        // Get the collision matrix of the current foob

        var mtx = new bool[3, 3];

        mtx[0, 0] = foob.Data.shape0.x == 1;
        mtx[0, 1] = foob.Data.shape0.y == 1;
        mtx[0, 2] = foob.Data.shape0.z == 1;
        mtx[1, 0] = foob.Data.shape1.x == 1;
        mtx[1, 1] = foob.Data.shape1.y == 1;
        mtx[1, 2] = foob.Data.shape1.z == 1;
        mtx[2, 0] = foob.Data.shape2.x == 1;
        mtx[2, 1] = foob.Data.shape2.y == 1;
        mtx[2, 2] = foob.Data.shape2.z == 1;

        // If the card is rotated, rotate the matrix

        var rot = foob.Rotation;

        for (var i = 0; i < rot; ++i)
        for (var j = 2; j >= 0; --j)
        for (var k = 0; k < 3; ++k)
            mtx[k, 2 - j] = mtx[j, k];

        // Search adjacent tiles for a match
        
    }
}