using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    public GameObject Prefab;

    private Queue<GameObject> availableInstances;
    // Start is called before the first frame update
    void Awake()
    {
        availableInstances = new Queue<GameObject>();
    }

    public GameObject GetInstance(Action<GameObject> beforeTakingFromPool = null)
    {
        GameObject fromPool;
        if (availableInstances.Count == 0)
        {
            fromPool = Instantiate(Prefab);
            fromPool.GetComponent<Poolable>().Setup(this);
        } else
        {
            fromPool = availableInstances.Dequeue();
        }
        if (beforeTakingFromPool != null) beforeTakingFromPool.Invoke(fromPool);
        fromPool.GetComponent<Poolable>().TakeFromPool();
        return fromPool;
    }

    internal void ReceiveInstance(GameObject gameObject)
    {
        availableInstances.Enqueue(gameObject);
    }
}
