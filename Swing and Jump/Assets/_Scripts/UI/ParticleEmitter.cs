using UnityEngine;

public class ParticleEmitter : MonoBehaviour
{
    public GameObject particlePrefab;
    public Transform target;

    public void EmitParticles(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            particle.GetComponent<LerpingParticle>().Init(target);
        }
    }
}
