using UnityEngine;

public class Star : Collectable
{
    public int starValue = 1;
    public int starParticles = 5;
    public override void Collect()
    {
        CollectableManager.Instance.CollectStars(starValue);
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ParticleEmitter>().EmitCollectStars(starParticles);
    }
}