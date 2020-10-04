using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Asteroids/Stage")]
public class StageReference : ScriptableObject
{
    private HashSet<GameObject> inGameObstaclesSet;
    public int InGameObstacles => inGameObstaclesSet.Count;
    public Action OnRemoveAll;


    public void Add(GameObject newObstacle)
    {
        if (!inGameObstaclesSet.Add(newObstacle))
        {
            Debug.LogException(new Exception($"Trying to add same game object twice: {newObstacle.name}"), newObstacle);
        }
    }

    public void Remove(GameObject existingObstacle)
    {
        if (!inGameObstaclesSet.Remove(existingObstacle))
        {
            Debug.LogException(new Exception($"Trying to remove a game object that is not contained: {existingObstacle.name}"), existingObstacle);
        }
        if (InGameObstacles == 0) OnRemoveAll?.Invoke();
    }

    internal void ResetInGameObstacles()
    {
        inGameObstaclesSet = new HashSet<GameObject>();
    }
}
