using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;


public class BoxEnemyMove : MonoBehaviour,EnemyAttack
{
    private Rigidbody2D _rigid;

    private Vector2 _moveDirection;

    private bool _rayrecast;

    private Vector2 _rayPos;

    private float _raySize;

    private float _dirKey;

    private float _moveSpeed;

    private float _power;

    [SerializeField]EnemyDataBase _enemyDatas;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _moveDirection = Vector2.left;
        _rayrecast=false;
        _dirKey= transform.localScale.x / 2.0f;
        _rayPos = new Vector2(transform.position.x - _dirKey, transform.position.y);
        _raySize = (transform.localScale.y/2.0f)+0.75f;

        _SetStatus();
    }

    private void _SetStatus()//ステータスのセット
    {
        _moveSpeed = _enemyDatas._EnemyDatas[0]._speed;
        _power = _enemyDatas._EnemyDatas[0]._power;
        GetComponent<EnemyHP>()._SetHP(_enemyDatas._EnemyDatas[0]._hp);
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _ChangeDirection();
        _UpdateRayPos();
        _ChangeDirectionWall();
    }

    private void _Move()//移動処理
    {
        _rigid.velocity = new Vector2(_moveDirection.x * _moveSpeed, _rigid.velocity.y);
    }
     private void _UpdateRayPos()//raycastのポジションを更新する
    {
        _rayPos = new Vector2(transform.position.x - _dirKey, transform.position.y);
    }

    private async void _ChangeDirection()//足場やブロックから落ちそうになったら方向転換する処理
    {
        if (_rayrecast) return;
       
        RaycastHit2D _hit = Physics2D.Raycast(_rayPos, Vector2.down,_raySize , (1 << 10) + (1 << 13));
       

        if (!_hit.collider)
        {
            _moveDirection = -_moveDirection;
            _dirKey=-_dirKey;
            _rayPos= new Vector2(transform.position.x - _dirKey, transform.position.y);
            _rayrecast = true;
            await UniTask.Delay(100);
            _rayrecast = false;
        }

    }
    private void _ChangeDirectionWall()//壁に触れた際に方向転換する処理
    {
        RaycastHit2D _hitWall = Physics2D.Raycast(transform.position, _moveDirection, (transform.localScale.x / 2.0f) + 0.1f, 1 << 10);
        if (_hitWall.collider)
        {
            _moveDirection = -_moveDirection;
            _dirKey = -_dirKey;
            _rayPos = new Vector2(transform.position.x - _dirKey, transform.position.y);
        }
    }

    public void PlayerDamage(PlayerStatus playerStatus)
    {
        playerStatus._Damage(_power);
    }
}
