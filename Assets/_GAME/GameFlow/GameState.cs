using UnityEngine;
[CreateAssetMenu(menuName ="Asteroids/GameState")]
public class GameState : ScriptableObject
{
    public int Points = 0;
    public int ShipsLeft = 3;
    public int InitialNumberOfObstacles = 3;
    public int NumberOfObstaclesIncrement = 4;
}
