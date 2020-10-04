using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class InputPlayerMapperController : MonoBehaviour
{
    
    [SerializeField]
    private Spawner m_missileSpawner = null;
    
    [SerializeField]
    private float m_delayToShoot = .5f;

    private PlayerController playerController;
    private float lastFireTime;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (Time.time - lastFireTime > m_delayToShoot)
        {
            m_missileSpawner.Spawn();
            lastFireTime = Time.time;
        }
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
