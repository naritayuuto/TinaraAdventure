using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHp : MonoBehaviour
{
    //[SerializeField]
    //PlayerController player = null;
    [SerializeField, Header("�̗͂̍ő�l"), Tooltip("player�̗̑͂̍ő�l")]
    private float _playerHp = 5000f;
    [SerializeField, Header("�_���[�W���󂯂ĕω�����̗�"), Tooltip("player�̑�������̗�")]
    float _playerDamageHp = 0;
    [Tooltip("�U�������X�L�����g���Ă��鎞�Ɏ󂯂��U�����J�E���g����")]
    int _invalidCount = 0;
    [SerializeField, Tooltip("�v���C���[��HP�\���p�e�L�X�g")]
    Text playerHpText = null;
    [SerializeField]
    Slider hpSlider = null;
    PlayerUseSkill _playerUseSkill = null;
    PlayerAnim _playerAnim = null;
    /// <summary> player�̑�������̗�</summary>
    public float PlayerDamageHp { get => _playerDamageHp; set => _playerDamageHp = value; }
    /// <summary> player�̗̑͂̍ő�l</summary>
    public float PlayerMaxHp { get => _playerHp; set => _playerHp = value; }
    void Start()
    {
        _playerDamageHp = _playerHp;
        _playerUseSkill = GameManager.Instance.PlayerUseSkill;
        _playerAnim = GameManager.Instance.PlayerAnim;
    }
    void Update()
    {
        playerHpText.text = _playerDamageHp.ToString() + "/" + _playerHp.ToString();
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
            hpSlider.value = _playerDamageHp / _playerHp;
            _playerAnim.DamageAnimation();
            Debug.Log(_playerDamageHp);
        }
    }
}
