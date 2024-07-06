using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;



public class MainManager : MonoBehaviour
{
    [SerializeField, Header("�Q�[���I�[�o�[UI")]
    private GameObject _gameOverUI;

    private GameObject _player;

    private bool _bShowGameOver;

    [SerializeField,Header("�Q�[���N���AUI")]
    private GameObject _gameClearUI;

    [SerializeField, Header("�^�C���I�[�o�[UI")]
    private GameObject _tooLateUI;

    public bool _bShowGameClear;

    [SerializeField] private GameObject Record;

    [SerializeField] private AudioSource _bgm;

    private GameObject _timer;

    

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player_Move>().gameObject;
        _bShowGameOver = false;
        _bShowGameClear= false;
        _timer = FindObjectOfType<TimeCount>().gameObject;
      

}

    // Update is called once per frame
    void Update()
    {
        _ShowGameOverUI();
    }

    public void _ShowGameOverUI()//�Q�[���I�[�o�[�A�������̓^�C���I�[�o�[�ɂȂ������̏���

    {
        if (_player!= null||_gameOverUI.activeSelf||_tooLateUI.activeSelf) return;

        if(FindObjectOfType<TimeCount>()._bTooLate) _tooLateUI.SetActive(true);
        else _gameOverUI.SetActive(true);

        GetComponent<PlayerInput>().enabled = true;
        _bShowGameOver = true;
        _bgm.Stop();
        _timer.SetActive(false);
    }

    public void OnRestart(InputAction.CallbackContext context)//���X�^�[�g����
    {
        if(!_bShowGameOver||!context.performed) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowGameClearUI()//�Q�[�����N���A�����Ƃ��̏���
    {
        _gameClearUI.SetActive(true);
        _bShowGameClear = true;
        Record.SetActive(true);
    }

    public void _BackToMenu(InputAction.CallbackContext context)//���C�����j���[�ɖ߂鏈��
    {

        if((_bShowGameClear||_bShowGameOver)&&context.performed) SceneManager.LoadScene("MainMenu");

        return;

    }

}
