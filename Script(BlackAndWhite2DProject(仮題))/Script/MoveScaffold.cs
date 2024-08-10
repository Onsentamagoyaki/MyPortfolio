using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;

using System.Threading;
using UnityEngine;
public class MoveScaffold : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed;

    [SerializeField, Header("���[�h 1:����@2����")]
    private int _mode;

    [SerializeField, Header("�J�n�ʒu")]
    private int _start;

    [SerializeField]
    GameObject []_pointObj;

    private Vector3[] _pointList;
    private int _count;
    private int _index;
    private float[] _dis;
    private float[] _revdis;
    


    // Start is called before the first frame update
    void Start()
    {


        _count = _pointObj.Length;    
        Array.Resize(ref _pointList, _count);
        Array.Resize(ref _dis, _count);
        Array.Resize(ref _revdis, _count);
        _SetPoint();//_pointObj�̍��W��e�I�u�W�F�N�g���Ƃ̋������擾����       
        transform.position =_pointList[_start];
        if (_start + 1 == _count) _index = 0;
        else _index = _start + 1;
        _Move();//�ړ����鏰�̓����������鏈��
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void _SetPoint()//_pointObj�̍��W��e�I�u�W�F�N�g���Ƃ̋������擾���� 
    {
        for(int i = 0; i < _count; i++)
        {
            _pointList[i] = _pointObj[i].transform.position;//transform.GetChild(i).position;          
        }
        for (int i = 1; i < _count; i++)
        {
            _dis[i] = Vector3.Distance(_pointList[i], _pointList[i-1]);           
        }
        _dis[0] = Vector3.Distance(_pointList[_count-1], _pointList[0]);
        for(int i = 0; i < _count-1; i++)
        {
            _revdis[i]= Vector3.Distance(_pointList[i], _pointList[i + 1]);
        }       
        for (int i = 0; i < _count ; i++)
        {
            //Destroy(transform.GetChild(i).gameObject);
            Destroy(_pointObj[i].gameObject);
        }

    }
  
    private async void _Move()
    {
        switch (_mode)
        {
            case 1://�������񂷂�ꍇ�̏���
                while (true)
                {

                    await transform.DOMove(_pointList[_index], Mathf.Abs(_dis[_index]) / _moveSpeed).SetEase(Ease.Linear);
                    _index++;
                    if (_index == _count) _index = 0;
                }
            
            case 2://������������ꍇ�̏���
                bool _shift = false;
                while (true)
                {
                    if (_shift)
                    {
                        await transform.DOMove(_pointList[_index], Mathf.Abs(_revdis[_index]) / _moveSpeed).SetEase(Ease.Linear);
                        _index--;
                    }
                        
                    
                    else 
                    {
                        await transform.DOMove(_pointList[_index], Mathf.Abs(_dis[_index]) / _moveSpeed).SetEase(Ease.Linear);
                        _index++;
                    }

                    if (_index == _count)
                    {
                        _index -= 2;
                        _shift = true;
                    }
                    else if(_index == -1){
                        _index = 1;
                        _shift = false;
                    }
                }

                
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)//�ړ����鏰�ɏ�������̏���
    {
        if (collision.gameObject.tag == "Player")
        {
            bool _touchUP=false;
            float _minPos =Mathf.Infinity;
            foreach(ContactPoint2D point in collision.contacts)
            {
                _minPos=MathF.Min(_minPos, point.point.y);
                
            }
            Debug.Log(_minPos);
            Debug.Log(transform.position.y);
            if (transform.position.y-0.2f< _minPos) _touchUP = true;
            Debug.Log(_touchUP);
            if (_touchUP)collision.transform.SetParent(transform);

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")collision.transform.SetParent(null);//�ړ����鏰���痣�ꂽ�Ƃ��̏���

    }
}
