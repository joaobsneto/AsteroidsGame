using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
    [SerializeField]
    private Vector2 m_angularSpeedRange = new Vector2(6, 11);

    private Vector3 rotationAxis;

    void Start()
    {
        rotationAxis = Random.insideUnitSphere;
    }

    private void Update()
    {
        transform.Rotate(rotationAxis, Time.deltaTime * Random.Range(m_angularSpeedRange.x, m_angularSpeedRange.y));
    }
}