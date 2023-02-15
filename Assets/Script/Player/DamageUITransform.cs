using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUITransform : MonoBehaviour
{
    [SerializeField]
    GameObject _attackJudgeObject = null;
    Attackjudge _attackJudge = null;
    private void Start()
    {
        _attackJudge = _attackJudgeObject.GetComponent<Attackjudge>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Vector3 hitPos = other.ClosestPointOnBounds(transform.position);
            if(_attackJudge)
            {
                _attackJudgeObject.SetActive(true);
                _attackJudge.TransformMove(hitPos);
            }
        }
    }
}
