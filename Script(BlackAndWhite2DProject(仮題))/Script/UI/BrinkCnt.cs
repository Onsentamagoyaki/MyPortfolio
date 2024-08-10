using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BrinkCnt : MonoBehaviour
{
    private int _brinkCnt;
    private float _brinkRecast;
    private PlayerMove _playerMove;

    [SerializeField, Header("ブリンクのアイコン")]
    private Image _brinkImage;
    // Start is called before the first frame update
    void Start()
    {
        _brinkCnt = 3;
        _brinkRecast = 0;
        _playerMove = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {    
        _brinkCnt=_playerMove._GetBrinkCnt();
        _FillBrinkIcon();
    }

    private void _FillBrinkIcon()
    {
        if (_brinkCnt == 3)
        {
            _brinkImage.fillAmount = 1.0f;
            return;
        }
        _brinkRecast += Time.deltaTime;     
        if (_brinkRecast >= 2.0f)
        {
           
            _brinkRecast = 0;
            

        }
        _brinkImage.fillAmount = _brinkRecast/ 2.0f;
    }
   
}
