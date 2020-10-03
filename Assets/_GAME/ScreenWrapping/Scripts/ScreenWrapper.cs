using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class ScreenWrapper : MonoBehaviour
{
    [SerializeField]
    private GameObject m_colliderParent = null;
    [SerializeField]
    private Vector2 m_BoundingBoxSize = Vector2.one;

    private Rigidbody2D targetRigidbody;

    private GameObject leftCollider;
    private GameObject topCollider;
    private GameObject leftTopCollider;

    private ScreenWrapperCameraController screenWrapperController;

    private float initialInertia;
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, m_BoundingBoxSize);
        if (screenWrapperController != null)
        {
            Gizmos.DrawLine(transform.position, LeftColliderPosition(targetRigidbody, screenWrapperController));
            Gizmos.DrawLine(transform.position, TopColliderPosition(targetRigidbody, screenWrapperController));
            Gizmos.DrawLine(transform.position, LeftTopColliderPosition(targetRigidbody, screenWrapperController));
        }
    }

    public static Vector3 LeftColliderPosition(Rigidbody2D colliderTransform, ScreenWrapperCameraController screenWrapperCameraController)
    {
        return colliderTransform.position - new Vector2(2 * screenWrapperCameraController.OrthographicSizeOnX, 0);
    }

    public static Vector3 TopColliderPosition(Rigidbody2D colliderTransform, ScreenWrapperCameraController screenWrapperCameraController)
    {
        return colliderTransform.position + new Vector2(0, 2 * screenWrapperCameraController.OrthographicSizeOnY);
    }

    public static Vector3 LeftTopColliderPosition(Rigidbody2D colliderTransform, ScreenWrapperCameraController screenWrapperCameraController)
    {
        return colliderTransform.position - new Vector2(2 * screenWrapperCameraController.OrthographicSizeOnX,
            -2 * screenWrapperCameraController.OrthographicSizeOnY);
    }

    private void Awake()
    {
        screenWrapperController = Camera.main.GetComponent<ScreenWrapperCameraController>();
        targetRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        
        var collider = m_colliderParent.GetComponent<Collider2D>();
        //var colliderTransform = m_colliderParent.transform;

        leftCollider = Instantiate(m_colliderParent, LeftColliderPosition(targetRigidbody, screenWrapperController),
            m_colliderParent.transform.rotation);
        var leftColliderComp = leftCollider.GetComponent<Collider2D>();
        leftCollider.AddComponent<FollowRigidbody>().SetUp(targetRigidbody, new Vector2(2 * screenWrapperController.OrthographicSizeOnX, 0));
        Physics2D.IgnoreCollision(collider, leftColliderComp, true);
        leftCollider.name = $"{gameObject.name}-LeftCollider";
        //leftColliderComp.isTrigger = true;


        topCollider = Instantiate(m_colliderParent, TopColliderPosition(targetRigidbody, screenWrapperController),
            m_colliderParent.transform.rotation);
        var topColliderComp = topCollider.GetComponent<Collider2D>();
        topCollider.AddComponent<FollowRigidbody>().SetUp(targetRigidbody, new Vector2(0, 2 * screenWrapperController.OrthographicSizeOnY));
        Physics2D.IgnoreCollision(collider, topColliderComp, true);
        topCollider.name = $"{gameObject.name}-TopCollider";
        //topColliderComp.isTrigger = true;


        leftTopCollider = Instantiate(m_colliderParent, LeftTopColliderPosition(targetRigidbody, screenWrapperController),
            m_colliderParent.transform.rotation);
        var leftTopColliderComp = leftTopCollider.GetComponent<Collider2D>();
        leftTopCollider.AddComponent<FollowRigidbody>().SetUp(targetRigidbody, new Vector2(2 * screenWrapperController.OrthographicSizeOnX,
            -2 * screenWrapperController.OrthographicSizeOnY));
        Physics2D.IgnoreCollision(collider, leftTopColliderComp, true);
        leftTopCollider.name = $"{gameObject.name}-LeftTopCollider";
        //leftTopColliderComp.isTrigger = true;

        //leftCollider.transform.parent = m_colliderParent.transform;
        leftCollider.SetActive(false);

        //topCollider.transform.parent = m_colliderParent.transform;
        topCollider.SetActive(false);

        //leftTopCollider.transform.parent = m_colliderParent.transform;
        leftTopCollider.SetActive(false);

        initialInertia = targetRigidbody.inertia;
        targetRigidbody.centerOfMass = Vector2.zero;
    }



    private Func<Rigidbody2D, ScreenWrapperCameraController, Vector3> calculateLeftColliderPosition = LeftColliderPosition;

    private Func<Rigidbody2D, ScreenWrapperCameraController, Vector3> calculateTopColliderPosition = TopColliderPosition;

    private Func<Rigidbody2D, ScreenWrapperCameraController, Vector3> calculateLeftTopColliderPosition = LeftTopColliderPosition;

    private void FixedUpdate()
    {
        float leftCorner = targetRigidbody.position.x - m_BoundingBoxSize.x / 2;
        

        float topCorner = targetRigidbody.position.y + m_BoundingBoxSize.y / 2;
        


        bool wrapRight = leftCorner > screenWrapperController.RightBound;
        bool wrapLeft = leftCorner < screenWrapperController.LeftBound;

        bool wrapBottom = topCorner < screenWrapperController.BottomBound;
        bool wrapTop = topCorner > screenWrapperController.TopBound;
        if (wrapRight || wrapBottom || wrapLeft || wrapTop)
        {
            Vector2 newPosition = targetRigidbody.position;
            if (wrapRight) newPosition.x -= 2 * screenWrapperController.OrthographicSizeOnX;
            else if (wrapLeft) newPosition.x += 2 * screenWrapperController.OrthographicSizeOnX;
            if (wrapBottom) newPosition.y += 2 * screenWrapperController.OrthographicSizeOnY;
            else if (wrapTop) newPosition.y -= 2 * screenWrapperController.OrthographicSizeOnY;
            targetRigidbody.position = newPosition;
        }

        float rightCorner = targetRigidbody.position.x + m_BoundingBoxSize.x / 2;
        float bottomCorner = targetRigidbody.position.y - m_BoundingBoxSize.y / 2;

        bool activateLeftCollider = rightCorner > screenWrapperController.RightBound;
        bool activateTopCollider = bottomCorner < screenWrapperController.BottomBound;
        if (UpdateAuxCollider(leftCollider, activateLeftCollider, calculateLeftColliderPosition, targetRigidbody, screenWrapperController) |
        UpdateAuxCollider(topCollider, activateTopCollider, calculateTopColliderPosition, targetRigidbody, screenWrapperController) |
        UpdateAuxCollider(leftTopCollider, activateTopCollider && activateLeftCollider, calculateLeftTopColliderPosition, targetRigidbody, screenWrapperController))
        {
            targetRigidbody.inertia = initialInertia;
            targetRigidbody.centerOfMass = Vector2.zero;
        }

    }

    private static bool UpdateAuxCollider(GameObject target, bool isActive, Func<Rigidbody2D, ScreenWrapperCameraController, Vector3> targetPosition,
        Rigidbody2D rigidbody, ScreenWrapperCameraController screenWrapperCameraController)
    {
        if (isActive)
        {
            //target.transform.position = targetPosition.Invoke(rigidbody, screenWrapperCameraController) - rigidbody.angularVelocity * Time.fixedDeltaTime);
            if (!target.activeSelf)
            {
                target.SetActive(true);
                return true;
            }
        }
        else
        {
            if (target.activeSelf) target.SetActive(false);
        }
        return false;
    }
}
