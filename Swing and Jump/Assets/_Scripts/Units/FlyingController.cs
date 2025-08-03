using System;
using UnityEngine;

public class FlyingController : MonoBehaviour
{
    [SerializeField]
    private float currentFlyingSpeed;
    [SerializeField]
    private float flyingSpeedDistanceModifier = 1;
    private Vector3 _offsetFromCamera = new(0, 0, 1);
    private float _deadZoneMovement = 0.01f;
    private Vector2 _playZone = new(5, 3);
    private bool canFly = true;

    private void Update()
    {
        if (canFly)
        {
            MoveToMousePosition();
        }
        else
        {
            gameObject.transform.position += new Vector3(0, -1*Time.deltaTime, 0);
        }
    }

    private void MoveToMousePosition()
    {

        Vector3 current = gameObject.transform.position;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 target = mousePosition + _offsetFromCamera;

        target = ClapToPlayZone(target);
        if (IsInMovementDeadzone(current, target))
        {
            return;
        }

        Vector3 distance = target - current;
        Vector3 minNextStep = distance.normalized * Time.deltaTime * currentFlyingSpeed;
        float distanceModifier = 1 + distance.magnitude * flyingSpeedDistanceModifier;
        Vector3 nextStep = minNextStep * distanceModifier;

        //gameObject.GetComponent<Rigidbody>().AddForce(nextStep, ForceMode.VelocityChange);
        gameObject.transform.position += nextStep;
    }

    private Vector3 ClapToPlayZone(Vector3 target)
    {
        if (IsInPlayzone(target))
        {
            return target;
        }

        float newX = Math.Min(_playZone.x, Math.Max(-_playZone.x, target.x));
        float newY = Math.Min(_playZone.y, Math.Max(-_playZone.y, target.y));
        Vector3 newTarget = new(newX, newY, target.z);

        return newTarget;
    }

    private bool IsInMovementDeadzone(Vector3 caracter, Vector3 mouse)
    {
        bool inDeadzoneX = Math.Abs(mouse.x - caracter.x) < _deadZoneMovement;
        bool inDeadzoneY = Math.Abs(mouse.y - caracter.y) < _deadZoneMovement;
        return inDeadzoneX && inDeadzoneY;
    }

    private bool IsInPlayzone(Vector3 target)
    {
        bool inX = Math.Abs(target.x) < _playZone.x;
        bool inY = Math.Abs(target.y) < _playZone.y;
        return inX && inY;
    }

    internal void StopFlying()
    {
        canFly = false;
    }
}
