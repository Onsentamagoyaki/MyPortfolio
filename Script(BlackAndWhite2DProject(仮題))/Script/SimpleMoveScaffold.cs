using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoveScaffold : MonoBehaviour
{
   
    [SerializeField, Header("移動方向（０：上下　１：左右")] private bool _direction;

    [SerializeField, Header("速度")]private float _speed;

    [SerializeField, Header("移動幅")] private float _length;

    [SerializeField, Header("移動開始方向(0:上,右 1:下,左)")] private bool _startdirectionKey;

    float _startDir;

    Vector3 _initPos;
    // Start is called before the first frame update
    void Start()
    {
        _initPos = transform.position;
        if (_startdirectionKey) _startDir = -1.0f;
        else _startDir = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if( !_direction) transform.position = new Vector3(transform.position.x,_initPos.y+ _startDir * Mathf.PingPong(Time.time*_speed,_length),transform.position.z); 
        else transform.position = new Vector3(_initPos.x + _startDir * Mathf.PingPong(Time.time * _speed, _length), transform.position.y, transform.position.z);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)//移動する床に乗った時の処理
    {
        if (collision.gameObject.tag == "Player")
        {
            bool _touchUP = false;
            float _minPos = Mathf.Infinity;
            foreach (ContactPoint2D point in collision.contacts)
            {
                _minPos = MathF.Min(_minPos, point.point.y);

            }
            Debug.Log(_minPos);
            Debug.Log(transform.position.y);
            if (transform.position.y-0.2f < _minPos) _touchUP = true;
            Debug.Log(_touchUP);
            if (_touchUP) collision.transform.SetParent(transform);

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") collision.transform.SetParent(null);//移動する床から離れたときの処理

    }

}
