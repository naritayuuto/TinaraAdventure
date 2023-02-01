using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff1 : ISkill
{
    public SkillType SkillType => SkillType.buff;

    string name = "Buff1";
    string ISkill.Name => name;

    public void Action(PlayerController player)
    {
        
    }
}
