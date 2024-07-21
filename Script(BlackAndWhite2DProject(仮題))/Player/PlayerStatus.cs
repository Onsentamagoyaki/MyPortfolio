using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    

    [SerializeField, Header("MAXHP")]
    private float _maxHP;
    private float _hp;
    private SpriteRenderer _spriteRenderer;
    private CancellationTokenSource _cancellationTokenSource;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _cancellationTokenSource = new CancellationTokenSource();
        _hp = _maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_hp);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")//�v���C���[���G�ɐG�ꂽ�ۂ̏���
        {
            GameObject _enemy = collision.gameObject;
            _enemy.GetComponent<EnemyAttack>().PlayerDamage(this);
            _DamageEffect(_cancellationTokenSource.Token).Forget();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Enemy")//�v���C���[���G�ɐG�ꂽ�ۂ̏���(trigger)
        {
            GameObject _enemy = collision.gameObject;
            _enemy.GetComponent<EnemyAttack>().PlayerDamage(this);
            _DamageEffect(_cancellationTokenSource.Token).Forget();
        }

        if (collision.gameObject.tag == "EnemyBullet")//�v���C���[���G�̒e�ɐG�ꂽ�ۂ̏���
        {
            GameObject _enemy = collision.gameObject;

            _enemy.GetComponent<EnemyAttack>().PlayerDamage(this);
        }
    }
    public void _Damage(float _damage)//�v���C���[���_���[�W���󂯂��ۂ̏���
    {
        _hp -= _damage;
    }

    private async UniTask _DamageEffect(CancellationToken token)//�_���[�W���󂯂��ۂ̃G�t�F�N�g����
    {
        gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
        Color color = _spriteRenderer.color;
        for (int i = 0; i < 12; i++)
        {
            if (token.IsCancellationRequested) return;
            _spriteRenderer.color = new Color(color.r, color.g, color.b, 0.0f);
            await UniTask.Delay(50, cancellationToken: token);
            _spriteRenderer.color = new Color(color.r, color.g, color.b, 1.0f);
            await UniTask.Delay(50, cancellationToken: token);
        }
        _spriteRenderer.color = color;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public float _GetHP()//hp�̏����O���̃X�N���v�g�ɓn������
    {
        return _hp;
    }

    public float _GetMAXHP()//hp�̍ő�l���O���̃X�N���v�g�ɓn������
    {
        return _maxHP;
    }



}
