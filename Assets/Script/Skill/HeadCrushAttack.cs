using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCrushAttack : ISkill
{
    public SkillType SkillType => SkillType.attack;

    string name = typeof(HeadCrushAttack).Name;

    [Tooltip("‰ÁŽZ‚·‚é’l‚Ì”{—¦iŒ³‚ÌUŒ‚—Í‚Ì‰½Š„•ª‚ð‰ÁŽZ‚·‚é‚©Œˆ‚ß‚é’lj")]
    float _magnification = 1.5f;
    string ISkill.Name => name;
    public void Action()
    {
        int addDamage = (int)(GameManager.Instance.PlayerAttackParam.AttackDamage * _magnification);
        GameManager.Instance.PlayerAnim.AttackDamageAdd(addDamage);
    }
}
