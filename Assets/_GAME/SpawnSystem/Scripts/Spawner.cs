using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObjectPool m_pool = null;
    [SerializeField]
    private bool m_setSpawnedRotation = true;
    [SerializeField]
    private Quaternion m_customRotation = Quaternion.identity;

    private Action<GameObject> setupInstance;
    private void Awake()
    {
        setupInstance = new Action<GameObject>(SetUpInstance);
    }
    public void Spawn()
    {
        m_pool.GetInstance(setupInstance);
    }

    private void SetUpInstance(GameObject instance)
    {
        instance.transform.position = transform.position;
        if (m_setSpawnedRotation) instance.transform.rotation = transform.rotation * m_customRotation;
    }
}
