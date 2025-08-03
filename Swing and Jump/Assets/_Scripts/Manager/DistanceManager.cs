using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistanceManager : MonoBehaviour
{
    public float totalDistance;
    public float totalDistanceLastFrame;
    public float goalDistance;
    public float distancePerSecond;
    public static DistanceManager Instance;
    public TMP_Text distanceUI;
    public bool goalReached = false;
    public List<Seight> seights;
    public Transform SeightSpawn;
    public Transform worldMapMask;
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
    void Start()
    {
        totalDistance = GameData.Instance.GetDistance();
        totalDistanceLastFrame = totalDistance;
    }

    private void Update()
    {

        if (!goalReached && totalDistance <= goalDistance)
        {
            totalDistance += distancePerSecond * Time.deltaTime;
            UpdateDistanceUI();
        }

        if (!goalReached && totalDistance >= goalDistance)
        {
            ReachGoal();
        }

        foreach (var seight in seights)
        {
            if (totalDistanceLastFrame < seight.distance && totalDistance > seight.distance)
            {
                SeightDistanceReached(seight);
            }
        }

        totalDistanceLastFrame = totalDistance;
    }

    private void ReachGoal()
    {
        goalReached = true;
        GameManager.Instance.StartGameEnd();
    }

    private void SeightDistanceReached(Seight seight)
    {
        Instantiate(seight.prefab, SeightSpawn.position, Quaternion.identity);
    }

    private void UpdateDistanceUI()
    {
        // Text
        int percentDone = (int)Math.Round(totalDistance / goalDistance * 100);
        int clampedPercentDone = Math.Min(percentDone, 100);
        distanceUI.text = clampedPercentDone + "%";

        // Mask
        worldMapMask.localScale = new(1, totalDistance / goalDistance, 1);
    }
    
}
