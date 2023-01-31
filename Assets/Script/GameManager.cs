using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager
{
    static private GameManager _instance = new GameManager();

    static public GameManager Instance { get => _instance;}

    public SkillManager _skillManager;

    public PlayerController _player = null;


    public void GetSkillManager(SkillManager s)
    {
        _skillManager = s;
    }

    public void GetPlayerObject(PlayerController p)
    {
        _player = p;
    }
}
