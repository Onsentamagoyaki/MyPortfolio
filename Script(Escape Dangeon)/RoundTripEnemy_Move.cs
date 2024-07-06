using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTripEnemy_Move : MonoBehaviour, EnemyAttacked
{
    [SerializeField, Header("攻撃力")] private int _attackPower;

    [SerializeField, Header("移動方向（０：上下　１：左右")] private bool _direction;

    [SerializeField, Header("速度")]private float _speed;

    [SerializeField, Header("移動幅")] private float _length;

    [SerializeField, Header("移動開始方向(0:上,右 1:下,左)")] private bool _startdirectionKey;

    float _startdirection;

    Vector3 _initPos;
    // Start is called before the first frame update
    void Start()
    {
        _initPos = transform.position;
        if (_startdirectionKey) _startdirection = -1.0f;
        else _startdirection = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if( !_direction) transform.position = new Vector3(transform.position.x,_initPos.y+_startdirection*Mathf.PingPong(Time.time*_speed,_length),transform.position.z); 
        else transform.position = new Vector3(_initPos.x + _startdirection * Mathf.PingPong(Time.time * _speed, _length), transform.position.y, transform.position.z);
        //上は敵の移動処理
    }

    public void PlayerDamage(Player_Move player_Move)
    {
        player_Move.Damage(_attackPower);//プレイヤーがこの敵に当たった際のダメージ処理
    }

}
