using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    GameObject _player;
    [SerializeField]
    Image _HPbar;
    
    float _playerHP;
    float _playerMAXHP;
    // Start is called before the first frame update
    void Start()
    {
        _player= GameObject.FindGameObjectWithTag("Player");
        _playerMAXHP=_player.GetComponent<PlayerStatus>()._GetMAXHP();
    }

    // Update is called once per frame
    void Update()
    {
        _playerHP=_player.GetComponent<PlayerStatus>()._GetHP();
        _HPbar.fillAmount = _playerHP / _playerMAXHP;
    }
}
