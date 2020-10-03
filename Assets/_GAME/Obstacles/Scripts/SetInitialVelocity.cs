using System.Collections;
using System.Collections.Generic;
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


    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = initialVelocity + Random.insideUnitCircle* initialRandomLength + customAxis * initialCustomAxisVelocity;
    }

}
