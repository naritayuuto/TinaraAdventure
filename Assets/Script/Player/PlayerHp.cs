using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHp : MonoBehaviour
{
    //[SerializeField]
    //PlayerController player = null;
    [SerializeField, Header("体力の最大値"), Tooltip("playerの体力の最大値")]
    private float _playerHp = 5000f;
    [Header("ダメージを受けて変化する体力"), Tooltip("playerの増減する体力")]
    float _playerDamageHp = 0;
    [SerializeField, Tooltip("プレイヤーのHP表示用テキスト")]
    Text playerHpText = null;
    [SerializeField]
    Slider hpSlider = null;
    /// <summary> playerの増減する体力</summary>
    public float PlayerDamageHp { get => _playerDamageHp; set => _playerDamageHp = value; }
    /// <summary> playerの体力の最大値</summary>
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
