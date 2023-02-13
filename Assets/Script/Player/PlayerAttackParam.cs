using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackParam : MonoBehaviour
{
    /// <summary>player�̊�{�U����</summary>
    [SerializeField]
    float _attackDamage = 500;//�֐��ŕύX����B

    float _keepAttackDamage = 0;

    float _minAttackDamage;

    float _maxAttackDamage;
    [Tooltip("�U���̍Œ�l�ƍő�l�����߂邽�߂ɁA_attackDamage�ɂ�����{��")]
    float _attackMagnification = 0.5f;

    public float AttackDamage { get => _attackDamage; set => _attackDamage = value; }
    public float KeepAttackDamage { get => _keepAttackDamage; set => _keepAttackDamage = value; }
    public float MinAttackDamage { get => _minAttackDamage; set => _minAttackDamage = value; }
    public float MaxAttackDamage { get => _maxAttackDamage; set => _maxAttackDamage = value; }

    private void Start()
    {
        _keepAttackDamage = _attackDamage;
        MinAndMaxAttackDamageDecision();
    }
    /// <summary>
    /// �U���̍Œ�l�ƍō��l�����߂�֐�
    /// </summary>
    public void MinAndMaxAttackDamageDecision()
    {
        _minAttackDamage = _attackDamage - (_attackDamage * _attackMagnification);
        _maxAttackDamage = _attackDamage + (_attackDamage * _attackMagnification);
    }
}
