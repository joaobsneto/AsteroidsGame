using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeCounter : MonoBehaviour
{
    [SerializeField]
    private float m_time = 2;
    [SerializeField]
    private UnityEvent m_onCompleteTimer = null;

    private float timeLeft;

    private void OnEnable()
    {
        timeLeft = m_time;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft <= 0)
        {
            m_onCompleteTimer.Invoke();
            enabled = false;
        }
        timeLeft -= Time.deltaTime;
    }
}
