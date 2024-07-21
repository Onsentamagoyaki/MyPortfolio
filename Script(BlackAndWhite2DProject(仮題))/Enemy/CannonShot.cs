using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class CannonShot : MonoBehaviour
{
    [SerializeField, Header("íeÇÃéÌóﬁ")]
    private GameObject _bullet;

    private bool _recastFire;

    public bool _bCannonRot;

    private Vector2 _shotDirection;

    private CancellationTokenSource _cancellationTokenSource;

    
    void Start()
    {

        _cancellationTokenSource = new CancellationTokenSource();
    }

    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
    }

    public async UniTask _fire() //íeÇåÇÇ¬èàóù
    {
        if (_recastFire) return;
        _recastFire = true;
        _bCannonRot = true;

        for (int i = 0; i < 3; i++)
        {
            if (_cancellationTokenSource.Token.IsCancellationRequested) return;

            GameObject shot = Instantiate(_bullet, transform.position, transform.rotation) as GameObject;
            SetBulletDirection s = shot.GetComponent<SetBulletDirection>();
            s._SetDirection(_shotDirection);

            try
            {
                await UniTask.Delay(300, cancellationToken: _cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                return;
            }
        }

        _bCannonRot = false;

        try
        {
            await UniTask.Delay(UnityEngine.Random.Range(1500,2000), cancellationToken: _cancellationTokenSource.Token);
        }
        catch (OperationCanceledException)
        {
            return;
        }

        _recastFire = false;
    }

    public void _SetShotDirection()//íeÇî≠éÀÇ∑ÇÈï˚å¸ÇÉZÉbÉgÇ∑ÇÈèàóù
    {
        Vector2 _difPos = transform.TransformPoint(transform.localPosition) - transform.root.position;
        _shotDirection = _difPos.normalized;
    }
}
