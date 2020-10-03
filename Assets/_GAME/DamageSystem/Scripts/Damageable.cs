using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField]
    private int m_initialHealth = 1;
    public int Health { get; private set; }
    [SerializeField]
    private UnityEvent m_onDie = null;
    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        Health = m_initialHealth;
    }



    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    private void LateUpdate()
    {
        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        m_onDie.Invoke();
    }
}
