using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Round", menuName = "=￣ω￣=/Round", order = 1)]
public class RoundData : ScriptableObject
{
    public new string name = null;
    
    public List<MemberData> members = null;
    public List<MemberData> foobs = null;
    
    public float minimumScore = 1000f;
    public float timeLimit = 60f;
    public float beltSpeed = 100f;
    
    [Tooltip("Amount of tiles on X and Y axes.")]
    public Vector2Int boardTiles = new(8, 8);
    [Tooltip("Spacing and scale of the tiles.")]
    public Vector2 boardSize = new(0.9f, 0.8f);
}