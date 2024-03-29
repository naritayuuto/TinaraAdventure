using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffPower : ISkill
{
    public SkillType SkillType => SkillType.buff;

    string name = typeof(BuffPower).Name;
    [Tooltip("倍率")]
    float _magnification = 1.5f;
    [Tooltip("効果時間")]
    float _buffTime = 30f;
    [Tooltip("クールタイム")]
    float _coolTime = 15f;
    [Tooltip("1 = 攻撃力強化, 2 = 速度強化, 3 = 物理一回無効")]
    int _buffJobNum = 1;
    string ISkill.Name => name;

    public void Action()
    {
        GameManager.Instance.PlayerUseSkill.BuffUse(_magnification, _buffTime, _coolTime,0,_buffJobNum);
    }
}
