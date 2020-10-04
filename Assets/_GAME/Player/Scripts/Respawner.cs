using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Damageable)), RequireComponent(typeof(Rigidbody2D))]
public class Respawner : MonoBehaviour
{
    private Vector3 initialPosition;
    private Damageable damageable;
    [SerializeField]
    private float m_blinkDuration = 2;
    [SerializeField]
    private GameState m_gameState = null;
    [SerializeField]
    private Animator m_viewAnimator = null;
    private Rigidbody2D characterRigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        damageable = GetComponent<Damageable>();
        characterRigidbody = GetComponent<Rigidbody2D>();
    }

    public void OnDie()
    {
        if (m_gameState.ShipsLeft > 0)
        {
            m_gameState.ShipsLeft--;
            transform.position = initialPosition;
            damageable.ResetHealth();
            damageable.IsInvulnerable = true;
            m_viewAnimator.SetBool("IsToBlink", true);
            StartCoroutine(RemoveInvulnerability());
            characterRigidbody.velocity = Vector2.zero;
            characterRigidbody.angularVelocity = 0;
        } else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator RemoveInvulnerability()
    {
        yield return new WaitForSeconds(m_blinkDuration);
        damageable.IsInvulnerable = false;
        m_viewAnimator.SetBool("IsToBlink", false);
    }
}
