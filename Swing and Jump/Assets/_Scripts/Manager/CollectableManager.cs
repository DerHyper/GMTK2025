using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    public static CollectableManager Instance;
    public float MeteorHitFuleLoss = 10;
    public float StarHitFuleGain = 0.1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void CollectStars(int stars)
    {
        GameData.Instance.AddStars(stars);
        FuleManager.Instance.AddFule(stars*StarHitFuleGain);
    }

    public void CollectMeteor()
    {
        GameData.Instance.AddMeteor();
        FuleManager.Instance.AddFule(-MeteorHitFuleLoss);
    }
}
