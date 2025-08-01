using UnityEngine;

public class GirlFlyingStart : MonoBehaviour
{
    public Vector3 direction = new(-1, 1, 0);
    public Vector3 torque = new(0, 0, 1);
    public float directionForce = 10;
    
    private void Update() {
        Vector3 currentPos = gameObject.transform.position;
        Vector3 nextPos = currentPos + direction * Time.deltaTime * directionForce;

        Vector3 currentRot = gameObject.transform.rotation.eulerAngles;
        Vector3 nextRot = currentRot + torque * Time.deltaTime;

        gameObject.transform.SetPositionAndRotation(nextPos, Quaternion.Euler(nextRot));
    }
}
