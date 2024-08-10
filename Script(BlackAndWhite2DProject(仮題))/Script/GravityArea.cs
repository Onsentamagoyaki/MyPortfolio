using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityArea : MonoBehaviour
{
    [SerializeField]
    private Vector2 _gravityDir;

    [SerializeField]
    private float _gravityPower;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {        
       if(collision.gameObject.TryGetComponent(out Rigidbody2D rb2D))//�G���A�ɓ������I�u�W�F�N�g��Rigidbody2D�������Ă����ꍇ�w�肵�������ɗ͂�^����
       {
            _AddGravity(rb2D);
       }       
    }

    private void _AddGravity(Rigidbody2D _obj)
    {
        _obj.AddForce(_gravityDir * _gravityPower);
    }
}
