using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerAttack //敵にダメージを与えるオブジェクトにつけるインターフェース
{ 
    void EnemyDamage(EnemyHP _enemyHP);

}
