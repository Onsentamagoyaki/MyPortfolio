using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Move : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float _moveSpeed;  
        
    [SerializeField, Header("ジャンプ速度(地上)")]
    private float _jumpSpeedGround;

    [SerializeField, Header("無敵時間")] 
    private float _damageTime;

    [SerializeField, Header("点滅時間")] 
    private float _flashTime;

    [SerializeField, Header("体力")]
    private int _hp;

    [SerializeField, Header("ジャンプSE")]
    private GameObject _jumpSE;

    [SerializeField, Header("ダメージSE")]
    private GameObject _damageSE;

    private bool _bJump;
    private Vector2 _inputDirection;//移動方向
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigid;
    private int _jumpCount;
    private float _countJumpTime;
    private bool _recastJump;
    PlayerInput _input;
    // Start is called before the first frame update
    void Start()
    {
        _rigid=GetComponent<Rigidbody2D>();
        _bJump=false;
        _spriteRenderer = GetComponent<SpriteRenderer>();  
        _jumpCount =2;
        
    }

    // Update is called once per frame
    void Update()
    {
        _Move();//移動処理
        _IsGround();//地面との接地判定の処理
                    //
        if (_recastJump) _countJumpTime += Time.deltaTime;
        //ジャンプのリキャスト処理（0.5秒間地面との設置判定のboxcastを飛ばさないようにする)
        Debug.Log(_rigid.velocity.y);
        if (_countJumpTime > 0.5f)
        {
            _recastJump = false;
            _countJumpTime = 0.0f;
        }
    }     

    private void _Move()//移動処理
    {
        _rigid.velocity = new Vector2(_inputDirection.x * _moveSpeed, _rigid.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
             _HitEnemy(collision.gameObject);//敵との衝突処理
            gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
            StartCoroutine(_Damage());
        }

        if (collision.gameObject.tag == "MoveScaffold") transform.SetParent(collision.gameObject.transform);//移動する床との衝突判定

        if(collision.gameObject.tag =="Goal")//ゴールとの衝突判定
        {
            FindObjectOfType<MainManager>().ShowGameClearUI();
            enabled = false;
            GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "MoveScaffold") transform.SetParent(null);//移動する床から離れたときの処理

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            _HitEnemy(collision.gameObject);//敵との衝突処理
            gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
            StartCoroutine(_Damage());
        }
    }

    private void _HitEnemy(GameObject enemy)//敵との衝突処理
    {
            enemy.GetComponent<EnemyAttacked>().PlayerDamage(this);
        }

    IEnumerator _Damage()//ダメージを受けたときのプレイヤーの点滅処理
    {
        Color color = _spriteRenderer.color;
        for (int i = 0; i < _damageTime; i++)
        {
            yield return new WaitForSeconds(_flashTime);
            _spriteRenderer.color = new Color(color.r, color.g, color.b, 0.0f);

            yield return new WaitForSeconds(_flashTime);
            _spriteRenderer.color = new Color(color.r, color.g, color.b, 1.0f);
        }
        _spriteRenderer.color = color;
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
    private void _Dead()//プレイヤーが死んだときの処理
    {
        if (_hp <= 0)
        {
            Destroy(gameObject );
        }
    }

    public void _OnMove(InputAction.CallbackContext context)//キー入力から移動方向を取得する処理
    {
        _inputDirection= context.ReadValue<Vector2>();     
    }

    public void _OnJump(InputAction.CallbackContext context)//キー入力からジャンプをする処理
    {
        if(!context.performed|| _bJump) return;
        Instantiate(_jumpSE); 
        _rigid.velocity = Vector2.zero;
        _rigid.AddForce(Vector2.up*_jumpSpeedGround,ForceMode2D.Impulse);
        _jumpCount--;
        _recastJump = true;
        if (_jumpCount == 0)
        {
            _bJump = true;          
        }
        
    }

    public void Damage(int damage)//プレイヤーがダメージを受けたときの処理
    {
        Instantiate(_damageSE);
        _hp = Mathf.Max(_hp - damage, 0 );
        _Dead ();

    }

    public int GetHP()//プレイヤーの現在のHPを取得する処理
    {
        return _hp;
    } 

    private void _IsGround()//地面との接地判定の処理
    {
        if (_recastJump) return;
        RaycastHit2D hit = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y - (transform.localScale.y / 2.0f)),
        transform.localScale/2.0f,0.0f,Vector2.down,distance:0.01f,1<<6);

        Debug.Log(hit.collider);
        if (hit.collider&&Mathf.Abs(_rigid.velocity.y)<=0.0005f)
        {
            _bJump = false;
            _jumpCount = 2;
        }
    }   
}
