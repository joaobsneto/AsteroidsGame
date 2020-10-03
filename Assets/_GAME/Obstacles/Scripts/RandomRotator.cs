using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
    [SerializeField]
    private float tumble;

    private Vector3 rotationAxis;

    void Start()
    {
        rotationAxis = Random.insideUnitSphere;
    }

    private void Update()
    {
        transform.Rotate(rotationAxis, Time.deltaTime * tumble);
    }
}