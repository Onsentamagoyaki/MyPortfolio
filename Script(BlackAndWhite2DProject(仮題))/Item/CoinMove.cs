using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMove : MonoBehaviour
{
    private Rigidbody2D _rigid;

    private float _countTime;

    // Start is called before the first frame update
    void Start()
    {
        _countTime = 0.0f;
        _rigid = GetComponent<Rigidbody2D>();
        _drop();

    }

    // Update is called once per frame
    void Update()
    {
        if (_countTime>10.0f) Destroy(gameObject);
        _countTime += Time.deltaTime;
    }

    public void _drop()//コインがドロップした際に散らばる用にする処理
    {
        float randomAngleDeg = Random.Range(80.0f, 100.0f);
        float randomAngleRad = randomAngleDeg * Mathf.Deg2Rad;
        float x = Mathf.Cos(randomAngleRad);
        float y = Mathf.Sin(randomAngleRad);
        Vector2 _dropDirection = new Vector2(x, y);
        float _dropPower = Random.Range(600.0f, 800.0f);
        _rigid.AddForce(_dropDirection * _dropPower);

    }
}

