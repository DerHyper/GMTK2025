using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    [SerializeField]
    private GameObject star;
    [SerializeField]
    private Transform spawnPosition;

    // Star Patterns
    [SerializeField]
    private List<GameObject> starPatterns;
    private GrowingNumber starPatternInterval;
    private FixedTimer starPatternTimer = new();
    private float starPatternIntervalMax = 5;
    private float starPatternIntervalMin = 2;
    public float starPatternIntervalGrowth = -0.5f;
    public float starPatternSpawnChance = 0.5f;

    // Meteor Patterns
    [SerializeField]
    private List<GameObject> meteorPatterns;
    private GrowingNumber meteorPatternInterval;
    private FixedTimer meteorPatternTimer = new();
    private float meteorPatternIntervalMax = 9;
    private float meteorPatternIntervalMin = 1;
    public float meteorPatternIntervalGrowth = -0.5f;
    public float meteorPatternSpawnChance = 0.6f;


    private const float UPDATE_INTERVALS_TIME = 1;

    private void Start()
    {
        starPatternTimer.Start();
        starPatternInterval = new(starPatternIntervalMax, starPatternIntervalMin, starPatternIntervalMax, starPatternIntervalGrowth);
        meteorPatternTimer.Start();
        meteorPatternInterval = new(meteorPatternIntervalMax, meteorPatternIntervalMin, meteorPatternIntervalMax, meteorPatternIntervalGrowth);
        InvokeRepeating(nameof(UpdateIntervals), UPDATE_INTERVALS_TIME, UPDATE_INTERVALS_TIME);
    }

    // Update is called once per frame
    void Update()
    {
        if (starPatternTimer.GetTime() >= starPatternInterval.Get())
        {
            TrySpawnPattern(starPatterns, starPatternSpawnChance);
            starPatternTimer.Start();
        }
        if (meteorPatternTimer.GetTime() >= starPatternInterval.Get())
        {
            TrySpawnPattern(meteorPatterns, meteorPatternSpawnChance);
            meteorPatternTimer.Start();
        }
    }


    private void TrySpawnPattern(List<GameObject> objs, float chance)
    {
        if (objs.Count < 1)
        {
            return;
        }
        int randIndex = UnityEngine.Random.Range(0, objs.Count - 1);
        TrySpawnPattern(objs[randIndex], chance);
    }

    private void TrySpawnPattern(GameObject obj, float chance)
    {
        if (!Randomize.PercentChance(chance))
        {
            return;
        }

        Instantiate(obj, spawnPosition.position, spawnPosition.rotation);
    }

    private void UpdateIntervals()
    {
        starPatternInterval.UpdateGrow();
        meteorPatternInterval.UpdateGrow();
    }
}
