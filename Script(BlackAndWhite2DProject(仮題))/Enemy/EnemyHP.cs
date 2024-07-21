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
        if (collision.gameObject.tag == "Bullet")//�v���C���[�̒e�ɓG���G�ꂽ�ۂ̏���
        {
            GameObject _bullet = collision.gameObject;

            _bullet.GetComponent<PlayerAttack>().EnemyDamage(this);
        }
    }

    private void _Dead()//�G�̎��S����
    {
        if (_hp <= 0)
        {

            GetComponent<CoinDrop>()._DropCoin();//���񂾍΂̃R�C���̃h���b�v����

            Destroy(gameObject);
        }
     }

        public void _Damage(float damage)//�G���_���[�W���󂯂��ۂ̏���
    {
        _hp -= damage;
    }
}
