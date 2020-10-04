using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Asteroids/Stage")]
public class StageReference : ScriptableObject
{
    public int InGameObstacles { get; private set; }
    public Action OnRemoveAll;


    public void Add()
    {
        InGameObstacles++;
    }

    public void Remove()
    {
        InGameObstacles--;
        if (InGameObstacles == 0) OnRemoveAll?.Invoke();
    }
}
