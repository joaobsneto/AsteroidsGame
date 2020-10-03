using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSpawner : MonoBehaviour
{
    public int NumberOfObstacles;
    public Spawner[] Spawners;

    private List<GameObject> inGameObstacles;

    private ScreenWrapperCameraController cameraController;

    private void Start()
    {
        cameraController = Camera.main.GetComponent<ScreenWrapperCameraController>();
        inGameObstacles = new List<GameObject>();
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
        
        for (int i = 0; i < NumberOfObstacles; i++)
        {
            var spawner = Spawners[Random.Range(0, Spawners.Length)];
            spawner.transform.position = GetPositionInBounds();
            inGameObstacles.Add(spawner.CallSpawn());
        }
        inGameObstacles = new List<GameObject>();
    }
}
