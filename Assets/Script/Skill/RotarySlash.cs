using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RotarySlash : ISkill//�X�L�����ƂɃ_���[�W��ς��������߃X�L�����̊֐������Ǘ�����B
{
    public SkillType skillType => SkillType.attack;
    string name = typeof(RotarySlash).Name;
    [Tooltip("���Z����l�̔{���i���̍U���͂̉����������Z���邩���߂�l�j")]
    float _magnification = 0.3f;
    string ISkill.Name => name;
    public void Action(PlayerController player)
    {
        int addDamage = (int)(player._playerAttackParam.AttackDamage * _magnification);
        player._playerAnim.AttackDamageAdd(addDamage);
    }
}
