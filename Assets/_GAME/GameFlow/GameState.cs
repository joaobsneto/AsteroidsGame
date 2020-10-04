using UnityEngine;
[CreateAssetMenu(menuName ="Asteroids/GameState")]
public class GameState : ScriptableObject
{
    [SerializeField]
    private int m_points = 0;
    
    public IntEvent OnChangePoints;
    public int Points
    {
        get => m_points;
        set  {
            if (value != m_points)
            {
                m_points = value;
                OnChangePoints.Invoke(m_points);
            }
        }
    }

    [SerializeField]
    private int m_shipsLeft = 3;

    public IntEvent OnChangeShipsLeft;
    public int ShipsLeft
    {
        get => m_shipsLeft;
        set
        {
            if (value != m_shipsLeft)
            {
                m_shipsLeft = value;
                OnChangeShipsLeft.Invoke(m_shipsLeft);
            }
        }
    }

    public int InitialNumberOfObstacles = 3;
    public int NumberOfObstaclesIncrement = 4;
}
