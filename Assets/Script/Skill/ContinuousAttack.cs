using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousAttack : ISkill
{
    public SkillType skillType => SkillType.attack;

    string name = typeof(ContinuousAttack).Name;

    [Tooltip("加算する値の倍率（元の攻撃力の何割分を加算するか決める値）")]
    float _magnification = 0.7f;
    string ISkill.Name => name;
    public void Action(PlayerController player)
    {
        int addDamage = (int)(player._playerAttackParam.AttackDamage * _magnification);
        player._playerAnimAndcollider.AttackDamageAdd(addDamage);
    }
}
