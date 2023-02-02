using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackParam : MonoBehaviour
{
    /// <summary>playerの基本攻撃力</summary>
    [SerializeField]
    float _attackDamage = 500;//関数で変更する。

    float _keepAttackDamage = 0;

    float _minAttackDamage = 400;

    float _maxAttackDamage = 600;

    public float AttackDamage { get => _attackDamage; set => _attackDamage = value; }
    public float KeepAttackDamage { get => _keepAttackDamage; set => _keepAttackDamage = value; }
    public float MinAttackDamage { get => _minAttackDamage; set => _minAttackDamage = value; }
    public float MaxAttackDamage { get => _maxAttackDamage; set => _maxAttackDamage = value; }

    private void Start()
    {
        _keepAttackDamage = _attackDamage;
    }
}
