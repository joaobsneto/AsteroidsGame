using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAsteroidSpawnController : MonoBehaviour
{
    [SerializeField]
    private Spawner[] m_spawners = null;
    
    [SerializeField]
    private Vector2Int SpawnAmount = new Vector2Int(2,3);

    public void Spawn(DamageInfo damageInfo)
    {
        int count = Random.Range(SpawnAmount.x, SpawnAmount.y);
        for (int i = 0; i < count; i++)
        {
            var spawner = m_spawners[Random.Range(0, m_spawners.Length)];
            spawner.Spawn();
        }
    }
}
