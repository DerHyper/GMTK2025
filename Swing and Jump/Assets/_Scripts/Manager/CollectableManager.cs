using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    public static CollectableManager Instance;

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
        // TODO: Fule
    }

    public void CollectMeteor()
    {
        GameData.Instance.AddMeteor();
        // TODO: Lose Fule, Damage
    }
}
