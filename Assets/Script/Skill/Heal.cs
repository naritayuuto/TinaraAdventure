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
        player._playerAnimAndcollider.Anim.Play(name);
        if (player._playerHp.PlayerDamagehp + recoveryAmount > player._playerHp.PlayerMaxHp)
        {
            player._playerHp.PlayerDamagehp = player._playerHp.PlayerMaxHp;
        }
        else
        {
            player._playerHp.PlayerDamagehp += recoveryAmount;
        }
    }
}
