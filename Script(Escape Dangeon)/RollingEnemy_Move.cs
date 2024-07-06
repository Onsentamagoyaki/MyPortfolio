using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingEnemy_Move : MonoBehaviour,EnemyAttacked
{
    [SerializeField, Header("攻撃力")] private int _attackPower;
    // Start is called before the first frame update 
    public void PlayerDamage(Player_Move player_Move)//プレイヤーがこの敵に当たった際のダメージ処理
    {
        player_Move.Damage(_attackPower);
    }
    
}
