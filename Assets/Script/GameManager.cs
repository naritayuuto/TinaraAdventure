using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager
{
    static private GameManager _instance = new GameManager();

    static public GameManager Instance { get => _instance;}

    public SkillManager _skillManager;

    public GameObject _player = null;


    void GetSkillManager(SkillManager s)
    {
        _skillManager = s;
    }

    void GetPlayerObject(GameObject p)
    {
        _player = p;
    }
}
