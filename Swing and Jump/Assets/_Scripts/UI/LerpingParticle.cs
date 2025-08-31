using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This class is for particles that lerp to a target position and then destroy themselves. 
/// It is used for stars that increase the flight length.
/// </summary>
public class LerpingParticle : MonoBehaviour
{
    public Sprite[] sprites;
    private bool _isLerping = false;
    private const float STD_SPEED = 3f;
    private float _speed;
    private Vector2 _startDirection = Vector2.zero;
    private Vector2 _currentDirection = Vector2.zero;
    private Transform _target;
    private FixedTimer timer = new();
    private float lerpTime; // Seconds
    private const float STD_LERPTIME = 0.5f; // Seconds
    private const float STD_DESTROY_DISTANCE = 0.1f;

    public void Init(Transform _target, float _speed = STD_SPEED, float lerpTime = STD_LERPTIME)
    {
        this._target = _target;
        this._speed = _speed * UnityEngine.Random.Range(0.8f, 1.2f);
        this.lerpTime = lerpTime * UnityEngine.Random.Range(0.8f, 1.2f);
        _startDirection = UnityEngine.Random.insideUnitCircle.normalized;
        _currentDirection = _startDirection;
        timer.Start();
        GetComponent<SpriteRenderer>().sprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];
        _isLerping = true;
    }

    // Update is called once per frame
    public void Update()
    {
        if (_isLerping)
        {
            LerpDirection();
            Move();
        }
        if (IsNearTarget())
        {
            Destroy(gameObject);
        }
    }

    private bool IsNearTarget()
    {
        if (_target == null) return true;
        return Vector2.Distance(transform.position, _target.position) < STD_DESTROY_DISTANCE;
    }

    private void Move()
    {
        transform.position += (Vector3)_currentDirection * _speed * Time.deltaTime;
    }

    /// <summary>
    /// Lerps _currentDirection to the direction of the target.
    /// </summary>
    private void LerpDirection()
    {
        _currentDirection = Vector2.Lerp(_startDirection, ((Vector2)_target.position - (Vector2)transform.position).normalized, timer.GetTime() / lerpTime).normalized;
    }
}
