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

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = initialVelocity + Random.insideUnitCircle* initialRandomLength;
    }

}
