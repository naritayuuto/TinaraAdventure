using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    [SerializeField, Header("EnemyAttackJudge���t���Ă���object"), Tooltip("")]
    EnemyAttackJudge _enemyAttackJudge = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Vector3 hitPos = other.ClosestPointOnBounds(transform.position);
            if (_enemyAttackJudge)
            {
                _enemyAttackJudge.ColliderActive();
                _enemyAttackJudge.TransformMove(hitPos);
                Debug.Log("�����蔻��True");
            }
        }
    }
}
