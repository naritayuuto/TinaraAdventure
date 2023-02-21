using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RotarySlash : ISkill//スキルごとにダメージを変えたいためスキル分の関数を作り管理する。
{
    public SkillType skillType => SkillType.attack;
    string name = typeof(RotarySlash).Name;
    [Tooltip("加算する値の倍率（元の攻撃力の何割分を加算するか決める値）")]
    float _magnification = 0.3f;
    string ISkill.Name => name;
    public void Action(PlayerController player)
    {
        int addDamage = (int)(player._playerAttackParam.AttackDamage * _magnification);
        player._playerAnim.AttackDamageAdd(addDamage);
    }
}
