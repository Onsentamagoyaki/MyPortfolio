using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerShot : MonoBehaviour
{

    [SerializeField, Header("�e�̎��")]
    private GameObject _bullet;

    private Vector2 _aimDirection;//�G�C�������킹�Ă������
    private Vector2 _shotDirection;
    private bool _bFire;
    private bool _recastFire;//�e�������L���X�g
    private CancellationTokenSource _cancellationTokenSource;
    GameObject _target;
    private Vector2 _aim;

    
    // Start is called before the first frame update
    void Start()
    {
        _bFire = false;
        _recastFire = false;
        _cancellationTokenSource = new CancellationTokenSource();
        _target = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        _fire(_cancellationTokenSource.Token).Forget();//�e��������
        _SetShotDirection();//�e�̒e�����΂�����鏈��
       
        _target.transform.localPosition = _aim;
        //Debug.DrawRay(transform.position + new Vector3(0.0f, 0.2f, 0.0f), aim, Color.red);
    }

    public void _OnAim(InputAction.CallbackContext context)//�L�[���͂���G�C��������ǂݎ�鏈��
    {
        /* 
         * if (context.ReadValue<Vector2>().magnitude < 0.4) return;
         * _aimDirection = context.ReadValue<Vector2>() == new Vector2(0.0f, 0.0f) ? _aimDirection : context.ReadValue<Vector2>();
         * _aimDirection.Normalize();
        �R���\�[���ł̏���*/

        _aimDirection = context.ReadValue<Vector2>();
        _aimDirection = Camera.main.ScreenToWorldPoint(_aimDirection) - transform.position;
        if (_aimDirection.magnitude > 12)//�Ə��̈ʒu�����߂鏈��
        {
            _aim = _aimDirection.normalized;
            _aim *= 12.0f;
        }
        else _aim = _aimDirection;
        _aimDirection.Normalize();
    }
    public void _Onfire(InputAction.CallbackContext context)//�L�[���͂����邱�ƂŒe�����Ă�悤�ɂ��鏈��
    {
        if (context.started) _bFire = true;
        else if (context.canceled) _bFire = false;
    }
    private async UniTask _fire(CancellationToken token)//�e��������
    {

        if (_bFire)
        {
            if (_aimDirection == Vector2.zero || _recastFire) return;
            GameObject shot = Instantiate(_bullet, transform.position + new Vector3(0.0f, 0.2f, 0.0f),transform.rotation) as GameObject;
            //�e�̐���
            SetBulletDirection s = shot.GetComponent<SetBulletDirection>();
            //�e�̓���������ݒ�
            s._SetDirection(_shotDirection);
            _recastFire = true;
            await UniTask.Delay(100, cancellationToken: token);
            _recastFire = false;
        }
    }

    private void _SetShotDirection()//�e�̒e�����΂�����鏈��
    {
        _shotDirection = _AimDirectionRandomize(_aimDirection);
        Vector3 _aimDirectionToVector3 = (Vector3)_shotDirection;    
    }

    private Vector2 _AimDirectionRandomize(Vector2 _direction)//�L�[���͂����G�C���������w��͈͓��ł΂炯�����鏈���i�L�[���͂́}5�x�͈̔́j
    {
        float angle = Mathf.Atan2(_direction.y, _direction.x);
        float angleDegrees = angle * Mathf.Rad2Deg;

        float randomAngleDeg = Random.Range(angleDegrees - 5.0f, angleDegrees + 5.0f);
        float randomAngleRad = randomAngleDeg * Mathf.Deg2Rad;

        float x = Mathf.Cos(randomAngleRad);
        float y = Mathf.Sin(randomAngleRad);
        Vector2 _shotDirection = new Vector2(x, y);

        return _shotDirection;
    }
}
