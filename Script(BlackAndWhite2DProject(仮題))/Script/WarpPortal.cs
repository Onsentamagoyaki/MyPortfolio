using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPortal : MonoBehaviour
{
    [SerializeField]
    GameObject _exit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")//�v���C���[�����̃I�u�W�F�N�g�ɐG�ꂽ��_exit�̍��W�փ��[�v����
        {
            collision.gameObject.transform.position=_exit.transform.position;
        }
    }
}
