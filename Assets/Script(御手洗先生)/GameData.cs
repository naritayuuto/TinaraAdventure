using System;
using System.Collections.Generic;

public enum SelectType
{
    Skill = 1,
    Passive = 2,
    Execute = 3,
}

[Serializable]
public class SkillSelectTable
{
    public SelectType Type;
    public int TargetId;
    public string Name;
    public int Level;
    public int Probability;
}

public class GameData
{
    static public List<SkillSelectTable> SkillSelectTable = new List<SkillSelectTable>()
    {
        new SkillSelectTable(){ Type = SelectType.Skill, TargetId = 1, Name = "‰“‹——£’e", Level = 0, Probability = 80 },//Probability‚ÍŠm—¦H
        new SkillSelectTable(){ Type = SelectType.Skill, TargetId = 2, Name = "‹ß‹——£”ÍˆÍ", Level = 0, Probability = 80 },
        new SkillSelectTable(){ Type = SelectType.Passive, TargetId = 1, Name = "UŒ‚UP", Level = 0, Probability = 40 },
        new SkillSelectTable(){ Type = SelectType.Passive, TargetId = 2, Name = "‘¬“xUP", Level = 0, Probability = 20 },
        new SkillSelectTable(){ Type = SelectType.Passive, TargetId = 3, Name = "UŒ‚‘¬“xUP", Level = 5, Probability = 10 },
        new SkillSelectTable(){ Type = SelectType.Execute, TargetId = 1, Name = "‰ñ•œ", Level = 0, Probability = 90 },
        new SkillSelectTable(){ Type = SelectType.Execute, TargetId = 2, Name = "ƒS[ƒ‹ƒh", Level = 0, Probability = 40 }
    };

    static public List<int> LevelTable = new List<int>()
    {
        0, //level 1
        5, //level 1¨2
        25,//...
        60,
        120,
        200,
        300,
        400,
        500,
        600,
        700,
        800,
    };
}