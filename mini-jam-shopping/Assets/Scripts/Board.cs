using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject tilePrefab = null;
    public List<RoundData> rounds = null;

    private GameObject[,] _tiles = null;

    private void Awake()
    {
        LoadRound(0);
    }

    private void LoadRound(int round)
    {
        if (rounds.Count <= round) return;

        if (_tiles != null)
            foreach (var tile in _tiles)
                Destroy(tile);

        var data = rounds[round];

        var width = data.boardTiles.x;
        var height = data.boardTiles.y;
        var tileSize = data.boardSize.x;
        var tileScale = data.boardSize.y;

        _tiles = new GameObject[width, height];

        for (var x = 0; x < width; x++)
        for (var y = 0; y < height; y++)
        {
            var posX = x * tileSize - (width - 1) * tileSize / 2;
            var posY = y * tileSize - (height - 1) * tileSize / 2;
            var pos = new Vector3(posX, posY, 0);
            var tile = Instantiate(tilePrefab, pos, Quaternion.identity);
            tile.transform.localScale = new Vector3(tileScale, tileScale, tileScale);
            tile.transform.parent = transform;
            _tiles[x, y] = tile;
        }
    }
}