using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpeed : ISkill
{
    public SkillType SkillType => SkillType.buff;

    string name = typeof(BuffSpeed).Name;
    [Tooltip("�{��")]
    float _magnification = 1.5f;
    [Tooltip("���ʎ���")]
    float _buffTime = 30f;
    [Tooltip("�N�[���^�C��")]
    float _coolTime = 15f;
    [Tooltip("1 = �U���͋���, 2 = ���x����, 3 = ������񖳌�")]
    int _buffJobNum = 2;
    string ISkill.Name => name;

    public void Action(PlayerController player)
    {
        player._playerSkill.BuffUse(_magnification, _buffTime, _coolTime, _buffJobNum);
    }
}
