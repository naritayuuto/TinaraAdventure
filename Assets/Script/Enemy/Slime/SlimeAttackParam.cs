using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeAttackParam : MonoBehaviour
{
    [SerializeField, Header("�U����"), Tooltip("�U����")]
    int _attackDamage = 200;

    public int AttackDamage { get => _attackDamage;}
}

