using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InGameUIController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_pointsLabel = null;

    [SerializeField]
    private GameObject[] m_shipsIcon = null;

    [SerializeField]
    private GameState m_gameState = null;

    [SerializeField]
    private GameObject m_gameOverUI = null;
    [SerializeField]
    private PlayerInput m_playerInput = null;
    [SerializeField]
    private string m_mainMenuSceneName = "Menu";
    [SerializeField]
    private float m_minimumTimeInGameOverScreen = 2;

    private void OnEnable()
    {
        OnChangePoints(m_gameState.Score);
        OnChangeShipsLeft(m_gameState.ShipsLeft);
        m_gameState.OnChangeScore.AddListener(OnChangePoints);
        m_gameState.OnChangeShipsLeft.AddListener(OnChangeShipsLeft);
    }

    private void OnDisable()
    {
        m_gameState.OnChangeScore.RemoveListener(OnChangePoints);
        m_gameState.OnChangeShipsLeft.RemoveListener(OnChangeShipsLeft);
    }

    private void OnChangeShipsLeft(int shipsLeft)
    {
        for (int i = 0; i < m_shipsIcon.Length; i++)
        {
            m_shipsIcon[i].SetActive(i < shipsLeft);
        }
        if (shipsLeft < 0)
        {
            OnGameOver();
        }
    }

    private void OnChangePoints(int points)
    {
        m_pointsLabel.text = points.ToString();
    }
    private float timePlayerDied;
    private void OnGameOver()
    {
        m_gameOverUI.SetActive(true);
        m_playerInput.enabled = true;
        timePlayerDied = Time.time;
    }

    public void EndGameCall(InputAction.CallbackContext context)
    {
        if (!m_gameOverUI.activeSelf || !context.performed || Time.time - timePlayerDied < m_minimumTimeInGameOverScreen) return;
        SceneManager.LoadScene(m_mainMenuSceneName);
    }
}
