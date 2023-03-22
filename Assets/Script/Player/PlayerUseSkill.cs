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
    const int _attackBuff = 1;
    const int _speedBuff = 2;
    const int _invalidBuff = 3;
    [Tooltip("_skillsの要素番号")]
    int _skillnum = 0;
    [Tooltip("攻撃無効バフを使った場合に攻撃を無効にする回数")]
    int _invalidCount = 0;
    [SerializeField, Tooltip("プレイヤーが使用するスキル名のText")]
    TextMeshProUGUI _skillText = null;
    [SerializeField, Tooltip("バフが続く時間")]
    public float _buffTimer = 0f;
    [SerializeField, Tooltip("バフスキルのクールタイム")]
    float _buffCoolTime = 0;
    [Tooltip("バフスキルを使っている場合true")]
    bool _buffUse = false;
    [Tooltip("バフスキルの効果時間が切れたらtrue")]
    bool _buffCool = false;
    [Tooltip("攻撃無効バフを使った場合true")]
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
    /// <param name="buff">バフの倍率</param>
    /// <param name="actionTime">効果時間</param>
    /// <param name="coolTime">クールタイム</param>
    /// <param name="invalidCount">攻撃無効回数</param>
    /// <param name="jobNum">バフの種類</param>
    /// <returns>buff = バフの倍率 actionTime = 効果時間 coolTime = クールタイム invalidCount = 攻撃無効回数 jobNum = バフの種類</returns>
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
