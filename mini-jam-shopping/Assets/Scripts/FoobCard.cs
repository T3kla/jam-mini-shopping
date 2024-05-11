using Data;
using UnityEngine;

public class FoobCard : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer = null;
    
    private FoobData _data = null;
    private float _speed = 0f;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();        
    }

    public void Init(FoobData data, float speed)
    {
        Debug.Log($"FoobCard: {data.name}");
        
        _data = data;
        _speed = speed;
        _spriteRenderer.sprite = data.foobSprite;
    }

    private void FixedUpdate()
    {
        transform.position += Vector3.up * (_speed * Time.fixedDeltaTime);
        
        if (transform.position.y > 6f)
            Destroy(gameObject);
    }
}
