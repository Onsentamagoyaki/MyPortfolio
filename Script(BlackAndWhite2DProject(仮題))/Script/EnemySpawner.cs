using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField, Header("�X�|�[������G")]
    private GameObject _enemy;

    private bool _bSpawn;

    // Start is called before the first frame update
    void Start()
    {
        _bSpawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_bSpawn)
        {
            GameObject enemy=Instantiate(_enemy, transform.position, transform.rotation) as GameObject;
            Destroy(gameObject);//�X�|�[���ケ�̃I�u�W�F�N�g�͍폜����
        }
    }
    private void OnWillRenderObject()//�X�|�i�[���f������X�|�[��������
    {
       
        //���C���J�����ɉf����������_isRendered��L����
        if (Camera.current.tag == "MainCamera")
        {
            _bSpawn=true;
        }
    }
    
}
