using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Attack : ISkill//スキルごとにダメージを変えたいためスキル分の関数を作り管理する。
{
    string name = "Attack";
    string ISkill.Name => name;
    public void Action(PlayerController player)
    {
        Debug.Log("Attack");
    }
}
