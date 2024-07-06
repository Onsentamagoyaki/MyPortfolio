using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChecker : MonoBehaviour
{
    private enum Mode
    {
        None,
        Render,
        Renderout,
    }

    private Mode _mode;


    // Start is called before the first frame update
    void Start()
    {
        _mode= Mode.None;
    }

    // Update is called once per frame
    void Update()
    {
        _Dead();//�G����ʊO�ɂł��ۂɃf�X�|�[�������鏈��
    }

    private void OnWillRenderObject()//�Ώۂ̃I�u�W�F�N�g���J�������ɑ��݂��邩�̊m�F
    {
        if (Camera.current.name == "Main Camera")
        {
            _mode = Mode.Render;
        }
    }

    private void _Dead()//�G����ʊO�ɂł��ۂɃf�X�|�[�������鏈��
    {
        Vector3 cameraMinPos = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 cameraMaxPos = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
       
        if (_mode == Mode.Renderout) 
        {
            if (transform.position.y < cameraMinPos.y - 15.0f /*|| transform.position.y > cameraMaxPos.y + 5.0f*/)
                //�R�����g�ɂ��Ă���͉̂�ʂ���ɍs�����Ƃ��Ƀf�X�|�[�������鏈��
            {
                Destroy(gameObject);
            }
        }
        if (_mode == Mode.Render)
        {
            _mode = Mode.Renderout;
        }
    }
}
