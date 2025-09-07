using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private Transform _fuelMask;

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
    public void UpdateFuelUI(float currentFule)
    {
        float maxFule = 100; // See FuleManager
        _fuelMask.localScale = new(1, currentFule / maxFule, 1);
    }
}
