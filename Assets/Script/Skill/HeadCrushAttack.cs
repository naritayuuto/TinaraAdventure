using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCrushAttack : ISkill
{
    public SkillType SkillType => SkillType.attack;

    string name = typeof(HeadCrushAttack).Name;

    [Tooltip("���Z����l�̔{���i���̍U���͂̉����������Z���邩���߂�l�j")]
    float _magnification = 1.5f;
    string ISkill.Name => name;
    public void Action()
    {
        int addDamage = (int)(GameManager.Instance.PlayerAttackParam.AttackDamage * _magnification);
        GameManager.Instance.PlayerAnim.AttackDamageAdd(addDamage);
    }
}
