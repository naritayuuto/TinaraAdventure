using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff1 : ISkill
{
    public SkillType SkillType => SkillType.buff;

    string name = "Buff1";

    float _attackBuff = 1.5f;

    float _buffTime = 30f;
    [Tooltip("1 = 攻撃力強化, 2 = 速度強化, 3 = 物理一回無効")]
    int _buffJobNum = 1;
    string ISkill.Name => name;

    public void Action(PlayerController player)
    {
        player._playerSkill.BuffUse(_attackBuff,_buffTime,_buffJobNum);
    }
}
