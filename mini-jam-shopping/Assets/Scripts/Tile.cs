using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public static Tile selected = null;

    [NonSerialized] public Board Board = null;
    [NonSerialized] public Vector2Int Position = Vector2Int.zero;

    [NonSerialized] public SpriteRenderer SpriteRenderer = null;
    [NonSerialized] public PolygonCollider2D PolygonCollider = null;
    
    [NonSerialized] public bool IsOccupied = false;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        selected = null;
    }

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        PolygonCollider = GetComponent<PolygonCollider2D>();
    }

    private async void OnMouseEnter()
    {
        await Awaitable.NextFrameAsync();

        selected = this;

        Board.UpdateTileColors();
    }

    private void OnMouseExit()
    {
        if (selected == this)
            selected = null;
        
        Board.UpdateTileColors();
    }
}