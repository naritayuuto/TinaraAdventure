using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCatBuff : ISkill
{
    public  SkillType SkillType => SkillType.buff;

    string name = "SpeedBuff";
    [Tooltip("�N�[���^�C��")]
    float _coolTime = 15f;
    [Tooltip("1 = �U���͋���, 2 = ���x����, 3 = ������񖳌�")]
    int _buffJobNum = 2;
    string ISkill.Name => name;

    public void Action(PlayerController player)
    {

    }
}