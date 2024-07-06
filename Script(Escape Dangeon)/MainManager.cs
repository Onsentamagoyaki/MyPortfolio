using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;



public class MainManager : MonoBehaviour
{
    [SerializeField, Header("ゲームオーバーUI")]
    private GameObject _gameOverUI;

    private GameObject _player;

    private bool _bShowGameOver;

    [SerializeField,Header("ゲームクリアUI")]
    private GameObject _gameClearUI;

    [SerializeField, Header("タイムオーバーUI")]
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

    public void _ShowGameOverUI()//ゲームオーバー、もしくはタイムオーバーになった時の処理

    {
        if (_player!= null||_gameOverUI.activeSelf||_tooLateUI.activeSelf) return;

        if(FindObjectOfType<TimeCount>()._bTooLate) _tooLateUI.SetActive(true);
        else _gameOverUI.SetActive(true);

        GetComponent<PlayerInput>().enabled = true;
        _bShowGameOver = true;
        _bgm.Stop();
        _timer.SetActive(false);
    }

    public void OnRestart(InputAction.CallbackContext context)//リスタート処理
    {
        if(!_bShowGameOver||!context.performed) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowGameClearUI()//ゲームをクリアしたときの処理
    {
        _gameClearUI.SetActive(true);
        _bShowGameClear = true;
        Record.SetActive(true);
    }

    public void _BackToMenu(InputAction.CallbackContext context)//メインメニューに戻る処理
    {

        if((_bShowGameClear||_bShowGameOver)&&context.performed) SceneManager.LoadScene("MainMenu");

        return;

    }

}
