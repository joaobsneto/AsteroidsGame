using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageObstacle : MonoBehaviour
{
    [SerializeField]
    private StageReference m_stage = null;
    private void OnEnable()
    {
        m_stage.Add();
    }

    private void OnDisable()
    {
        m_stage.Remove();
    }
}
