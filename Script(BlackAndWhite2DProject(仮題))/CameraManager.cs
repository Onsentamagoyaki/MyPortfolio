using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Vector2 _initPos;
    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _initPos = transform.position;
        _player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        _FollowPlayer();
    }

    private void _FollowPlayer()//ÉvÉåÉCÉÑÅ[Çí«ê’Ç∑ÇÈèàóù
    {
        float x = _player.transform.position.x;
        float y = _player.transform.position.y+5.0f;
        Vector3 _playerPos = new Vector3(Mathf.Clamp(x, _initPos.x, Mathf.Infinity), Mathf.Clamp(y, _initPos.y, Mathf.Infinity),transform.position.z);
        transform.position = _playerPos;
    }
}
