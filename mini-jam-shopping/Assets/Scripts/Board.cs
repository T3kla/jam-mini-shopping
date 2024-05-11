using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject tilePrefab = null;
    public int width = 8;
    public int height = 8;
    public float tileSize = 1.0f;
    public float tileScale = 1.0f;

    private GameObject[,] tiles = null;
    
    private void Awake()
    {
        tiles = new GameObject[width, height];
        
        for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                var pos = new Vector3(x * tileSize - ((width-1) * tileSize)/2, y * tileSize - ((height - 1) * tileSize)/2, 0);
                var tile = Instantiate(tilePrefab, pos, Quaternion.identity);
                tile.transform.localScale = new Vector3(tileScale, tileScale, tileScale);
                tile.transform.parent = transform;
                tiles[x, y] = tile;
            }
    }
    
}
