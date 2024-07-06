using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
  
{
    private Vector3 _initPos;
    private Player_Move _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player_Move>();
        _initPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_player != null)
        {
            _FollowPlayer();
        }
    }

    private void _FollowPlayer()//プレイヤーのy軸に合わせてカメラを追従させる処理
    {
       
        float y = _player.transform.position.y;
        y= Mathf.Clamp (y, _initPos.y, 72.0f);
        transform.position= new Vector3(transform.position.x, y, transform.position.z);
    }

}
