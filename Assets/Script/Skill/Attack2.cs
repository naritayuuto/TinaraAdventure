using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2 : ISkill
{
    string name = "Attack2";
    string ISkill.Name => name;
    public void Action(PlayerController player)
    {
        player._playerAnimAndcollider.Anim.Play(name);
        Debug.Log("Attack");
    }
}
