using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{

    [SerializeField, Header("移動速度")]
    private float _moveSpeed;

    [SerializeField, Header("ジャンプ速度")]
    private float _jumpSpeed;

    private Rigidbody2D _rigid;
    private CancellationTokenSource _cancellationTokenSource;
    private Vector2 _moveDirection;//移動方向
    private Vector2 _brinkDirection;
    private float _brinkLen=6.0f;
    private bool _bJump;
    private bool _recastJump;//ジャンプのリキャスト
    private int _brinkCnt;
    private float _brinkRecast;


    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _cancellationTokenSource = new CancellationTokenSource();
        _bJump = false;
        _recastJump = false;
        _brinkCnt = 3;
        _brinkRecast = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _IsGround();
        _ReloadBrink();
        //Vector3 brink = _brinkDirection * _brinkLen;
        // Debug.DrawRay(transform.position, brink, Color.green);
    }

    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
    }
    private void _Move()//移動処理
    {
        _rigid.velocity = new Vector2(_moveDirection.x * _moveSpeed, _rigid.velocity.y);
    }

    public void _OnMove(InputAction.CallbackContext context)//キー入力から移動方向を読み取る処理
    {
        _moveDirection = context.ReadValue<Vector2>();
        _brinkDirection = _SetBrinkDirection(_moveDirection);
        _moveDirection.Normalize();
    }
    private Vector2 _SetBrinkDirection(Vector2 _getDirection)//キー入力からワープ方向を読み取る処理
    {
        Vector2 _brinkDirection = Vector2.zero;
        if (_getDirection.magnitude < 0.2) return _brinkDirection;
        if (_getDirection.x > 0) _brinkDirection = new Vector2(1.0f, 0.0f);
        else if (_getDirection.x < 0) _brinkDirection = new Vector2(-1.0f, 0.0f);

        return _brinkDirection;
    }
    public void _OnBrink(InputAction.CallbackContext context)//ブリンク（移動スキル）の処理
    {
        if (!context.performed || _brinkDirection == Vector2.zero||_brinkCnt<=0) return;

        RaycastHit2D hit = Physics2D.BoxCast(transform.position,new Vector3(1.0f,1.0f,1.0f),0.0f, _brinkDirection, _brinkLen, 1 << 10);//layer 10 ステージの足場やブロックのレイヤ

        if (hit.collider)//レイキャストが何かしらの足場ブロックにあたったらそこへ移動する
        {
            transform.position = hit.point;
        }

        else//あたらなかった場合レイキャストの先端の座標へ移動
        {
            transform.position += _brinkLen * (Vector3)_brinkDirection;     
        }
        _brinkCnt--;
    }

    private void _ReloadBrink()
    {
        if (_brinkCnt==3) return;
        _brinkRecast += Time.deltaTime;
        if (_brinkRecast >= 2.0f)
        {
            _brinkCnt++;
            _brinkRecast = 0;
        }
    }

    public int _GetBrinkCnt()
    {
        return _brinkCnt;
    }

    public void _OnJump(InputAction.CallbackContext context)//キー入力でジャンプする処理
    {
        if (!context.performed || _bJump) return;
        _Jump(_cancellationTokenSource.Token).Forget();      
    }
    
    private async UniTask _Jump(CancellationToken token)//ジャンプの処理
    {
        _rigid.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
        _bJump = true;
        _recastJump = true;
        await UniTask.Delay(100, cancellationToken: token);
        _recastJump = false;
    }

    private void _IsGround()//地面との設置判定をとる処理
    {
        if (_recastJump) return;
        RaycastHit2D hit = Physics2D.BoxCast(transform.position,new Vector2(0.8f, 0.5f), 0.0f, Vector2.down, distance: 0.9f, (1 << 10) + (1 << 13));//layer 10 ステージの足場やブロックのレイヤ　13 すり抜ける床のレイヤ
      
        if (hit.collider && Mathf.Abs(_rigid.velocity.y) <= 0.05f)
        {
            _bJump = false;
        }
        else _bJump = true;

    }
    public void _OnSlipThrough(InputAction.CallbackContext context)//プレイヤーがすり抜ける床に乗った際に特定のキー入力ですり抜けるようにする処理
    {
        if (!context.performed) return;
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(0.5f, 0.5f), 0.0f, Vector2.down, distance: 0.9f, 1 << 13);//layer 13 すり抜ける床のレイヤ

        if (hit.collider == null) return;
        if (hit.collider.tag == "OnSlipThrough")
        {
            transform.position -= new Vector3(0.0f, 2.0f, 0.0f);
        }
    }

    public void _OnEvent(InputAction.CallbackContext context)//IHaveEventのインターフェースを持つオブジェクトの近くで特定のキーを押すとアクションを起こす処理
    {
        
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 3.0f, Vector2.down,0.01f,1<<16);//layer 16 イベントアクションを持つオブジェクトのレイヤ
        Debug.Log(hit.collider);
        if (hit.collider)
        {
            hit.collider.gameObject.GetComponent<IHaveEvent>()._DoEvent();
        }
    }

   
}
