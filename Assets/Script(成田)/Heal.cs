using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : ISkill
{
    string name = "Heal";
    string ISkill.Name => name;
    public void Action(PlayerController player)
    {
        player.Anim.Play(name);
        if (player.Hp.PlayerDamagehp + 500 > player.Hp.PlayerMaxHp)
        {
            player.Hp.PlayerDamagehp = player.Hp.PlayerMaxHp;
        }
        else
        {
            player.Hp.PlayerDamagehp += 500;
        }
    }
}
