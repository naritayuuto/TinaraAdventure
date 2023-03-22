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
    const int _attackBuff = 1;
    const int _speedBuff = 2;
    const int _invalidBuff = 3;
    [Tooltip("_skills�̗v�f�ԍ�")]
    int _skillnum = 0;
    [Tooltip("�U�������o�t���g�����ꍇ�ɍU���𖳌��ɂ����")]
    int _invalidCount = 0;
    [SerializeField, Tooltip("�v���C���[���g�p����X�L������Text")]
    TextMeshProUGUI _skillText = null;
    [SerializeField, Tooltip("�o�t����������")]
    public float _buffTimer = 0f;
    [SerializeField, Tooltip("�o�t�X�L���̃N�[���^�C��")]
    float _buffCoolTime = 0;
    [Tooltip("�o�t�X�L�����g���Ă���ꍇtrue")]
    bool _buffUse = false;
    [Tooltip("�o�t�X�L���̌��ʎ��Ԃ��؂ꂽ��true")]
    bool _buffCool = false;
    [Tooltip("�U�������o�t���g�����ꍇtrue")]
    public bool _invalid = false;
    PlayerAnim _playerAnim = null;
    PlayerAttackParam _playerAttackParam = null;
    public int InvalidCount { get => _invalidCount; }

    PlayerController _player = null;
    private void Start()
    {
        _playerAnim = GameManager.Instance.PlayerAnim;
        _playerAttackParam = GameManager.Instance.PlayerAttackParam;
    }
    private void Update()
    {
        if (_buffUse)
        {
            _buffTimer -= Time.deltaTime;
            if (_buffTimer <= 0)
            {
                _buffUse = false;
            }
        }
        if (_buffCool)
        {
            _buffCoolTime -= Time.deltaTime;
            if (_buffCoolTime <= 0)
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
            SkillAnimPlay(_skill);
            _skill.Action();
        }
    }

    void SkillAnimPlay(ISkill skill)
    {
        if (skill.SkillType == SkillType.attack)
        {
            _playerAnim.Anim.Play(_skill.Name);
        }
        else
        {
            if (!_buffCool)
            {
                _playerAnim.Anim.Play(_skill.Name);
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="buff">�o�t�̔{��</param>
    /// <param name="actionTime">���ʎ���</param>
    /// <param name="coolTime">�N�[���^�C��</param>
    /// <param name="invalidCount">�U��������</param>
    /// <param name="jobNum">�o�t�̎��</param>
    /// <returns>buff = �o�t�̔{�� actionTime = ���ʎ��� coolTime = �N�[���^�C�� invalidCount = �U�������� jobNum = �o�t�̎��</returns>
    public void BuffUse(float buff, float actionTime, float coolTime, int invalidCount, int jobNum)
    {
        if (!_buffCool)
        {
            _buffTimer = actionTime;
            _buffCoolTime = coolTime;
            _buffUse = true;
            _buffCool = true;
            switch (jobNum)
            {
                case _attackBuff:
                    {
                        _playerAttackParam.MinAttackDamage *= buff;
                        _playerAttackParam.MaxAttackDamage *= buff;
                        break;
                    }
                case _speedBuff:
                    {
                        _player.MoveSpeed *= buff;
                        break;
                    }
                case _invalidBuff:
                    {
                        _invalid = true;
                        _invalidCount = invalidCount;
                        break;
                    }
            }
        }
    }
}
