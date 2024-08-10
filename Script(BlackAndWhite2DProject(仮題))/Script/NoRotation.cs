using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoRotation : MonoBehaviour
{
    private Quaternion _initRot;
    private Vector3 _initPos;

    // Start is called before the first frame update
    void Start()
    {
        _initRot = transform.rotation;
        _initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       transform.rotation = _initRot;
       transform.position = _initPos;
    }
}
