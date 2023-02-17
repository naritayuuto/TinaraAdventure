using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackJudge : MonoBehaviour
{
    [SerializeField, Header("UŒ‚—Í"), Tooltip("Enemy‚ÌUŒ‚—Í")]
    int _enemyAttackDamage = 500;
    Collider _collider = default;
    private void Start()
    {
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerHp>(out PlayerHp playerHp))
        {
            playerHp.Damage(_enemyAttackDamage);
            _collider.enabled = false;
        }
    }
}
