using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreGiver : MonoBehaviour
{
    [SerializeField]
    private GameState m_gameState = null;
    [SerializeField]
    private int m_score = 10;

    public void GiveScore()
    {
        m_gameState.Score += m_score;
    }


}
