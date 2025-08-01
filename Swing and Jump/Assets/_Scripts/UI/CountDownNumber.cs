using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CountDownNumber : MonoBehaviour
{
    public void Init(int number)
    {
        TMP_Text[] texte = GetComponentsInChildren<TMP_Text>();
        foreach (var text in texte)
        {
            text.text = number.ToString();
        }
        Animation animation = GetComponent<Animation>();
        animation.Play();
        Invoke(nameof(SelfDestrory), 1.5f);
    }

    private void SelfDestrory()
    {
        
    }
}
