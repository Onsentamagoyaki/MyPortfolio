using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour,SetBulletDirection,PlayerAttack
{

    [SerializeField, Header("�e�̑��x")]
    private float _bulletSpeed;

    [SerializeField, Header("�U����")]
    private float _power;

    private Rigidbody2D _rigid;
    private Vector2 _bulletDirection;
    private float _timeCount;//�e�����ł���܂ł̎��Ԃ��J�E���g����ϐ�

    

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _timeCount = 0.0f;                                                              
       
    }

    // Update is called once per frame
    void Update()
    {
        _BulletMove();//�e�̓����̏���
        _timeCount += Time.deltaTime;
        if (_timeCount > 2.0f) Destroy(gameObject);//��莞�ԂŒe�����ł����鏈��(�P�b�j
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer ==10)//layer 10 �X�e�[�W�̑����u���b�N�̃��C��
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {

            Destroy(gameObject);
        }
    }


    private void _BulletMove()//�e�̓����̏���
    {
        _rigid.velocity = _bulletDirection*_bulletSpeed;
    }

    public void _SetDirection(Vector2 _Direction)//�e�̓������p���Z�b�g���鏈��(�Z�b�g��PlayerShot�Ŏ��s)
    {
        _bulletDirection = _Direction;
    }

    public void EnemyDamage(EnemyHP _enemyHP)//�G�ɐG�ꂽ�ۂɃ_���[�W��^���鏈��
    {
        _enemyHP._Damage(_power);
    }
}
