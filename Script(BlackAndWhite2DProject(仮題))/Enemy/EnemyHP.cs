using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    

    private float _hp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void _SetHP(float HP)
    {
        _hp = HP;
    }

    // Update is called once per frame
    void Update()
    {
        _Dead();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")//プレイヤーの弾に敵が触れた際の処理
        {
            GameObject _bullet = collision.gameObject;

            _bullet.GetComponent<PlayerAttack>().EnemyDamage(this);
        }
    }

    private void _Dead()//敵の死亡判定
    {
        if (_hp <= 0)
        {

            GetComponent<CoinDrop>()._DropCoin();//死んだ歳のコインのドロップ処理

            Destroy(gameObject);
        }
     }

        public void _Damage(float damage)//敵がダメージを受けた際の処理
    {
        _hp -= damage;
    }
}
