using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;

public class Rolling : MonoBehaviour
{
    [SerializeField, Header("��]���x")] 
    private float rotate;

    [SerializeField, Header("(�����j��]�����@�����v�F�O�@���v�F�P")] 
    private bool key_Direction;

    private int Direction;
    // Start is called before the first frame update
    void Start()
    {
        if (key_Direction) Direction = -1;
        else Direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
    gameObject.transform.Rotate(new Vector3(0,0,Direction*rotate*Time.deltaTime));//�G�̈ړ�����      
    }

   
}
