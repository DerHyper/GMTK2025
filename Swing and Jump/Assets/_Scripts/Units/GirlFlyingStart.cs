using UnityEngine;

public class GirlFlyingStart : MonoBehaviour
{
    public Vector3 StartDirection = new(-1,1);
    public Vector3 StartRotation = new(0,0,1);
    public float StartForce = 10;
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(StartDirection * StartForce, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddTorque(StartRotation, ForceMode.Impulse);
    }
}
