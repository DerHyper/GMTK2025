using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private CountDownManager countDownManager;
    [SerializeField]
    private SwingManager swingManager;
    [SerializeField]
    private GameObject girlSittingSprite;
    [SerializeField]
    private GameObject girlStartingPrefab;

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

        if (newState == GameState.Swinging)
        {
            countDownManager.StartCountDown();
        }
    }

    internal void startFlying()
    {
        girlSittingSprite.SetActive(false);
        GameObject.Instantiate(girlStartingPrefab, girlSittingSprite.transform.position, girlSittingSprite.transform.rotation);
        swingManager.StartAfterJump();
    }
}