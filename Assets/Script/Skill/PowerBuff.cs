using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBuff : ISkill
{
    public SkillType SkillType => SkillType.buff;

    string name = "PowerBuff";
    [Tooltip("”{—¦")]
    float _magnification = 1.5f;
    [Tooltip("Œø‰ÊŽžŠÔ")]
    float _buffTime = 30f;
    [Tooltip("1 = UŒ‚—Í‹­‰», 2 = ‘¬“x‹­‰», 3 = •¨—ˆê‰ñ–³Œø")]
    int _buffJobNum = 1;
    string ISkill.Name => name;

    public void Action(PlayerController player)
    {
        player._playerSkill.BuffUse(_magnification, _buffTime,_buffJobNum);
    }
}
