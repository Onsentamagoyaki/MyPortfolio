using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrinkCntText : MonoBehaviour
{

    [SerializeField, Header("ブリンクのテキスト")]
    private Text _brinkText;

    private PlayerMove _playerMove;
    private int _brinkCnt;

    // Start is called before the first frame update
    void Start()
    {
        _playerMove = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        _brinkCnt=3;
}

    // Update is called once per frame
    void Update()
    {
        _brinkCnt=_playerMove._GetBrinkCnt();
        _brinkText.text=_brinkCnt.ToString();
    }
}
