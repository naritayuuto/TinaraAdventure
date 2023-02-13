using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousAttack : ISkill
{
    public SkillType skillType => SkillType.attack;

    string name = "ContinuousAttack";

    [Tooltip("���Z����l�̔{���i���̍U���͂̉����������Z���邩���߂�l�j")]
    float _magnification = 0.7f;
    string ISkill.Name => name;
    public void Action(PlayerController player)
    {
        int addDamage = (int)(player._playerAttackParam.AttackDamage * _magnification);
        player._playerAnimAndcollider.AttackDamageAdd(addDamage);
    }
}
