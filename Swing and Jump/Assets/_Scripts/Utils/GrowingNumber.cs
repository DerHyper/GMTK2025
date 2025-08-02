using System;

public class GrowingNumber
{
    private float current;
    private float min;
    private float max;
    public float growAmount;

    public GrowingNumber(float current, float min, float max, float growAmount)
    {
        this.current = current;
        this.min = min;
        this.max = max;
        this.growAmount = growAmount;
    }

    public float Get()
    {
        return current;
    }

    public void UpdateGrow()
    {
        current = Math.Max(Math.Min(current - growAmount, max), min);
    }
}