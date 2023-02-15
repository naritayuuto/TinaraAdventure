using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUITransform : MonoBehaviour
{
    [SerializeField]
    Attackjudge _attackJudge = null;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Vector3 hitPos = other.ClosestPointOnBounds(transform.position);
            if(_attackJudge)
            {
                _attackJudge.ColliderActive();
                _attackJudge.TransformMove(hitPos);
            }
        }
    }
}
