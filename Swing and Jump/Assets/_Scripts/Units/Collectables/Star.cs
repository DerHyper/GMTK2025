using UnityEngine;

public class Star : Collectable
{
    public override void Collect()
    {
        CollectableManager.Instance.CollectStars(1);
    }
}