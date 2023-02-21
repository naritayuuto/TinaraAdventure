using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDamageCat : ISkill
{
    public  SkillType SkillType => SkillType.buff;

    string name = typeof(BuffDamageCat).Name;
    [Tooltip("�N�[���^�C��")]
    float _coolTime = 15f;
    [Tooltip("�U�������̉�")]
    int _invalidCount = 1;
    [Tooltip("1 = �U���͋���, 2 = ���x����, 3 = ������񖳌�")]
    int _buffJobNum = 2;
    string ISkill.Name => name;

    public void Action(PlayerController player)
    {
        player._playerUseSkill.BuffUse(0, 0, _coolTime, _invalidCount, _buffJobNum);
    }
}
