using UnityEngine;
using UnityEngine.Events;

public class Poolable : MonoBehaviour
{
    private GameObjectPool owner;
    [SerializeField]
    private UnityEvent m_onTakeFromPool = null;
    [SerializeField]
    private UnityEvent m_onReturnToPool = null;

    private bool isOnPool = false;
    internal void Setup(GameObjectPool pool)
    {
        isOnPool = true;
        owner = pool;
    }

    internal void TakeFromPool()
    {
        isOnPool = false;
        gameObject.SetActive(true);
        m_onTakeFromPool.Invoke();
    }

    public void ReturnToPool()
    {
        if (isOnPool) return;
        if (owner == null)
        {
            Destroy(gameObject);
        } else
        {
            isOnPool = true;
            owner.ReceiveInstance(gameObject);
            m_onReturnToPool.Invoke();
            gameObject.SetActive(false);
        }
    }

}
