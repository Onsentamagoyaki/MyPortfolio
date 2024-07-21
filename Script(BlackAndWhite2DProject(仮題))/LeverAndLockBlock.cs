using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverAndLockBlock : MonoBehaviour, IHaveEvent
{   
    [SerializeField]
    private GameObject[] _lockBlock;
   
    public void _DoEvent()
    {
        transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));

        foreach (GameObject child in _lockBlock)//�S�Ă̎q�I�u�W�F�N�g(lockBlock)���폜����
        {
            Destroy(child.gameObject);
        }
        gameObject.layer = LayerMask.NameToLayer("NonTouchPlayer");//1�x�������s�ł��Ȃ��悤�Ƀv���C���[���G����Ȃ����C���Ɉړ�����
        Destroy(GetComponent<LeverAndLockBlock>());//���s�ケ�̃R���|�[�l���g���폜
        
    }
    
}
