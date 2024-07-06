using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTripEnemy_Move : MonoBehaviour, EnemyAttacked
{
    [SerializeField, Header("�U����")] private int _attackPower;

    [SerializeField, Header("�ړ������i�O�F�㉺�@�P�F���E")] private bool _direction;

    [SerializeField, Header("���x")]private float _speed;

    [SerializeField, Header("�ړ���")] private float _length;

    [SerializeField, Header("�ړ��J�n����(0:��,�E 1:��,��)")] private bool _startdirectionKey;

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
        //��͓G�̈ړ�����
    }

    public void PlayerDamage(Player_Move player_Move)
    {
        player_Move.Damage(_attackPower);//�v���C���[�����̓G�ɓ��������ۂ̃_���[�W����
    }

}
