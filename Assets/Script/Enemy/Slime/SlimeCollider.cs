using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCollider : MonoBehaviour
{
    [SerializeField,Header("�X���C����animator"), Tooltip("�X���C����animator")]
    Animator _anim = null;
    [SerializeField,Header("�X���C���̍U���͂������Ă���script"), Tooltip("�X���C���̍U���͂������Ă���script")]
    SlimeAttackParam _attackParam = null;
    Collider _collider = null;
    [SerializeField]
    float _colliderActiveTime = 0.5f;
    float _timer = 0;
    private void Start()
    {
        _collider = GetComponent<Collider>();
    }
    private void Update()
    {
        if(_collider.enabled)
        {
            _timer += Time.deltaTime;
            if(_timer >= _colliderActiveTime)
            {
                _timer = 0;
                _collider.enabled = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            if (other.CompareTag("Player"))
            {
                PlayerHp playerHp = other.GetComponent<PlayerHp>();
                playerHp.Damage(_attackParam.AttackDamage);
            }
        }
        _collider.enabled = false;
    }

}
