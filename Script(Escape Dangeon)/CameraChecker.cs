using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChecker : MonoBehaviour
{
    private enum Mode
    {
        None,
        Render,
        Renderout,
    }

    private Mode _mode;


    // Start is called before the first frame update
    void Start()
    {
        _mode= Mode.None;
    }

    // Update is called once per frame
    void Update()
    {
        _Dead();//敵が画面外にでた際にデスポーンさせる処理
    }

    private void OnWillRenderObject()//対象のオブジェクトがカメラ内に存在するかの確認
    {
        if (Camera.current.name == "Main Camera")
        {
            _mode = Mode.Render;
        }
    }

    private void _Dead()//敵が画面外にでた際にデスポーンさせる処理
    {
        Vector3 cameraMinPos = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 cameraMaxPos = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
       
        if (_mode == Mode.Renderout) 
        {
            if (transform.position.y < cameraMinPos.y - 15.0f /*|| transform.position.y > cameraMaxPos.y + 5.0f*/)
                //コメントにしているのは画面より上に行ったときにデスポーンさせる処理
            {
                Destroy(gameObject);
            }
        }
        if (_mode == Mode.Render)
        {
            _mode = Mode.Renderout;
        }
    }
}
