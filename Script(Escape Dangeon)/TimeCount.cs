using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using unityroom.Api;


public class TimeCount : MonoBehaviour
{
    public float _countTime;

    private bool _bCount;

    public Text time;

    private GameObject _player;

    public bool _bTooLate;

    

    // Start is called before the first frame update
    void Start()
    {
        _countTime = 0.0f;
        _bCount = true;
        _player = FindObjectOfType<Player_Move>().gameObject;
        _bTooLate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<MainManager>()._bShowGameClear)
        {
            UnityroomApiClient.Instance.SendScore(1, _countTime, ScoreboardWriteMode.HighScoreAsc);
            //上はゲームクリア時のタイムをunityroomのランキングに送る処理（windowsでは実装しないように）
            _bCount = false;
            time.enabled = false;
        }

        if( _bCount)
        {
            _countTime += Time.deltaTime;
            
            time.text ="Time:"+ _countTime.ToString("F2");
        }

        if(_countTime > 180.0f)//タイムオーバーになったらプレイヤーを消す
        {
            _bTooLate = true;
            Destroy(_player);

        }
    }
}
