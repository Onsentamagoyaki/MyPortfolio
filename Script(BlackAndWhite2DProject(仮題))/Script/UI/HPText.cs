using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPText : MonoBehaviour
{
    [SerializeField]
    Text _hpText;

    float _playerHP;
    float _playerMAXHP;
    GameObject _player;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerMAXHP = _player.GetComponent<PlayerStatus>()._GetMAXHP();
    }

    // Update is called once per frame
    void Update()
    {
        _playerHP = _player.GetComponent<PlayerStatus>()._GetHP();
        _hpText.text =_playerHP.ToString()+"/"+_playerMAXHP.ToString();
    }
}
