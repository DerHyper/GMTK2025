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
            swingManager.ActionPressed();
        }
    }
}