using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackJudge : MonoBehaviour
{
    int _enemyAttackDamage = 500;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player._playerHp.Damage(_enemyAttackDamage);
            player._playerAnimAndcollider.DamageAnimation();
        }
    }
}
