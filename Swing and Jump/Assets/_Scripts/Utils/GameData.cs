using System;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public float fule;
    private float distance; // KM
    private int stars;

    public static GameData Instance;

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

    private void Start() {
        ResetData();
    }

    private void ResetData()
    {
        fule = 20;
        distance = 0;
        stars = 0;
    }

    public void SetFule(float fule)
    {
        this.fule = fule;
    }

    public void AddFule(float fule)
    {
        this.fule += fule;
    }

    public float GetFule()
    {
        return fule;
    }

    public void AddDistance(float distance)
    {
        this.distance += distance;
    }

    public float GetDistance()
    {
        return distance;
    }

    public void AddStars(int stars)
    {
        this.stars += stars;
    }

    public float GetStars()
    {
        return stars;
    }
}
