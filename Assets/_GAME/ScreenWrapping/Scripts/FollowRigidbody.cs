using System;
using UnityEngine;

public class FollowRigidbody : MonoBehaviour
{
    public Vector2 Offset;

    private Rigidbody2D target;

    private Rigidbody2D self;

    private void Awake()
    {
        self = gameObject.AddComponent<Rigidbody2D>();
        self.gravityScale = 0;
        self.isKinematic = true;
    }

    private void OnEnable()
    {
        if (target != null)
            UpdateTransform();
    }

    private void UpdateTransform()
    {
        self.MovePosition(target.position + Offset);
        self.SetRotation(target.rotation);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateTransform();
    }

    internal void SetUp(Rigidbody2D targetRigidbody, Vector2 offset)
    {
        target = targetRigidbody;
        Offset = offset;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        target.SendMessage("OnCollisionEnter2D", collision);
    }
}
