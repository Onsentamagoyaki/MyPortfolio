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
        if (collision.gameObject.tag == "Enemy")//プレイヤーが敵に触れた際の処理
        {
            GameObject _enemy = collision.gameObject;
            _enemy.GetComponent<EnemyAttack>().PlayerDamage(this);
            _DamageEffect(_cancellationTokenSource.Token).Forget();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Enemy")//プレイヤーが敵に触れた際の処理(trigger)
        {
            GameObject _enemy = collision.gameObject;
            _enemy.GetComponent<EnemyAttack>().PlayerDamage(this);
            _DamageEffect(_cancellationTokenSource.Token).Forget();
        }

        if (collision.gameObject.tag == "EnemyBullet")//プレイヤーが敵の弾に触れた際の処理
        {
            GameObject _enemy = collision.gameObject;

            _enemy.GetComponent<EnemyAttack>().PlayerDamage(this);
        }
    }
    public void _Damage(float _damage)//プレイヤーがダメージを受けた際の処理
    {
        _hp -= _damage;
    }

    private async UniTask _DamageEffect(CancellationToken token)//ダメージを受けた際のエフェクト処理
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

    public float _GetHP()//hpの情報を外部のスクリプトに渡す処理
    {
        return _hp;
    }

    public float _GetMAXHP()//hpの最大値を外部のスクリプトに渡す処理
    {
        return _maxHP;
    }



}
