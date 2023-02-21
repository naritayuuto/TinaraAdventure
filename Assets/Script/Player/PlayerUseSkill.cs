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
    [Tooltip("攻撃無効バフを使った場合に攻撃を無効にする回数")]
    int _invalidCount = 0;
    [SerializeField,Tooltip("プレイヤーが使用するスキル名のText")]
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
    public bool _invalidBuff = false;
    public int InvalidCount { get => _invalidCount;}

    PlayerController _player = null;
    private void Start()
    {
        _player = GetComponent<PlayerController>();
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
            _skill.Action(_player);
            _player._playerAnim.Anim.Play(_skill.Name);
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
    public void BuffUse(float buff, float actionTime, float coolTime,int invalidCount, int jobNum)
    {
        if (!_buffCool)
        {
            _buffTimer = actionTime;
            _buffCoolTime = coolTime;
            _buffUse = true;
            _buffCool = true;
            switch (jobNum)
            {
                case 1:
                    {
                        _player._playerAttackParam.MinAttackDamage *= buff;
                        _player._playerAttackParam.MaxAttackDamage *= buff;
                        break;
                    }
                case 2:
                    {
                        _player.MoveSpeed *= buff;
                        break;
                    }
                case 3:
                    {
                        _invalidBuff = true;
                        _invalidCount = invalidCount;
                        break;
                    }
            }
        }
    }
}
