using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDamageCat : ISkill
{
    public  SkillType SkillType => SkillType.buff;

    string name = typeof(BuffDamageCat).Name;
    [Tooltip("クールタイム")]
    float _coolTime = 15f;
    [Tooltip("攻撃無効の回数")]
    int _invalidCount = 1;
    [Tooltip("1 = 攻撃力強化, 2 = 速度強化, 3 = 物理一回無効")]
    int _buffJobNum = 2;
    string ISkill.Name => name;

    public void Action(PlayerController player)
    {
        player._playerUseSkill.BuffUse(0, 0, _coolTime, _invalidCount, _buffJobNum);
    }
}
