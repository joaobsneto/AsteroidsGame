using TMPro;
using UnityEngine;

public class InGameUIController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_pointsLabel = null;

    [SerializeField]
    private GameObject[] m_shipsIcon = null;

    [SerializeField]
    private GameState m_gameState = null;

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
    }

    private void OnChangePoints(int points)
    {
        m_pointsLabel.text = points.ToString();
    }
}
