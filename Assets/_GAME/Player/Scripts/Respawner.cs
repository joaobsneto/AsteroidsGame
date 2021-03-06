﻿using System;
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
    [SerializeField]
    private float m_timeToRespawn = .5f;
    private Rigidbody2D characterRigidbody;
    [SerializeField]
    private GameObject m_view = null;
    
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        damageable = GetComponent<Damageable>();
        characterRigidbody = GetComponent<Rigidbody2D>();
    }

    public void OnDie()
    {
        m_gameState.ShipsLeft--;
        if (m_gameState.ShipsLeft >= 0)
        {
            StartCoroutine(RespawnCoroutine());
            m_view.SetActive(false);
        } else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(m_timeToRespawn);
        transform.position = initialPosition;
        damageable.ResetHealth();
        damageable.IsInvulnerable = true;
        characterRigidbody.velocity = Vector2.zero;
        characterRigidbody.angularVelocity = 0;
        m_view.SetActive(true);
        m_viewAnimator.SetBool("IsToBlink", true);
        yield return new WaitForSeconds(m_blinkDuration);
        damageable.IsInvulnerable = false;
        m_viewAnimator.SetBool("IsToBlink", false);
    }
}
