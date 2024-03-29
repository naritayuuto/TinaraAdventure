using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCrushAttack : ISkill
{
    public SkillType SkillType => SkillType.attack;

    string name = typeof(HeadCrushAttack).Name;

    [Tooltip("加算する値の倍率（元の攻撃力の何割分を加算するか決める値）")]
    float _magnification = 1.5f;
    string ISkill.Name => name;
    public void Action()
    {
        int addDamage = (int)(GameManager.Instance.PlayerAttackParam.AttackDamage * _magnification);
        GameManager.Instance.PlayerAnim.AttackDamageAdd(addDamage);
    }
}
