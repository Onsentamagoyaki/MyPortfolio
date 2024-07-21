using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CoinJudge : MonoBehaviour,GetItem
{
    [SerializeField, Header("金額")]
    private float _money;
    
    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag==("Player"))//プレイヤーに触れた際に金額をカウントして消滅する(未実装)
        {
            _GetItem();
            Destroy(transform.root.gameObject);
        }
       
    }

    public void _GetItem()
    {

    }

   

}
