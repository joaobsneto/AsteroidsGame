using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class InputPlayerMapperController : MonoBehaviour
{
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire!");
    }

    public void Rotate(InputAction.CallbackContext context)
    {
        playerController.PlayerRotation = context.ReadValue<float>();
    }

    public void Thrust(InputAction.CallbackContext context)
    {

        playerController.PlayerThrust = context.ReadValue<float>();
    }
}
