using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        BeforeSwinging,
        Swinging,
        Flying
    }

    public static GameManager Instance;
    public GameState state;

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

    public void ChangeState(GameState newState)
    {
        state = newState;
    }
}