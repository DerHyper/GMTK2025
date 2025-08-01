using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwingManager : MonoBehaviour
{
    [SerializeField] private Transform _swingSeat;
    private const float GOOD_AREA_MODIFIER = 0.9f;
    private const float GOOD_AREA_MIN = 0.1f;
    private const float GOOD_INCREASE = 0.02f;
    private const float GOOD_INCREASE_MODIFIER = 1.05f;
    private const float CRIT_AREA_MODIFIER = 0.95f;
    private const float CRIT_AREA_MIN = 0.05f;
    private const float CRIT_INCREASE = 0.04f;
    private const float CRIT_INCREASE_MODIFIER = 1.1f;
    private const float MIN_SPEED = 2f;
    private const float MAX_SPEED = 10f;
    private const float MIN_HEIGHT = 0.1f;
    private const float MAX_HEIGHT = 1f;
    private const float SLOWDOWN_MODIFIER = 0.95f;
    private const float AIR_DRAG = 0.0005f;
    private const float MAX_ROTATION = 170f;
    [SerializeField] private float _currentSpeed = 0;
    [SerializeField] private float _targetSpeed = 0;
    [SerializeField] private float _currentMaxHeight = 0;
    [SerializeField] private float _targetMaxHeight = 0;
    private FixedTimer _SwingPositionTimer = new();
    private FixedTimer _LerpTimer = new();
    private float _lerpTime = 2f; // Seconds
    private float _swingStep = 0f;

    public float swingPosition = 0;



    private void FixedUpdate()
    {
        if (_SwingPositionTimer.IsRunning())
        {
            _SwingPositionTimer.Update();
            UpdateSwingStep();
            UpdateSwingView();
            LerpToTargetSpeed();
            AddDrag();
        }
    }

    private void AddDrag()
    {
        _targetSpeed = Math.Max(_targetSpeed - AIR_DRAG, MIN_SPEED);
        _targetMaxHeight = Math.Max(_targetMaxHeight - AIR_DRAG/2, MIN_HEIGHT);
    }

    private void UpdateSwingStep()
    {
        float speedModifier = Math.Min(_currentSpeed, MAX_SPEED);
        _swingStep += Time.fixedDeltaTime * speedModifier;
    }

    public void ActionPressed()
    {
        if (IsInRange(GetSwingPosition(), GetCritSwingRange()))
        {
            IncreaseSpeedCrit();
        }
        else if (IsInRange(GetSwingPosition(), GetGoodSwingRange()))
        {
            IncreaseSpeedGood();
        }
        else
        {
            DecreaseSpeed();
        }
    }

    /// <summary>
    /// Starts the _SwingPositionTimer and starts swinging the swing
    /// </summary>
    public void KickStart()
    {
        _SwingPositionTimer.Start();
        _targetSpeed = MIN_SPEED;
        _targetMaxHeight = MIN_HEIGHT;
    }

    private void LerpToTargetSpeed()
    {
        _LerpTimer.Update();
        float currentPercent = _LerpTimer.GetTime() / _lerpTime;
        _currentSpeed = Mathf.Lerp(_currentSpeed, _targetSpeed, currentPercent);
        _currentMaxHeight = Mathf.Lerp(_currentMaxHeight, _targetMaxHeight, currentPercent);
    }

    private void DecreaseSpeed()
    {
        _targetSpeed = Math.Max(_targetSpeed * SLOWDOWN_MODIFIER, MIN_SPEED);
        _targetMaxHeight = Math.Max(_targetMaxHeight * SLOWDOWN_MODIFIER, MIN_HEIGHT);
    }

    private void IncreaseSpeedCrit()
    {
        _LerpTimer.Start();
        _targetSpeed = _targetSpeed * CRIT_INCREASE_MODIFIER + CRIT_INCREASE;
        _targetMaxHeight = _targetMaxHeight * CRIT_INCREASE_MODIFIER + CRIT_INCREASE;
    }

    private void IncreaseSpeedGood()
    {
        _LerpTimer.Start();
        _targetSpeed = _targetSpeed * GOOD_INCREASE_MODIFIER + GOOD_INCREASE;
        _targetMaxHeight = _targetMaxHeight * GOOD_INCREASE_MODIFIER + GOOD_INCREASE;
    }

    private void UpdateSwingView()
    {
        _SwingPositionTimer.Update();
        swingPosition = GetSwingPosition();
        Vector3 newRotation = new(0, 0, swingPosition * MAX_ROTATION);
        _swingSeat.rotation = Quaternion.Euler(newRotation);
    }

    /// <summary>
    /// Get the current swing position between -1 and 1
    /// </summary>
    /// <returns></returns>
    private float GetSwingPosition()
    {
        float clampedHeight = Math.Min(_currentMaxHeight, MAX_HEIGHT);

        float position = (float)(Math.Sin(_swingStep) * clampedHeight);
        return position;
    }

    private bool IsInRange(float swingPosition, (float, float) range)
    {
        (float min, float positiveMin) = range;
        bool inRange = swingPosition < min || swingPosition > -min;
        return inRange;
    }

    private (float min, float positiveMin) GetGoodSwingRange()
    {
        float min = Math.Max(-_currentMaxHeight,-MAX_HEIGHT) * GOOD_AREA_MODIFIER + GOOD_AREA_MIN;
        return (min, 0);
    }

    private (float min, float positiveMin) GetCritSwingRange()
    {
        float min = Math.Max(-_currentMaxHeight, -MAX_HEIGHT) * CRIT_AREA_MODIFIER + CRIT_AREA_MIN;
        return (min, 0);
    }
}
