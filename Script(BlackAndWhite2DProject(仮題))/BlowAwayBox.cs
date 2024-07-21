using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BlowAwayBox : MonoBehaviour
{
    [SerializeField, Header("威力")]
    private float _power;

    private Rigidbody2D _rigid;
    private CancellationTokenSource _cancellationTokenSource;
    private bool _recastBlowAway;
    private float _initPos;
    private float _limitPos;
    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _cancellationTokenSource = new CancellationTokenSource();
        _recastBlowAway = false;
        _initPos = transform.position.y;
        _limitPos = _initPos+10.0f;
    }

    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > _limitPos)//吹っ飛ばし床が限界高度を超えたら少し下げて速度を0にする
        {
            _rigid.velocity = Vector3.zero;
            transform.position = new Vector3(transform.position.x, transform.position.y - 1.0f, transform.position.z);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")//プレイヤーがふれたら吹っ飛ばす
        {
            _BlowAway(_cancellationTokenSource.Token).Forget();
        }
    }
    private async UniTask _BlowAway(CancellationToken token)//吹っ飛ばす処理
    {
        if (_recastBlowAway) return;
        _rigid.AddForce(new Vector2(0.0f, _power), ForceMode2D.Impulse);
        _recastBlowAway = true;
        await UniTask.Delay(3000, cancellationToken: token);
        _recastBlowAway = false;
    }




}
