using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerUseSkill : MonoBehaviour
{
    [Tooltip("�v���C���[���擾���Ă���X�L��")]
    List<ISkill> _skills = new List<ISkill>();
    [Tooltip("�v���C���[���g�p���悤�Ƃ��Ă���X�L��")]
    ISkill _skill = null;
    [Tooltip("_skills�̗v�f�ԍ�")]
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

    public void NextSkill()//Next�{�^�����������Ƃ�
    {
        if (_skills != null)
        {
            _skillnum++;
            _skill = _skills[_skillnum % _skills.Count];
            _skillText.text = _skill.Name;
        }

    }
    public void UseSkill()//�X�L���{�^�����������Ƃ�
    {
        if (_skill != null)
        {
            _skill.Action(GameManager.Instance._player);
        }
    }
}
