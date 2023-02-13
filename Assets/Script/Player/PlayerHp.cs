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
    [Header("�_���[�W���󂯂ĕω�����̗�"), Tooltip("player�̑�������̗�")]
    float _playerDamageHp = 0;
    [SerializeField, Tooltip("�v���C���[��HP�\���p�e�L�X�g")]
    Text playerHpText = null;
    [SerializeField]
    Slider hpSlider = null;
    /// <summary> player�̑�������̗�</summary>
    public float PlayerDamageHp { get => _playerDamageHp; set => _playerDamageHp = value; }
    /// <summary> player�̗̑͂̍ő�l</summary>
    public float PlayerMaxHp { get => _playerHp; set => _playerHp = value; }
    void Start()
    {
        _playerDamageHp = _playerHp;
    }
    void Update()
    {
        playerHpText.text = _playerDamageHp.ToString() + "/" + _playerHp.ToString();
        if (Input.GetButtonDown("Jump"))
        {
            Damage(500);
        }
    }
    public void Damage(int damage)
    {
        _playerDamageHp = _playerDamageHp > damage ? _playerDamageHp - damage : 0;

        hpSlider.value = _playerDamageHp / _playerHp;
    }
}
