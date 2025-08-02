using UnityEngine;

public class Meteor : Collectable
{
    public override void Collect()
    {
        CollectableManager.Instance.CollectMeteor();
    }
}
