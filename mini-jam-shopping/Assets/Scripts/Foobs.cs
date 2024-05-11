using UnityEngine;

[CreateAssetMenu(fileName = "Foobs", menuName = "Foobs/Foobs", order = 1)]
public class Foobs : ScriptableObject
{
    public new string name = null;
    public Sprite foodSprite = null;
    public Sprite backgroundSprite = null;
}