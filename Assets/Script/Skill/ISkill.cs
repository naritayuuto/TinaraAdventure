using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    heal,
    attack,
    buff
}
public interface ISkill
{
    public SkillType SkillType { get; }

    public string Name { get; }

    void Action(PlayerController player);
}
