using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnActiveEventArea : MonoBehaviour
{
    [SerializeField,Header("1度だけ使う")]
    private bool _once;
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
        if (collision.gameObject.tag == "Player")//プレイヤーがEventAreaに入った場合に_DoEvent()を起動させる
        {
            GetComponent<IHaveEvent>()._DoEvent();
            if(_once)Destroy(gameObject);
        } 
    }
}
