using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeAttackParam : MonoBehaviour
{
    [SerializeField, Header("UŒ‚—Í"), Tooltip("UŒ‚—Í")]
    int _attackDamage = 200;

    public int AttackDamage { get => _attackDamage;}
}

