using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Lumin;
using UnityEngine.UI;

public class Player_HP : MonoBehaviour
{

    [SerializeField, Header("HP�A�C�R��")] 
    private GameObject _playerIcon;

    private Player_Move _player_Move;
    private int _beforeHP;


    // Start is called before the first frame update
    void Start()
    {
        _player_Move = FindObjectOfType<Player_Move>();
        _beforeHP = _player_Move.GetHP();//�v���C���[���猻�݂�HP�̒l���擾
        _CreateHPIcon();//HP�A�C�R���𐶐����鏈��
    }

    private void _CreateHPIcon()//HP�A�C�R���𐶐����鏈��
    {
        for(int i=0; i<_player_Move.GetHP(); i++)
        {
            GameObject _playrerHPObj = Instantiate(_playerIcon);
            _playrerHPObj.transform.SetParent(transform);
        }
    }
    // Update is called once per frame
    void Update()
    {
        _ShowHPIcon();        
    }

    private void _ShowHPIcon()//���݂̃v���C���[��HP��\�����鏈��
    {
        if (_beforeHP == _player_Move.GetHP()) return;
        Image[] icons = transform.GetComponentsInChildren<Image>();
        for(int i = 0;i<icons.Length;i++)
        {
            icons[i].gameObject.SetActive(i < _player_Move.GetHP());
        }
        _beforeHP = _player_Move.GetHP();
    }
}
