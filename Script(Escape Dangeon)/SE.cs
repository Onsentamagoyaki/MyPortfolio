using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE : MonoBehaviour
{
    private AudioSource _source;

    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _PlayingEnd();
    }

    private void _PlayingEnd()//SE�̍Đ����I�����ꂽ��Ăяo����SE�I�u�W�F�N�g���폜���鏈��
    {
        if (_source.isPlaying) return;
        Destroy(gameObject);
    }
}
