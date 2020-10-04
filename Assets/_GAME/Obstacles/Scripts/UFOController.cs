using UnityEngine;

[RequireComponent(typeof(Poolable))]
public class UFOController : MonoBehaviour
{
    private float m_offToHide = 2;
    private Poolable poolable;
    private ScreenWrapperCameraController screenWrapperController;
    // Start is called before the first frame update
    void Start()
    {
        screenWrapperController = Camera.main.GetComponent<ScreenWrapperCameraController>();
        poolable = GetComponent<Poolable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x - m_offToHide > screenWrapperController.RightBound)
        {
            poolable.ReturnToPool();
        }
    }
}
