using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;

public class Rolling : MonoBehaviour
{
    [SerializeField, Header("回転速度")] 
    private float rotate;

    [SerializeField, Header("(初期）回転方向　反時計：０　時計：１")] 
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
    gameObject.transform.Rotate(new Vector3(0,0,Direction*rotate*Time.deltaTime));//敵の移動処理      
    }

   
}
