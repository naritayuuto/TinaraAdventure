using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    [SerializeField, Header("EnemyAttackJudge‚ª•t‚¢‚Ä‚¢‚éobject"), Tooltip("")]
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
                Debug.Log("“–‚½‚è”»’èTrue");
            }
        }
    }
}
