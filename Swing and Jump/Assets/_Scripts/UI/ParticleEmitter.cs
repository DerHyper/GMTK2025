using UnityEngine;

public class ParticleEmitter : MonoBehaviour
{
    public GameObject swingStarsPrefab;
    public GameObject collectStarsPrefab;
    public Transform target;

    public void EmitParticles(int count, GameObject particlePrefab)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            particle.GetComponent<LerpingParticle>().Init(target);
        }
    }

    public void EmitParticles(int count, GameObject particlePrefab, float speed, float lerpTime)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            particle.GetComponent<LerpingParticle>().Init(target, speed, lerpTime);
        }
    }

    public void EmitSwingStars(int count)
    {
        EmitParticles(count, swingStarsPrefab);
    }

    public void EmitCollectStars(int count)
    {
        EmitParticles(count, swingStarsPrefab, 5, 0.2f);
    }
}
