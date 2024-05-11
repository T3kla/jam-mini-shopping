using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Member", menuName = "=￣ω￣=/Member", order = 1)]
    public class MemberData : ScriptableObject
    {
        public new string name = null;
        public Sprite sprite = null;
        public List<FoobData> needs = null;
    }
}