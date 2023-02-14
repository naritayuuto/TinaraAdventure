using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : ISkill
{
    public SkillType SkillType => SkillType.heal;

    string name = typeof(Heal).Name;
    string ISkill.Name => name;

    int recoveryAmount = 500;
    public void Action(PlayerController player)
    {
        if (player._playerHp.PlayerDamageHp + recoveryAmount > player._playerHp.PlayerMaxHp)
        {
            player._playerHp.PlayerDamageHp = player._playerHp.PlayerMaxHp;
        }
        else
        {
            player._playerHp.PlayerDamageHp += recoveryAmount;
        }
    }
}
