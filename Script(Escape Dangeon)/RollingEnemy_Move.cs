using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingEnemy_Move : MonoBehaviour,EnemyAttacked
{
    [SerializeField, Header("�U����")] private int _attackPower;
    // Start is called before the first frame update 
    public void PlayerDamage(Player_Move player_Move)//�v���C���[�����̓G�ɓ��������ۂ̃_���[�W����
    {
        player_Move.Damage(_attackPower);
    }
    
}
