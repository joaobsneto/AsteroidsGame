using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObjectPool m_pool = null;
    [SerializeField]
    private string m_poolTag = "";
    [SerializeField]
    private bool m_setSpawnedRotation = true;
    [SerializeField]
    private Quaternion m_customRotation = Quaternion.identity;

    private Action<GameObject> setupInstance;
    private void Awake()
    {
        setupInstance = new Action<GameObject>(SetUpInstance);
    }

    private void Start()
    {
        TryToSetPoolFromObjectWithTag();
    }

    private void TryToSetPoolFromObjectWithTag()
    {
        if (m_pool != null || string.IsNullOrEmpty(m_poolTag)) return;
        var pool = GameObject.FindGameObjectWithTag(m_poolTag);
        if (pool == null)
        {
            throw new Exception($"No GameObject with tag {m_poolTag} was found.");
        }
        m_pool = pool.GetComponent<GameObjectPool>();
        if (m_pool == null)
        {
            throw new Exception($"GameObject with tag {m_poolTag} does not have a GameObjectPool component.");
        }
    }

    public void Spawn(DamageInfo damageInfo)
    {
        Vector3 lastPosition = transform.position;
        Quaternion lastRotation = transform.rotation;
        transform.position = damageInfo.Contacts[0].point;
        transform.up = damageInfo.Contacts[0].normal;
        CallSpawn();
        transform.position = lastPosition;
        transform.rotation = lastRotation;
    }

    public void Spawn()
    {
        CallSpawn();
    }


    internal GameObject CallSpawn()
    {
        return m_pool.GetInstance(setupInstance);
    }

    private void SetUpInstance(GameObject instance)
    {
        instance.transform.position = transform.position;
        if (m_setSpawnedRotation) instance.transform.rotation = transform.rotation * m_customRotation;
    }
}
