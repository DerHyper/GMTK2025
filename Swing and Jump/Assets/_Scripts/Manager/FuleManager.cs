using System;
using UnityEngine;
using UnityEngine.Rendering;

public class FuleManager : MonoBehaviour
{
    public float fule = 0;
    private float maxFule = 100;
    public float fuleConsume = 1;
    public Transform fuleMask;
    public static FuleManager Instance;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fule = GameData.Instance.GetFule();
    }

    private void Update()
    {
        AddFule(-fuleConsume * Time.deltaTime);
        UpdateFuleMask();
        if (fule <= 0)
        {
            GameManager.Instance.StopFlying();
        }
    }

    private void UpdateFuleMask()
    {
        fuleMask.localScale = new(1, fule / maxFule, 1);
    }

    public void AddFule(float amount)
    {
        fule = Math.Min(maxFule, Math.Max(0, fule + amount));
    }
}
