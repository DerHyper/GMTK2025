using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    public float speed = 10;

    private void Update() {
        Vector3 nextStep = Vector3.down * Time.deltaTime * speed;
        gameObject.transform.position += nextStep;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Collect();
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "DestroyCollectables")
        {
            Destroy(gameObject);
        }
    }

    public abstract void Collect();
}
