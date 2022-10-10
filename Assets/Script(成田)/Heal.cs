using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Skilltree
{
    public override void SkillAction()
    {
        if (Playerhp.PlayerDamagehp >= Playerhp.PlayerHp)
        {
            Playerhp.PlayerDamagehp += 500;
        }
        else
        {
            Playerhp.PlayerDamagehp = Playerhp.PlayerHp;
        }
    }
}
