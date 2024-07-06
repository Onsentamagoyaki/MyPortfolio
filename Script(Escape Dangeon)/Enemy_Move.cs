using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Move : MonoBehaviour,EnemyAttacked
{   
    [SerializeField, Header("移動速度")]
    private float _moveSpeed;

    [SerializeField, Header("攻撃力")]
    private int _attackPower;

    private Vector2 _moveDirection;
    private Rigidbody2D _rigid;

    // Start is called before the first frame update
    void Start()
    {
        _rigid =GetComponent<Rigidbody2D>();
        _moveDirection = Vector2.left;
    }

    // Update is called once per frame
    void Update()
    {
        _Move();//移動処理
        _ChangeMoveDirectoin();//壁に当たった際に移動方向を逆にする処理
        _LookMoveDirection();//移動方向を変えた際にモデルの向きを反転させる処理
    }

    private void _Move()
    {
        _rigid.velocity = new Vector2(_moveDirection.x * _moveSpeed , _rigid.velocity.y);
    }   

    private void _ChangeMoveDirectoin()//壁に当たった際に移動方向を逆にする処理
    {
        Vector2 halfSize =transform.localScale/ 2.0f;
        int layerMask = LayerMask.GetMask("Floor");
        RaycastHit2D ray = Physics2D.Raycast(transform.position, -transform.right, halfSize.x + 0.1f, layerMask);
        if (ray.transform == null) return;
        if (ray.transform.tag == "Floor")
        {
            _moveDirection = -_moveDirection;
        }

    }

    private void _LookMoveDirection()//移動方向を変えた際にモデルの向きを反転させる処理
    {
        if(_moveDirection.x <0.0f)
        {
            transform.eulerAngles = Vector3.zero;
        }

        else if (_moveDirection.x > 0.0f)
        {
            transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        }
    }
    public void PlayerDamage(Player_Move Player_Move)//プレイヤーがこの敵に当たった際のダメージ処理
    {
        Player_Move.Damage(_attackPower);
    }
}
