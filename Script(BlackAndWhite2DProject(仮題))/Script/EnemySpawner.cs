using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField, Header("スポーンする敵")]
    private GameObject _enemy;

    private bool _bSpawn;

    // Start is called before the first frame update
    void Start()
    {
        _bSpawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_bSpawn)
        {
            GameObject enemy=Instantiate(_enemy, transform.position, transform.rotation) as GameObject;
            Destroy(gameObject);//スポーン後このオブジェクトは削除する
        }
    }
    private void OnWillRenderObject()//スポナーが映ったらスポーンさせる
    {
       
        //メインカメラに映った時だけ_isRenderedを有効に
        if (Camera.current.tag == "MainCamera")
        {
            _bSpawn=true;
        }
    }
    
}
