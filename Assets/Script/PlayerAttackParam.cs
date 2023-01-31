using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackParam : MonoBehaviour
{
    /// <summary>player‚ÌŠî–{UŒ‚—Í</summary>
    [SerializeField]
    int _attackDamage = 500;//ŠÖ”‚Å•ÏX‚·‚éB

    int _keepAttackDamage = 0;

    int _minAttackDamage = 400;

    int _maxAttackDamage = 600;

    public int AttackDamage { get => _attackDamage; set => _attackDamage = value; }
    public int KeepAttackDamage { get => _keepAttackDamage; set => _keepAttackDamage = value; }
    public int MinAttackDamage { get => _minAttackDamage; set => _minAttackDamage = value; }
    public int MaxAttackDamage { get => _maxAttackDamage; set => _maxAttackDamage = value; }

    private void Start()
    {
        _keepAttackDamage = _attackDamage;
    }
}
