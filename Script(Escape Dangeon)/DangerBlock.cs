using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerBlock : MonoBehaviour,EnemyAttacked
{
    [SerializeField, Header("�U����")]
    private int _attackPower;
    public void PlayerDamage(Player_Move player_Move)//�_���[�W���ɂ��_���[�W����
    {
        player_Move.Damage(_attackPower);
    }
    
}
