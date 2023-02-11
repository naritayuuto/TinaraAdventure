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
    [Tooltip("�v���C���[���g�p����X�L����")]
    TextMeshProUGUI _skillText = null;
    PlayerAttackParam _attackParam;
    [Tooltip("�o�t�X�L�����g���Ă���ꍇtrue")]
    bool _buffUse = false;
    [Tooltip("�o�t�X�L�����g������true")]
    public bool _buffCool = false;
    [SerializeField, Tooltip("�o�t����������")]
    public float _buffTimer = 0f;
    [SerializeField, Tooltip("�o�t�X�L���̃N�[���^�C��")]
    float _buffCoolTime = 0;
    private void Start()
    {
        _attackParam = GameManager.Instance._player._playerAttackParam;
    }
    private void Update()
    {
        if (_buffUse)
        {
            _buffTimer -= Time.deltaTime;
            if (_buffTimer <= 0)
            {
                _buffUse = false;
                _buffCool = true;
            }
        }
        if (_buffCool)
        {
            _buffCoolTime -= Time.deltaTime;
            if(_buffCoolTime <= 0)
            {
                _buffCool = false;
            }
        }
    }
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

    public void BuffUse(float buff, float actionTime, float coolTime, int jobNum)
    {
        if (!_buffCool)
        {
            _buffTimer = actionTime;
            _buffCoolTime = coolTime;
            switch (jobNum)
            {
                case 1:
                    {
                        _attackParam.MinAttackDamage *= buff;
                        _attackParam.MaxAttackDamage *= buff;
                        _buffUse = true;
                        break;
                    }
                case 2:
                    {
                        GameManager.Instance._player.MoveSpeed *= buff;
                        _buffUse = true;
                        break;
                    }
                case 3:
                    {

                        break;
                    }
            }
        }
    }
}
