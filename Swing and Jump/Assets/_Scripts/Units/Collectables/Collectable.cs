using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    public float speed = 10;

    private void Update() {
        Vector3 nextStep = Vector3.down * Time.deltaTime * speed;
        gameObject.transform.position += nextStep;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Collect();
        }

        Destroy(gameObject);
    }

    public abstract void Collect();
}
