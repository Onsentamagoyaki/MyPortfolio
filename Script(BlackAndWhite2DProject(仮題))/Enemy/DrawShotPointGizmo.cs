using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawShotPointGizmo : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(0.5f, 0.5f, 0.5f));
    }

}
