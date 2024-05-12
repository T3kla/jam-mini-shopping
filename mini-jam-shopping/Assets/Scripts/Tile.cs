using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public static Tile selected = null;

    [NonSerialized] public Board Board = null;
    [NonSerialized] public Vector2Int Position = Vector2Int.zero;

    [NonSerialized] public SpriteRenderer SpriteRenderer = null;
    [NonSerialized] public PolygonCollider2D PolygonCollider = null;

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

    private void OnMouseEnter()
    {
        Board.UpdateTileColors();

        selected = this;
    }

    private void OnMouseExit()
    {
        Board.UpdateTileColors();

        if (selected == this)
            selected = null;
    }
}