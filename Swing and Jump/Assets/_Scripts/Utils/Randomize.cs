

using UnityEngine;

public static class Randomize
{

    /// <summary>
    /// Randomnes given in %
    /// </summary>
    /// <param name="percent">Chance of True, must be between 0 and 1</param>
    /// <returns>True/False</returns>
    public static bool PercentChance(float percent)
    {
        float pick = Random.Range(0f, 1f);
        return pick <= percent;
    }
}