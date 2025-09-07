using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text distanceText;
    public TMP_Text starText;
    public TMP_Text meteorText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreLables();
    }

    private void UpdateScoreLables()
    {
        distanceText.text = GameData.Instance.GetDistance() + " km";
        starText.text = GameData.Instance.GetStars().ToString();
        meteorText.text = GameData.Instance.GetMeteor().ToString();
    }
}
