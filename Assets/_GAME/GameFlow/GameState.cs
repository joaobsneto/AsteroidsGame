using UnityEngine;
[CreateAssetMenu(menuName ="Asteroids/GameState")]
public class GameState : ScriptableObject
{
    [SerializeField]
    private int m_score = 0;
    
    public IntEvent OnChangeScore;
    public int Score
    {
        get => m_score;
        set  {
            if (value != m_score)
            {
                m_score = value;
                OnChangeScore.Invoke(m_score);
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
