using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSpawn : MonoBehaviour,IHaveEvent
{
    [SerializeField]
    GameObject[] _enemys;
    public void _DoEvent()//�Z�b�g���Ă������I�u�W�F�N�g����ĂɃA�N�e�B�u�ɂ���
    {
        foreach (var e in _enemys)
        {
            e.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
