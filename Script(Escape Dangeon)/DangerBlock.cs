using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerBlock : MonoBehaviour,EnemyAttacked
{
    [SerializeField, Header("攻撃力")]
    private int _attackPower;
    public void PlayerDamage(Player_Move player_Move)//ダメージ床によるダメージ処理
    {
        player_Move.Damage(_attackPower);
    }
    
}
