using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInitialVelocity : MonoBehaviour
{
    [SerializeField]
    private Vector2 initialVelocity = Vector2.zero;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = initialVelocity;
    }

}
