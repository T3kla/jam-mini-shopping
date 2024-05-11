using Data;
using UnityEngine;

public class FoobCard : MonoBehaviour
{
    public static FoobCard selected = null;
    
    private SpriteRenderer _spriteRenderer = null;
    private PolygonCollider2D _polygonCollider = null;

    private Camera _camera;
    private FoobData _data = null;
    private float _speed = 0f;
    private bool _followMouse = false;

    private Vector3 _targetPosition = Vector3.zero;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        selected = null;
    }
    
    private void Awake()
    {
        _camera = Camera.main;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _polygonCollider = GetComponent<PolygonCollider2D>();
    }

    private void OnMouseDown()
    {
        _followMouse = true;
        selected = this;
    }

    private async void OnMouseUp()
    {
        await Awaitable.NextFrameAsync();

        _followMouse = false;
        selected = null;
    }

    public void Init(FoobData data, float speed)
    {
        _data = data;
        _speed = speed;
        _spriteRenderer.sprite = data.foobSprite;

        _targetPosition = transform.position;

        Destroy(_polygonCollider);
        _polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        if (_targetPosition.y < 6f)
            _targetPosition += Vector3.up * (_speed * Time.fixedDeltaTime);

        if (_followMouse)
        {
            var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = Vector3.Lerp(transform.position, mousePos, 0.4f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, 0.4f);
        }
        
        if (transform.position.y > 6f && !_followMouse)
            Destroy(gameObject);
    }
}