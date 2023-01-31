using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerUseSkill : MonoBehaviour
{
    [Tooltip("プレイヤーが取得しているスキル")]
    List<ISkill> _skills = new List<ISkill>();
    [Tooltip("プレイヤーが使用しようとしているスキル")]
    ISkill _skill = null;
    [Tooltip("_skillsの要素番号")]
    int _skillnum = 0;
    TextMeshProUGUI _skillText = null;

    // Start is called before the first frame update
    public void AddSkill(ISkill skill)
    {
        if (_skill == null)
        {
            _skills.Add(skill);
            _skill = skill;
            _skillText.text = _skill.Name;
        }
        else
        {
            _skills.Add(skill);
            _skillnum = 0;
        }
    }

    public void NextSkill()//Nextボタンを押したとき
    {
        if (_skills != null)
        {
            _skillnum++;
            _skill = _skills[_skillnum % _skills.Count];
            _skillText.text = _skill.Name;
        }

    }
    public void UseSkill()//スキルボタンを押したとき
    {
        if (_skill != null)
        {
            _skill.Action(GameManager.Instance._player);
        }
    }
}
