using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Foobs", menuName = "=￣ω￣=/Foob", order = 1)]
    public class FoobData : ScriptableObject
    {
        public new string name = null;
        public Sprite foobSprite = null;
        public Sprite backgroundSprite = null;
    }
}