using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private SwingManager swingManager;

    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (GameManager.Instance.state)
            {
                case GameManager.GameState.BeforeSwinging:
                    swingManager.KickStart();
                    GameManager.Instance.ChangeState(GameManager.GameState.Swinging);
                    break;
                case GameManager.GameState.Swinging:
                    swingManager.ActionPressed();
                    break;
                default:
                    break;
            }
        }
    }
}