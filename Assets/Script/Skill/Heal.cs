using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : ISkill
{
    public SkillType SkillType => SkillType.heal;

    string name = typeof(Heal).Name;
    string ISkill.Name => name;

    int recoveryAmount = 500;
    public void Action()
    {
        if (GameManager.Instance.PlayerHp.PlayerDamageHp + recoveryAmount > GameManager.Instance.PlayerHp.PlayerMaxHp)
        {
            GameManager.Instance.PlayerHp.PlayerDamageHp = GameManager.Instance.PlayerHp.PlayerMaxHp;
        }
        else
        {
            GameManager.Instance.PlayerHp.PlayerDamageHp += recoveryAmount;
        }
        GameManager.Instance.PlayerHp.HpSlider.value = GameManager.Instance.PlayerHp.PlayerDamageHp / GameManager.Instance.PlayerHp.PlayerMaxHp;
    }
}
