using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeCounter : MonoBehaviour
{
    [SerializeField]
    private float m_time = 2;
    [SerializeField]
    private Vector2 m_timeVariance = Vector2.zero;
    [SerializeField]
    private UnityEvent m_onCompleteTimer = null;
    [SerializeField]
    private bool m_resetOnComplete = false;

    private float timeLeft;

    private void OnEnable()
    {
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            m_onCompleteTimer.Invoke();
            if (m_resetOnComplete)
            {
                ResetTimer();
            } else
            {
                enabled = false;
            }
        }
        
    }

    private void ResetTimer()
    {
        timeLeft = m_time + Random.Range(m_timeVariance.x, m_timeVariance.y);
    }
}
