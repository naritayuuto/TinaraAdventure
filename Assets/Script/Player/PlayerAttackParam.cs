using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackParam : MonoBehaviour
{
    /// <summary>player‚ÌŠî–{UŒ‚—Í</summary>
    [SerializeField]
    float _attackDamage = 500;//ŠÖ”‚Å•ÏX‚·‚éB

    float _keepAttackDamage = 0;

    float _minAttackDamage;

    float _maxAttackDamage;
    [SerializeField,Header("UŒ‚”{—¦"),Tooltip("UŒ‚‚ÌÅ’á’l‚ÆÅ‘å’l‚ğŒˆ‚ß‚é‚½‚ß‚ÉA_attackDamage‚É‚©‚¯‚é”{—¦")]
    float _attackMagnification = 0.3f;

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
    /// UŒ‚‚ÌÅ’á’l‚ÆÅ‚’l‚ğŒˆ‚ß‚éŠÖ”
    /// </summary>
    public void MinAndMaxAttackDamageDecision()
    {
        _minAttackDamage = _attackDamage - (_attackDamage * _attackMagnification);
        _maxAttackDamage = _attackDamage + (_attackDamage * _attackMagnification);
    }
}
