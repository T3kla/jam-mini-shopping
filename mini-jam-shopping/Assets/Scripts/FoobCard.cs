using System;
using Data;
using UnityEngine;

public class FoobCard : MonoBehaviour
{
    public static FoobCard selected = null;

    public int Rotation
    {
        get => _rotation % 4;
        set => _rotation = value;
    }
    private int _rotation = 400;
    
    private SpriteRenderer _spriteRenderer = null;
    private PolygonCollider2D _polygonCollider = null;

    private Camera _camera;
    [NonSerialized] public FoobData Data = null;
    private float _speed = 0f;
    private bool _followMouse = false;

    private Vector3 _targetPosition = Vector3.zero;
    private Vector3 _targetRotation = Vector3.zero;

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

    private void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Q) && _followMouse)
            Rotation += 1;
        
        if (Input.GetKeyDown(KeyCode.E) && _followMouse)
            Rotation -= 1;
        
        if (Input.GetMouseButtonUp(0) && _followMouse)
            OnMouseUpFake();
    }

    private void OnMouseDown()
    {
        _polygonCollider.enabled = false;
        _spriteRenderer.sortingOrder = 100;
        _followMouse = true;
        selected = this;
    }

    private async void OnMouseUpFake()
    {
        await Awaitable.NextFrameAsync();

        _polygonCollider.enabled = true;
        _spriteRenderer.sortingOrder = 50;
        _followMouse = false;
        selected = null;
    }
    
    public void Init(FoobData data, int rot, float speed)
    {
        Data = data;
        _speed = speed;
        _spriteRenderer.sprite = data.foobSprite;

        _targetPosition = transform.position;

        Rotation = rot;

        Destroy(_polygonCollider);
        _polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
    }
    
    private void Movement()
    {
        if (_targetPosition.y < 6f)
            _targetPosition += Vector3.up * (_speed * Time.fixedDeltaTime);

        _targetRotation = new Vector3(0, 0, 90f * Rotation);
        var angle = Mathf.LerpAngle(transform.eulerAngles.z, _targetRotation.z, 0.1f);
        transform.eulerAngles = new Vector3(0, 0, angle);

        if (_followMouse)
        {
            var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = Vector3.Lerp(transform.position, mousePos, 0.4f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, 0.2f);
        }

        if (transform.position.y > 6f && !_followMouse)
            Destroy(gameObject);
    }
}