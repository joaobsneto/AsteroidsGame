using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SetInitialVelocity : MonoBehaviour
{
    [SerializeField]
    private Vector2 initialVelocity = Vector2.zero;

    [SerializeField]
    private float initialRandomLength = 0;

    [SerializeField]
    private float initialCustomAxisVelocity = 0;

    [SerializeField]
    private Vector2 customAxis = Vector2.up;

    private void OnEnable()
    {
        var rigidbody = GetComponent<Rigidbody2D>();
        var customAxis3 = transform.TransformDirection(customAxis);
        rigidbody.velocity = initialVelocity + Random.insideUnitCircle * initialRandomLength + new Vector2(customAxis3.x, customAxis3.y) * initialCustomAxisVelocity;
    }

}
