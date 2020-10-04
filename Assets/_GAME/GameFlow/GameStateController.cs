using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    [SerializeField]
    private GameState m_gameState = null;
    [SerializeField]
    private string InGameSceneName = "InGame";

    public int SelectDifficulty = 1;

    public void StartGame()
    {
        m_gameState.Score = 0;
        m_gameState.ShipsLeft = 5 - SelectDifficulty;
        m_gameState.InitialNumberOfObstacles = 2 * (SelectDifficulty+1);
        m_gameState.NumberOfObstaclesIncrement = SelectDifficulty+1;
        SceneManager.LoadScene(InGameSceneName);
    }

    public void UpdateDifficulty(int newValue)
    {
        SelectDifficulty = newValue;
    }
}
