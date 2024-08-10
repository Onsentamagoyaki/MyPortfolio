using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBulletMove : MonoBehaviour,SetBulletDirection,EnemyAttack
{
    [SerializeField, Header("弾の速度")]
    private float _bulletSpeed;

    [SerializeField, Header("攻撃力")]
    private float _power;

    private Rigidbody2D _rigid;
    private Vector2 _bulletDirection;
    private float _timeCount;//弾が消滅するまでの時間をカウントする変数



    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _timeCount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        _BulletMove();//弾の動きの処理
        _timeCount += Time.deltaTime;
        if (_timeCount > 2.8f) Destroy(gameObject);//一定時間で弾を消滅させる処理
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)//layer10にふれた際の処理　layer 10 ステージの足場やブロックのレイヤ
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player")//プレイヤーに触れた際の処理
        {

            Destroy(gameObject);
        }
    }



    private void _BulletMove()//弾の動きの処理
    {
        _rigid.velocity = _bulletDirection * _bulletSpeed;
    }

    public void _SetDirection(Vector2 _Direction)//弾の進行方向を決める処理
    {
        _bulletDirection = _Direction;
    }
   
    public void PlayerDamage(PlayerStatus playerStatus)//この弾がプレイヤーにふれた際にダメージを与える処理
    {
        playerStatus._Damage(_power);
    }
}
