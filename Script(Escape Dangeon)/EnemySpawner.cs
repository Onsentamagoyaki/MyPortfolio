using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField, Header("敵オブジェクト")]
    private GameObject _enemy;//スポーンさせる敵

    private Player_Move _player;
    private GameObject _enemyObj;
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player_Move>();
        _enemyObj = null;
    }

    // Update is called once per frame
    void Update()
    {
        _SpawnEnemy();//敵をスポーンさせる処理
    }

    private void _SpawnEnemy()//敵をスポーンさせる処理
    {
        if (_player == null) return;
        Vector3 playerPos = _player.transform.position;
        Vector3 cameraMaxPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        Vector3 Scale = _enemy.transform.lossyScale;
        float distance = Vector2.Distance(transform.position, new Vector2(transform.position.x, playerPos.y));
        float spawnDis = Vector2.Distance(playerPos, new Vector2(playerPos.x , cameraMaxPos.y + Scale.y / 2.0f));
        if (distance <= spawnDis && _enemyObj == null)
        {
            _enemyObj = Instantiate(_enemy);
            _enemyObj.transform.position = transform.position;
            transform.parent = _enemyObj.transform;
        }
    }
}
