using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;



public class FlyEnemyMove : MonoBehaviour, EnemyAttack
{
    private GameObject _player;

    private bool _recastBirdAttack;

    private bool _isMove;

    private bool _recastShot;

    [SerializeField, Header("弾の種類")]
    private GameObject _bullet;

    private float _power;

    [SerializeField] EnemyDataBase _enemyDatas;

    private CancellationTokenSource _cancellationTokenSource;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _recastBirdAttack = false;
        _isMove = false;
        _recastShot = false;
        _cancellationTokenSource = new CancellationTokenSource();
        _SetStatus();
    }

    private void _SetStatus()//ステータスのセット
    {
        
        _power = _enemyDatas._EnemyDatas[2]._power;
        GetComponent<EnemyHP>()._SetHP(_enemyDatas._EnemyDatas[2]._hp);
    }
    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(_player.transform.position.x - transform.position.x) > 40.0f && !_recastBirdAttack)//プレイヤーとの距離が一定以上離れていたら_Move()を実行
        {
            _isMove = true;
            _recastBirdAttack = true;
            _Move(_cancellationTokenSource.Token).Forget();
        }
        _BirdAttack(_cancellationTokenSource.Token).Forget();
        _ChangeDir();//プレイヤーの方向を向かせる処理

        if (_isMove)
        {
            _ShotDuaMove(_cancellationTokenSource.Token).Forget();//_Move()実行中に弾を撃つ処理
        }
    }

    private async UniTaskVoid _Move(CancellationToken token)//プレイヤーとの距離が一定の範囲内になるまで移動する処理
    {
    

        float _targetPoint = 0.0f;
        if (_player.transform.position.x - transform.position.x < 0)
        {
            float newx = transform.position.x;
            float newy = transform.position.y;
            _targetPoint = _player.transform.position.x + Random.Range(0.0f, 30.0f);
            while (transform.position.x > _targetPoint)
            {
                if (token.IsCancellationRequested) return;
                newx -= 0.25f;
                transform.position = new Vector2(newx, newy);
                await UniTask.Delay(25, cancellationToken: token);
            }
        }
        else
        {
            float newx = transform.position.x;
            float newy = transform.position.y;
            _targetPoint = _player.transform.position.x - Random.Range(0.0f, 30.0f);
            while (transform.position.x < _targetPoint)
            {
                if (token.IsCancellationRequested) return;
                newx += 0.25f;
                transform.position = new Vector2(newx, newy);
                await UniTask.Delay(25, cancellationToken: token);
            }
        }

        await UniTask.Delay(Random.Range(400, 600), cancellationToken: token);

        _isMove = false;
        _recastBirdAttack = false;
    }

    

    private async UniTaskVoid _BirdAttack(CancellationToken token)//プレイヤーに向かってタックルする
    {
        if (_recastBirdAttack) return;
        _recastBirdAttack = true;

        await UniTask.Delay(Random.Range(1000, 1400), cancellationToken: token);
        _Shot();
        await UniTask.Delay(Random.Range(1000, 1400), cancellationToken: token);

        float _defX = transform.position.x - _player.transform.position.x;
        float _defY = transform.position.y - _player.transform.position.y;
        float _playerX = _player.transform.position.x;
        float _playerY = _player.transform.position.y;

        float A = _defY / (_defX * _defX);

        if (_defX >= 0)
        {
            int j = 0;
            float newx = transform.position.x;
            float newy = transform.position.y;
            for (float i = _defX; i > -_defX; i -= _defX / 50)
            {
                if (token.IsCancellationRequested) return;

                if (j < 50)
                {
                    newx -= _defX / 50;
                    newy = A * i * i + _playerY;
                }
                else
                {
                    newx -= _defX / 100;
                    newy = A * i * i + _playerY;
                }

                transform.position = new Vector2(newx, newy);

                await UniTask.Delay(30, cancellationToken: token);
                j++;
            }
        }
        else
        {
            int j = 0;
            float newx = transform.position.x;
            float newy = transform.position.y;
            for (float i = _defX; i < -_defX; i -= _defX / 50)
            {
                if (token.IsCancellationRequested) return;

                if (j < 50)
                {
                    newx -= _defX / 50;
                    newy = A * i * i + _playerY;
                }
                else
                {
                    newx -= _defX / 100;
                    newy = A * i * i + _playerY;
                }

                transform.position = new Vector2(newx, newy);

                await UniTask.Delay(30, cancellationToken: token);
                j++;
            }
        }

        await UniTask.Delay(Random.Range(400, 600), cancellationToken: token);
        _Shot();
        await UniTask.Delay(Random.Range(2000, 3000), cancellationToken: token);
        _recastBirdAttack = false;
    }

    private void _ChangeDir()
    {
        if (transform.position.x - _player.transform.position.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180.0f, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }

    private void _Shot()//弾を撃つ処理
    {
        Vector2[] _dir = new Vector2[3];
        _dir[0] = _player.transform.position - transform.position;
        _dir[0].Normalize();

        float angle = Mathf.Atan2(_dir[0].y, _dir[0].x);
        float angleDegrees = angle * Mathf.Rad2Deg;
        float angleDeg1 = angleDegrees + 10.0f;
        angleDeg1 *= Mathf.Deg2Rad;
        float angleDeg2 = angleDegrees - 10.0f;
        angleDeg2 *= Mathf.Deg2Rad;
        _dir[1] = new Vector2(Mathf.Cos(angleDeg1), Mathf.Sin(angleDeg1));
        _dir[2] = new Vector2(Mathf.Cos(angleDeg2), Mathf.Sin(angleDeg2));

        for (int i = 0; i < 3; i++)
        {
            GameObject shot = Instantiate(_bullet, transform.position, transform.rotation) as GameObject;
            SetBulletDirection s = shot.GetComponent<SetBulletDirection>();
            s._SetDirection(_dir[i]);
        }
    }

    private async UniTaskVoid _ShotDuaMove(CancellationToken token)//_Move()実行中に弾を撃つ処理
    {
        if (_recastShot) return;
        _recastShot = true;
        await UniTask.Delay(1500, cancellationToken: token);
        _Shot();
        _recastShot = false;
    }

    public void PlayerDamage(PlayerStatus playerStatus)
    {
        playerStatus._Damage(_power);
    }
}
