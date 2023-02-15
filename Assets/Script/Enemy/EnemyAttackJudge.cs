using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackJudge : MonoBehaviour
{
    [SerializeField, Header("�U����"), Tooltip("Enemy�̍U����")]
    int _enemyAttackDamage = 500;
    [Tooltip("����I�ɓ����蔻��������������s�����߂̃^�C�}�[")]
    float _timer = 0;
    [Tooltip("�����蔻����ɕW�I�������Ă��Ȃ��ꍇ�ɓ����蔻��������b��")]
    float _colliderActiveTime = 0.5f;
    Collider _collider = default;
    Vector3 _iTransform;
    private void Start()
    {
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
    }
    private void Update()
    {
        if (_collider.enabled == true)
        {
            _timer += Time.deltaTime;
            if (_timer >= _colliderActiveTime)
            {
                _collider.enabled = false;
                _timer = 0;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerHp>(out PlayerHp playerHp))
        {
            playerHp.Damage(_enemyAttackDamage);
        }
        transform.position = _iTransform;
        _collider.enabled = false;
    }

    public void TransformMove(Vector3 pos)
    {
        _iTransform = transform.position;
        transform.position = pos;
    }

    public void ColliderActive()
    {
        _collider.enabled = true; 
    }
}
