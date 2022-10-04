using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Skilltree//スキルごとにダメージを変えたいためスキル分の関数を作り管理する。
{
    public override void SkillAction()
    {
        Player.AttackDamage += 500;
    }
}
