using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Foobs", menuName = "=￣ω￣=/Foob", order = 1)]
    public class FoobData : ScriptableObject
    {
        public new string name = null;
        public Sprite foobSprite = null;
        public Sprite shapeSprite = null;
        public Vector3Int shape0 = Vector3Int.zero;
        public Vector3Int shape1 = Vector3Int.zero;
        public Vector3Int shape2 = Vector3Int.zero;
    }
}