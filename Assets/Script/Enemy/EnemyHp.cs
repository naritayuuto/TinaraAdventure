using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHp : MonoBehaviour
{
    [SerializeField,Header("体力の最大値"),Tooltip("体力の最大値")]
    private float _enemyHp = 5000;
    [Header("ダメージを受けて変化する体力"), Tooltip("playerの増減する体力")]
    float _enemyDamageHp;
    [Tooltip("死んだかどうか")]
    bool _die = false;
    [SerializeField]
    Slider hpSlider = null;
    public bool Die { get => _die; }
    public float Hp { get => _enemyDamageHp; set => _enemyDamageHp = value; }
    // Start is called before the first frame update
    private void Start()
    {
        _enemyDamageHp = _enemyHp;
    }
    // Update is called once per frame
    public void Damage(int damage)
    {
        _enemyDamageHp -= damage;
        hpSlider.value = _enemyDamageHp / _enemyHp;
        if (_enemyHp <= 0)
        {
            _die = true;
            //アニメーションを流す、当たり判定となっているコライダーのリストを作っておき、当たり判定を消す。
            Debug.Log("敵が死んだ");
        }
    }
}
