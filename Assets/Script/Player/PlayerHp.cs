using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHp : MonoBehaviour
{
    //[SerializeField]
    //PlayerController player = null;
    [SerializeField, Header("体力の最大値"), Tooltip("playerの体力の最大値")]
    private float _playerMaxHp = 5000f;
    [SerializeField, Header("ダメージを受けて変化する体力"), Tooltip("playerの増減する体力")]
    float _playerDamageHp = 0;
    [Tooltip("攻撃無効スキルを使っている時に受けた攻撃をカウントする")]
    int _invalidCount = 0;
    [SerializeField, Tooltip("プレイヤーのHP表示用テキスト")]
    Text playerHpText = null;
    [SerializeField]
    Slider hpSlider = null;
    PlayerUseSkill _playerUseSkill = null;
    PlayerAnim _playerAnim = null;
    /// <summary> playerの増減する体力</summary>
    public float PlayerDamageHp { get => _playerDamageHp; set => _playerDamageHp = value; }
    /// <summary> playerの体力の最大値</summary>
    public float PlayerMaxHp { get => _playerMaxHp; set => _playerMaxHp = value; }
    public Slider HpSlider { get => hpSlider; set => hpSlider = value; }

    void Start()
    {
        _playerDamageHp = _playerMaxHp;
        _playerUseSkill = GameManager.Instance.PlayerUseSkill;
        _playerAnim = GameManager.Instance.PlayerAnim;
    }
    void Update()
    {
        playerHpText.text = _playerDamageHp.ToString() + "/" + _playerMaxHp.ToString();
    }
    public void Damage(int damage)
    {
        if (_playerUseSkill._invalid)
        {
            _invalidCount++;
            if (_playerUseSkill.InvalidCount <= _invalidCount)
            {
                _playerUseSkill._invalid = false;
            }
            _playerAnim.InvalidBuffAnimation(_playerUseSkill._invalid);
        }
        else
        {
            _playerDamageHp = _playerDamageHp > damage ? _playerDamageHp - damage : 0;
            HpSlider.value = _playerDamageHp / _playerMaxHp;
            if (_playerDamageHp > 0)
            {
                _playerAnim.DamageAnimation();
            }
            else
            {
                _playerAnim.Die();
                Die();
            }
            Debug.Log(_playerDamageHp);
        }
    }
    public void Die()
    {
        GameManager.Instance.FadeOUT();
    }
}
