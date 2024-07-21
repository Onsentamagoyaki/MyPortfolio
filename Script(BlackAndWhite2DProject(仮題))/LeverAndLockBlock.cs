using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverAndLockBlock : MonoBehaviour, IHaveEvent
{   
    [SerializeField]
    private GameObject[] _lockBlock;
   
    public void _DoEvent()
    {
        transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));

        foreach (GameObject child in _lockBlock)//全ての子オブジェクト(lockBlock)を削除する
        {
            Destroy(child.gameObject);
        }
        gameObject.layer = LayerMask.NameToLayer("NonTouchPlayer");//1度しか実行できないようにプレイヤーが触れられないレイヤに移動する
        Destroy(GetComponent<LeverAndLockBlock>());//実行後このコンポーネントを削除
        
    }
    
}
