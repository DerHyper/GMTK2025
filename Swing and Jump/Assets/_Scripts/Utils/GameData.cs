using System;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public float fule = 20;
    private float distance = 0; // KM
    private int stars = 0;
    private int meteors = 0;

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

    public void AddMeteor()
    {
        meteors++;
    }

    public float GetMeteor()
    {
        return meteors;
    }
}
