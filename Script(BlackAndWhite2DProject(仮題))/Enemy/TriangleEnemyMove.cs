using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TriangleEnemyMove : MonoBehaviour,EnemyAttack
{
    private Rigidbody2D _rigid;

    private Vector2 _moveDirection;

    private float _moveSpeed;

    private float _power;

    [SerializeField] EnemyDataBase _enemyDatas;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _moveDirection = Vector2.left;
        _SetStatus();
    }

    private void _SetStatus()//ステータスのセット
    {
        _moveSpeed = _enemyDatas._EnemyDatas[1]._speed;
        _power = _enemyDatas._EnemyDatas[1]._power;
        GetComponent<EnemyHP>()._SetHP(_enemyDatas._EnemyDatas[1]._hp);
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _ChangeDirection();
    }

    private void _Move()//移動処理
    {
        _rigid.velocity = new Vector2(_moveDirection.x * _moveSpeed, _rigid.velocity.y);
    }

    private void _ChangeDirection()//壁にぶつかった際に移動方向を変える処理
    {
        RaycastHit2D _hitWall = Physics2D.Raycast(transform.position, _moveDirection, (transform.localScale.x/2.0f)+0.1f, 1 << 10);
        if (_hitWall.collider) _moveDirection = -_moveDirection;
    }

    public void PlayerDamage(PlayerStatus playerStatus)
    {
        playerStatus._Damage(_power);
    }
}
