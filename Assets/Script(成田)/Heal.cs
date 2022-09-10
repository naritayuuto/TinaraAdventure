using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Skilltree
{
    public override void SkillAction()
    {
        Player.Playerhp += 500;
    }
}
