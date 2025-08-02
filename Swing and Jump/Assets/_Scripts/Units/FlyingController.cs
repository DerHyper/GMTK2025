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

    private void Update()
    {
        MoveToMousePosition();
    }

    private void MoveToMousePosition()
    {

        Vector3 current = gameObject.transform.position;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 target = mousePosition + _offsetFromCamera;

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

    private bool IsInMovementDeadzone(Vector3 mouse, Vector3 caracter)
    {
        bool inDeadzoneX = Math.Abs(mouse.x - caracter.x) < _deadZoneMovement;
        bool inDeadzoneY = Math.Abs(mouse.y - caracter.y) < _deadZoneMovement;
        return inDeadzoneX && inDeadzoneY;
    }
}
