using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    public Vector2 boundingBox;
    private Rigidbody2D targetRigidbody;
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boundingBox);
    }
    private Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
        targetRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //camera
        float aspect = ((float)Screen.width / Screen.height);
        float topBound = mainCamera.transform.position.y + mainCamera.orthographicSize;
        float bottomBound = mainCamera.transform.position.y - mainCamera.orthographicSize;
        float orthographicSizeOnX = mainCamera.orthographicSize * aspect;
        float rightBound = mainCamera.transform.position.x + orthographicSizeOnX;
        float leftBound = mainCamera.transform.position.x - orthographicSizeOnX;

        //object
        float leftCorner = targetRigidbody.position.x - boundingBox.x / 2;
        float topCorner = targetRigidbody.position.y + boundingBox.y / 2;

        bool wrapRight = leftCorner > rightBound;
        bool wrapLeft = leftCorner < leftBound;

        bool wrapBottom = topCorner < bottomBound;
        bool wrapTop = topCorner > topBound;
        if (wrapRight || wrapBottom || wrapLeft || wrapTop)
        {
            Vector2 newPosition = targetRigidbody.position;
            if (wrapRight) newPosition.x -= 2 * orthographicSizeOnX;
            else if (wrapLeft) newPosition.x += 2 * orthographicSizeOnX;
            if (wrapBottom) newPosition.y += 2 * mainCamera.orthographicSize;
            else if (wrapTop) newPosition.y -= 2 * mainCamera.orthographicSize;
            targetRigidbody.MovePosition(newPosition);
        }
    }
}
