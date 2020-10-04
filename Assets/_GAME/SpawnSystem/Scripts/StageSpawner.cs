using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSpawner : MonoBehaviour
{
    [SerializeField]
    private GameState m_gameState = null;
    public int NumberOfObstacles;
    public int NumberOfObstaclesIncrement;
    public Spawner[] Spawners;
    [SerializeField]
    private StageReference m_stage = null;
    [SerializeField]
    private float m_delayToCreateAsteroids = 1;

    private ScreenWrapperCameraController cameraController;

    private void Start()
    {
        isExiting = false;
        cameraController = Camera.main.GetComponent<ScreenWrapperCameraController>();
        m_stage.OnRemoveAll += OnClearStage;
        if (m_gameState != null)
        {
            NumberOfObstacles = m_gameState.InitialNumberOfObstacles;
            NumberOfObstaclesIncrement = m_gameState.NumberOfObstaclesIncrement;
        }
        StartStage();

    }
    private bool isExiting = false;
    private void OnApplicationQuit()
    {
        isExiting = true;
    }

    private void OnDestroy()
    {
        isExiting = true;
    }

    private void OnClearStage()
    {
        if (isExiting) return;
        NumberOfObstacles += NumberOfObstaclesIncrement;
        StartStage();
    }

    private Vector3 GetPositionInBounds()
    {
        int corner = Random.Range(0, 4);
        Vector3 position;
        switch (corner)
        {
            case 0:
                position = new Vector3(cameraController.OrthographicSizeOnX, Random.Range(-cameraController.OrthographicSizeOnY, cameraController.OrthographicSizeOnY));
                break;
            case 1:
                position = new Vector3(Random.Range(-cameraController.OrthographicSizeOnX, cameraController.OrthographicSizeOnX), -cameraController.OrthographicSizeOnY);
                break;
            case 2:
                position = new Vector3(-cameraController.OrthographicSizeOnX, Random.Range(-cameraController.OrthographicSizeOnY, cameraController.OrthographicSizeOnY));
                break;
            default:
                position = new Vector3(Random.Range(-cameraController.OrthographicSizeOnX, cameraController.OrthographicSizeOnX), cameraController.OrthographicSizeOnY);
                break;

        }
        return position;
    }

    public void StartStage()
    {
        StartCoroutine(StartStageCoroutine());
    }

    private IEnumerator StartStageCoroutine()
    {
        yield return new WaitForSeconds(m_delayToCreateAsteroids);
        m_stage.ResetInGameObstacles();
        for (int i = 0; i < NumberOfObstacles; i++)
        {
            var spawner = Spawners[Random.Range(0, Spawners.Length)];
            spawner.transform.position = GetPositionInBounds();
            spawner.Spawn();
        }
    }
}
