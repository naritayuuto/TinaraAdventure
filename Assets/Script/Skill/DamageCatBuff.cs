using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCatBuff : ISkill
{
    public  SkillType SkillType => SkillType.buff;

    string name = "SpeedBuff";
    [Tooltip("クールタイム")]
    float _coolTime = 15f;
    [Tooltip("1 = 攻撃力強化, 2 = 速度強化, 3 = 物理一回無効")]
    int _buffJobNum = 2;
    string ISkill.Name => name;

    public void Action(PlayerController player)
    {

    }
}
