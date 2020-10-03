﻿using System;
using UnityEngine;

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

    private ScreenWrapperCameraController screenWraperController;
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, m_BoundingBoxSize);
    }
    private void Start()
    {
        screenWraperController = Camera.main.GetComponent<ScreenWrapperCameraController>();
        targetRigidbody = GetComponent<Rigidbody2D>();
        var collider = m_colliderParent.GetComponent<Collider2D>();
        var colliderTransform = m_colliderParent.transform;

        leftCollider = Instantiate(m_colliderParent, LeftColliderPosition(colliderTransform, screenWraperController),
            m_colliderParent.transform.rotation);
        var leftColliderComp = leftCollider.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(collider, leftColliderComp, true);
        leftColliderComp.isTrigger = true;
        

        topCollider = Instantiate(m_colliderParent, TopColliderPosition(colliderTransform, screenWraperController),
            m_colliderParent.transform.rotation);
        var topColliderComp = topCollider.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(collider, topColliderComp, true);
        topColliderComp.isTrigger = true;
        

        leftTopCollider = Instantiate(m_colliderParent, LeftTopColliderPosition(colliderTransform, screenWraperController),
            m_colliderParent.transform.rotation);
        var leftTopColliderComp = leftTopCollider.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(collider, leftTopColliderComp, true);
        leftTopColliderComp.isTrigger = true;

        leftCollider.transform.parent = m_colliderParent.transform;
        leftCollider.SetActive(false);

        topCollider.transform.parent = m_colliderParent.transform;
        topCollider.SetActive(false);

        leftTopCollider.transform.parent = m_colliderParent.transform;
        leftTopCollider.SetActive(false);
    }

    public static Vector3 LeftColliderPosition(Transform colliderTransform, ScreenWrapperCameraController screenWrapperCameraController) {
        return colliderTransform.position - new Vector3(2 * screenWrapperCameraController.OrthographicSizeOnX, 0);
    }

    public static Vector3 TopColliderPosition(Transform colliderTransform, ScreenWrapperCameraController screenWrapperCameraController)
    {
        return colliderTransform.position + new Vector3(0, 2 * screenWrapperCameraController.OrthographicSizeOnY);
    }

    public static Vector3 LeftTopColliderPosition(Transform colliderTransform, ScreenWrapperCameraController screenWrapperCameraController)
    {
        return colliderTransform.position - new Vector3(2 * screenWrapperCameraController.OrthographicSizeOnX, 
            -2 * screenWrapperCameraController.OrthographicSizeOnY);
    }

    private Func<Transform, ScreenWrapperCameraController, Vector3> calculateLeftColliderPosition = LeftColliderPosition;

    private Func<Transform, ScreenWrapperCameraController, Vector3> calculateTopColliderPosition = TopColliderPosition;

    private Func<Transform, ScreenWrapperCameraController, Vector3> calculateLeftTopColliderPosition = LeftTopColliderPosition;

    private void FixedUpdate()
    {
        float leftCorner = targetRigidbody.position.x - m_BoundingBoxSize.x / 2;
        float rightCorner = targetRigidbody.position.x + m_BoundingBoxSize.x / 2;

        float topCorner = targetRigidbody.position.y + m_BoundingBoxSize.y / 2;
        float bottomCorner = targetRigidbody.position.y - m_BoundingBoxSize.y / 2;


        bool wrapRight = leftCorner > screenWraperController.RightBound;
        bool wrapLeft = leftCorner < screenWraperController.LeftBound;

        bool wrapBottom = topCorner < screenWraperController.BottomBound;
        bool wrapTop = topCorner > screenWraperController.TopBound;
        if (wrapRight || wrapBottom || wrapLeft || wrapTop)
        {
            Vector2 newPosition = targetRigidbody.position;
            if (wrapRight) newPosition.x -= 2 * screenWraperController.OrthographicSizeOnX;
            else if (wrapLeft) newPosition.x += 2 * screenWraperController.OrthographicSizeOnX;
            if (wrapBottom) newPosition.y += 2 * screenWraperController.OrthographicSizeOnY;
            else if (wrapTop) newPosition.y -= 2 * screenWraperController.OrthographicSizeOnY;
            targetRigidbody.position = newPosition;
        }

        //Debug.Log($"{wrapTop} {wrapRight} {wrapBottom} {wrapLeft}");

        
        bool activateLeftCollider = rightCorner > screenWraperController.RightBound;
        bool activateTopCollider = bottomCorner < screenWraperController.BottomBound;
        
        UpdateAuxCollider(leftCollider, activateLeftCollider, calculateLeftColliderPosition, m_colliderParent.transform, screenWraperController);
        UpdateAuxCollider(topCollider, activateTopCollider, calculateTopColliderPosition, m_colliderParent.transform, screenWraperController);
        UpdateAuxCollider(leftTopCollider, activateTopCollider && activateLeftCollider, calculateLeftTopColliderPosition, m_colliderParent.transform, screenWraperController);
        
    }

    private static void UpdateAuxCollider(GameObject target, bool isActive, Func<Transform, ScreenWrapperCameraController, Vector3> targetPosition, 
        Transform colliderTransform, ScreenWrapperCameraController screenWrapperCameraController)
    {
        if (isActive)
        {
            if (!target.activeSelf) target.SetActive(true);
            target.transform.position = targetPosition.Invoke(colliderTransform, screenWrapperCameraController);
        }
        else
        {
            if (target.activeSelf) target.SetActive(false);
        }
    }
}
