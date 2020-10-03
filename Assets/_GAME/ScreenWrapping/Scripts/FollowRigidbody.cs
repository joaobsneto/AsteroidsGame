using System;
using UnityEngine;

public class FollowRigidbody : MonoBehaviour
{
    public Vector2 Offset;
    private Rigidbody2D self;

    public Rigidbody2D Target { get; private set; }

    private void Awake()
    {
        self = gameObject.AddComponent<Rigidbody2D>();
        self.gravityScale = 0;
        self.isKinematic = true;
    }

    private void OnEnable()
    {
        if (Target != null)
            UpdateTransform();
    }

    private void UpdateTransform()
    {
        self.MovePosition(Target.position + Offset);
        self.SetRotation(Target.rotation);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateTransform();
    }

    internal void SetUp(Rigidbody2D targetRigidbody, Vector2 offset)
    {
        Target = targetRigidbody;
        Offset = offset;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Target.SendMessage("OnCollisionEnter2D", collision);
    }
}
