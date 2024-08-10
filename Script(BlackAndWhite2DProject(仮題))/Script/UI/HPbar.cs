using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    PlayerStatus _playerStatus;
    [SerializeField]
    Image _HPbar;
    
    float _playerHP;
    float _playerMAXHP;
    // Start is called before the first frame update
    void Start()
    {
        _playerStatus= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        _playerMAXHP=_playerStatus._GetMAXHP();
    }

    // Update is called once per frame
    void Update()
    {
        _playerHP=_playerStatus._GetHP();
        _HPbar.fillAmount = _playerHP / _playerMAXHP;
    }
}
