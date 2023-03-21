using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousAttack : ISkill
{
    public SkillType SkillType => SkillType.attack;

    string name = typeof(ContinuousAttack).Name;

    [Tooltip("‰ÁŽZ‚·‚é’l‚Ì”{—¦iŒ³‚ÌUŒ‚—Í‚Ì‰½Š„•ª‚ð‰ÁŽZ‚·‚é‚©Œˆ‚ß‚é’lj")]
    float _magnification = 0.7f;
    string ISkill.Name => name;
    public void Action(PlayerController player)
    {
        int addDamage = (int)(player._playerAttackParam.AttackDamage * _magnification);
        player._playerAnim.AttackDamageAdd(addDamage);
    }
}
