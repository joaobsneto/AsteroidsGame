using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    public float PlayerThrust;

    public float PlayerRotation;

    [SerializeField]
    private Vector2 m_forwardDirection = Vector2.up;
    [SerializeField]
    private float m_forceIntesity = 5;
    [SerializeField]
    private float m_rotationSpeed = 2;

    private Rigidbody2D playerRigidbody;
    

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        Vector2 acc = transform.TransformDirection(m_forwardDirection) * Mathf.Clamp01(PlayerThrust) * m_forceIntesity;
        playerRigidbody.AddForce(acc, ForceMode2D.Force);
        float rotationAcc = Mathf.Clamp(PlayerRotation, -1, 1) * -m_rotationSpeed;
        playerRigidbody.AddTorque(rotationAcc);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Entrou");
    }
}
