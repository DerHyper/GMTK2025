using System;
using TMPro;
using UnityEngine;

public class DistanceManager : MonoBehaviour
{
    public float totalDistance;
    public float goalDistance;
    public float distancePerSecond;
    public static DistanceManager Instance;
    public TMP_Text distanceUI;
    public bool goalReached = false;
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
            goalReached = true;
            GameManager.Instance.StartGameEnd();
        }
    }

    private void UpdateDistanceUI()
    {
        int percentDone = (int) Math.Round(totalDistance / goalDistance * 100);
        int clampedPercentDone = Math.Min(percentDone, 100);
        distanceUI.text = clampedPercentDone + "%";
    }
}
