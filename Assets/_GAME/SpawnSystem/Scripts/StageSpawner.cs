using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSpawner : MonoBehaviour
{
    public Spawner[] Spawners;

    public Rect Bounds = new Rect();

    private List<GameObject> inGameObstacles;


    private void Start()
    {
        StartStage();
    }
    public void StartStage()
    {
        inGameObstacles = new List<GameObject>();
    }
}
