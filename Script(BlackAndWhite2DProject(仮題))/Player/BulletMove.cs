using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour,SetBulletDirection,PlayerAttack
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
        if (_timeCount > 2.0f) Destroy(gameObject);//一定時間で弾を消滅させる処理(１秒）
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer ==10)//layer 10 ステージの足場やブロックのレイヤ
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {

            Destroy(gameObject);
        }
    }


    private void _BulletMove()//弾の動きの処理
    {
        _rigid.velocity = _bulletDirection*_bulletSpeed;
    }

    public void _SetDirection(Vector2 _Direction)//弾の動く方角をセットする処理(セットはPlayerShotで実行)
    {
        _bulletDirection = _Direction;
    }

    public void EnemyDamage(EnemyHP _enemyHP)//敵に触れた際にダメージを与える処理
    {
        _enemyHP._Damage(_power);
    }
}
