using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffPower : ISkill
{
    public SkillType SkillType => SkillType.buff;

    string name = typeof(BuffPower).Name;
    [Tooltip("�{��")]
    float _magnification = 1.5f;
    [Tooltip("���ʎ���")]
    float _buffTime = 30f;
    [Tooltip("�N�[���^�C��")]
    float _coolTime = 15f;
    [Tooltip("1 = �U���͋���, 2 = ���x����, 3 = ������񖳌�")]
    int _buffJobNum = 1;
    string ISkill.Name => name;

    public void Action(PlayerController player)
    {
        player._playerUseSkill.BuffUse(_magnification, _buffTime, _coolTime,0,_buffJobNum);
    }
}
