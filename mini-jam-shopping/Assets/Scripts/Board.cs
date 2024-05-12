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

        CanPlace();
    }

    public bool CanPlace()
    {
        var canPlace = true;

        var tile = Tile.selected;
        var foob = FoobCard.selected;

        if (!tile || !foob)
            return false;

        var mtxShape = GetShape(foob);
        var mtxTiles = GetTiles(tile.Position);

        if (mtxShape == null || mtxTiles == null)
            return false;

        for (var x = 0; x < 3; x++)
        for (var y = 0; y < 3; y++)
            if (mtxShape[x, y] && (!mtxTiles[x, y] || mtxTiles[x, y].IsOccupied))
                canPlace = false;

        for (var x = 0; x < 3; x++)
        for (var y = 0; y < 3; y++)
            if (mtxTiles[x, y] && mtxShape[x, y])
                mtxTiles[x, y].SpriteRenderer.color = canPlace ? Color.green : Color.red;

        return canPlace;
    }

    public bool[,] GetShape(FoobCard card)
    {
        var arr = new bool[3, 3];

        arr[0, 0] = card.Data.shape0.x == 1;
        arr[0, 1] = card.Data.shape0.y == 1;
        arr[0, 2] = card.Data.shape0.z == 1;
        arr[1, 0] = card.Data.shape1.x == 1;
        arr[1, 1] = card.Data.shape1.y == 1;
        arr[1, 2] = card.Data.shape1.z == 1;
        arr[2, 0] = card.Data.shape2.x == 1;
        arr[2, 1] = card.Data.shape2.y == 1;
        arr[2, 2] = card.Data.shape2.z == 1;

        // If the card is rotated, rotate the matrix

        var rot = card.Rotation;

        for (var i = 0; i < rot; ++i)
        for (var j = 2; j >= 0; --j)
        for (var k = 0; k < 3; ++k)
            arr[k, 2 - j] = arr[j, k];

        return arr;
    }

    public Tile[,] GetTiles(Vector2Int coords)
    {
        var tile = Tile.selected;
        var arr = new Tile[3, 3];

        for (var x = -1; x <= 1; x++)
        for (var y = -1; y <= 1; y++)
        {
            var posX = tile.Position.x + x;
            var posY = tile.Position.y + y;

            if (posX < 0 || posX >= _currentRound.boardTiles.x)
                arr[x + 1, y + 1] = null;
            else if (posY < 0 || posY >= _currentRound.boardTiles.y)
                arr[x + 1, y + 1] = null;
            else
                arr[x + 1, y + 1] = _tiles[posX, posY];
        }

        return arr;
    }
}