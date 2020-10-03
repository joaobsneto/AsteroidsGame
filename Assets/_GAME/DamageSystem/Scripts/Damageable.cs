﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField]
    private int m_initialHealth = 1;
    public int Health { get; private set; }
    [SerializeField]
    private DamageInfoEvent m_onDie = null;
    [SerializeField]
    private DamageInfoEvent m_onTakeDamage = null;
    private bool wasAlive;
    private DamageInfo lastDamage;

    public bool IsDead => Health <= 0;
    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        Health = m_initialHealth;
        wasAlive = true;
    }



    public void TakeDamage(DamageInfo damageInfo)
    {
        if (IsDead) return;
        lastDamage = damageInfo;
        m_onTakeDamage.Invoke(damageInfo);
        Health -= damageInfo.DamageValue;
    }

    private void LateUpdate()
    {
        if (IsDead && wasAlive)
        {
            wasAlive = false;
            Die();
        }
    }

    private void Die()
    {
        m_onDie.Invoke(lastDamage);
    }
}
