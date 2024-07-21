using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDrawPoint : MonoBehaviour
{

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }

}
