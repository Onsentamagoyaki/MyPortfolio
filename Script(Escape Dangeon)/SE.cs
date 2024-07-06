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

    private void _PlayingEnd()//SEの再生が終了されたら呼び出したSEオブジェクトを削除する処理
    {
        if (_source.isPlaying) return;
        Destroy(gameObject);
    }
}
