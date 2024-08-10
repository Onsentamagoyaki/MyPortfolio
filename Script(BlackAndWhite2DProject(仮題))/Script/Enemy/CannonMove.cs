using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CannonMove : MonoBehaviour,EnemyAttack
{
    [SerializeField,Header("キャノンのタイプ　0:B　1:A")]
    private bool _cannonType;
    private GameObject _player;
    private Transform _mainGun;
    private Vector3 _shotPosition;
    private float _angle;
    private CannonShot _cannonShot;
    private float _power;
    private float _rotSpeed;
    [SerializeField] EnemyDataBase _enemyDatas;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _mainGun= transform.GetChild(0).transform;
        _shotPosition = _mainGun.position;
        _cannonShot = transform.GetChild(0).GetComponent<CannonShot>();
        _SetStatus();


    }
    private void _SetStatus()//ステータスのセット
    {
        _power = _enemyDatas._EnemyDatas[3]._power;
        GetComponent<EnemyHP>()._SetHP(_enemyDatas._EnemyDatas[3]._hp);
        _rotSpeed = _enemyDatas._EnemyDatas[3]._otherPara1;
    }

    // Update is called once per frame
    async void Update()
    {
        if (_cannonShot._bCannonRot) return;
        _CalculateAngle();
        _TrackingPlayer();
        _UpdateShotPos();
        _cannonShot._SetShotDirection();
        await _cannonShot._fire();

    }

    private void _UpdateShotPos()//弾を撃つポジションの更新
    {
        _shotPosition = _mainGun.position;
    }

    private void _CalculateAngle()//キャノンとプレイヤーの位置関係から水平方向に対しての角度を計算する処理
    {
        if (_player == null) return;
        Vector3 direction;
        if (_cannonType)
        {
            direction = transform.position - _player.transform.position;
        }
        else
        {
            direction = _player.transform.position - transform.position;
        }
            
        _angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    private void _TrackingPlayer()//プレイヤーを追尾する処理
    {
        if (_player == null) return;

        
        float currentAngle = transform.eulerAngles.z;

        
        float angleDifference = Mathf.DeltaAngle(currentAngle, _angle);

       
        if (Mathf.Abs(angleDifference) > 0.1f)
        {
            float rotationStep = _rotSpeed * Mathf.Sign(angleDifference);
            transform.Rotate(0.0f, 0.0f, rotationStep);
        }
    }

    

    public void PlayerDamage(PlayerStatus playerStatus)
    {
        playerStatus._Damage(_power);
    }
}
