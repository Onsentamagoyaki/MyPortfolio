using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowRecode : MonoBehaviour
{
    public Text _record;
  
    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<MainManager>()._bShowGameClear)
        {         
            _record.text = "Record:"+FindObjectOfType<TimeCount>()._countTime.ToString("F2");
            //ゲームクリア時のタイムを表示する処理
        }            
    }
}
