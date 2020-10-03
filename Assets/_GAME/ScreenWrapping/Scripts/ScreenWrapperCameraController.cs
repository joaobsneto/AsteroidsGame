using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]
public class ScreenWrapperCameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_childCameraPrefab = null;

    private Camera rightCamera;
    private Camera bottomCamera;
    private Camera rightBottomCamera;

    private Camera selfCamera;
    public float Aspect { get; private set; }

    public float OrthographicSizeOnX { get; private set; }

    public float OrthographicSizeOnY { get; private set; }

    public float TopBound { get; private set; }
    public float BottomBound { get; private set; }
    
    public float RightBound { get; private set; }
    public float LeftBound { get; private set; }

    public float CameraZ { get; private set; }

    void Awake()
    {
        selfCamera = GetComponent<Camera>();
        
        UpdateCameraValues();

        rightCamera = Instantiate(m_childCameraPrefab, new Vector3(transform.position.x + 2* OrthographicSizeOnX,0, CameraZ), 
            Quaternion.identity, transform).GetComponent<Camera>();
        rightCamera.depth = 1;
        rightCamera.orthographicSize = selfCamera.orthographicSize;

        bottomCamera = Instantiate(m_childCameraPrefab, new Vector3(0, transform.position.y - 2 * OrthographicSizeOnY, CameraZ), 
            Quaternion.identity, transform).GetComponent<Camera>();
        bottomCamera.depth = 2;
        bottomCamera.orthographicSize = selfCamera.orthographicSize;

        rightBottomCamera = Instantiate(m_childCameraPrefab, new Vector3(transform.position.x + 2 * OrthographicSizeOnX, transform.position.y -  2*OrthographicSizeOnY, CameraZ), 
            Quaternion.identity, transform).GetComponent<Camera>();
        rightBottomCamera.depth = 3;
        rightBottomCamera.orthographicSize = selfCamera.orthographicSize;
    }

    private void UpdateCameraValues()
    {
        CameraZ = transform.position.z;
        Aspect = ((float)Screen.width / Screen.height);
        OrthographicSizeOnY = selfCamera.orthographicSize;
        TopBound = selfCamera.transform.position.y + OrthographicSizeOnY;
        BottomBound = selfCamera.transform.position.y - OrthographicSizeOnY;
        OrthographicSizeOnX = selfCamera.orthographicSize * Aspect;
        RightBound = selfCamera.transform.position.x + OrthographicSizeOnX;
        LeftBound = selfCamera.transform.position.x - OrthographicSizeOnX;
    }
}
