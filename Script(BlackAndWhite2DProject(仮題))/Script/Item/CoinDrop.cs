using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinDrop : MonoBehaviour
{   

    
    [SerializeField,Header("銅コインの最小値")]
    private int _bronzeMin;
    [SerializeField, Header("銅コインの最大値")]
    private int _bronzeMax;

    
    [SerializeField, Header("銀コインの最小値")]
    private int _silverMin;
    [SerializeField, Header("銀コインの最大値")]
    private int _silverMax;

    
    [SerializeField,Header("金コインの最小値")]
    private int _goldMin;
    [SerializeField, Header("金コインの最大値")]
    private int _goldMax;

    [SerializeField] ItemData _itemData;


   

    public void _DropCoin()//コインをドロップする処理
    {
       int i= Random.Range(_bronzeMin, _bronzeMax);
        _Droper(0, i);
        i = Random.Range(_silverMin, _silverMax);
       _Droper(1, i);
        i = Random.Range(_goldMin, _goldMax);
       _Droper(2, i);
    }

    private void _Droper(int _id,int _rep)//ドロップするコインの生成
    {
        for(int i=0;i<_rep;i++)
        {
            GameObject drop = Instantiate(_itemData._ItemDatas[_id]._item, transform.position + new Vector3(0.0f, 0.2f, 0.0f), new Quaternion(0.0f,0.0f,0.0f,0.0f)) as GameObject;           
        }
      
    }
}
