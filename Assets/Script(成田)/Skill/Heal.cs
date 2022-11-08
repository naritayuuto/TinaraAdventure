using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : ISkill
{
    public SkillType SkillType => SkillType.heal;

    string name = "Heal";
    string ISkill.Name => name;

    int recoveryAmount = 500;
    public void Action(PlayerController player)
    {
        player.Anim.Play(name);
        if (player.Hp.PlayerDamagehp + recoveryAmount > player.Hp.PlayerMaxHp)
        {
            player.Hp.PlayerDamagehp = player.Hp.PlayerMaxHp;
        }
        else
        {
            player.Hp.PlayerDamagehp += recoveryAmount;
        }
    }
}
