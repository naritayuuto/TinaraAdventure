using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class EnemyHp : MonoBehaviour
{
    [SerializeField,Header("�̗͂̍ő�l"),Tooltip("�̗͂̍ő�l")]
    private float _enemyHp = 5000;
    [Header("�_���[�W���󂯂ĕω�����̗�"), Tooltip("player�̑�������̗�")]
    float _enemyDamageHp;
    [Tooltip("���񂾂��ǂ���")]
    bool _die = false;
    [SerializeField]
    Slider hpSlider = null;
    Animator _anim = null;
    public bool Die { get => _die; }
    public float Hp { get => _enemyDamageHp; set => _enemyDamageHp = value; }
    // Start is called before the first frame update
    private void Start()
    {
        _enemyDamageHp = _enemyHp;
        _anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    public void Damage(int damage)
    {     
        _enemyDamageHp = _enemyDamageHp <= damage ? 0 : _enemyDamageHp - damage;
        hpSlider.value = _enemyDamageHp / _enemyHp;
        if (_enemyDamageHp <= 0)
        {
            _die = true;
            _anim.Play("Die");
            GetComponent<IEnemy>().Die();
            Debug.Log("�G������");
        }
        else
        {
            _anim.Play("Damage");
        }
    }
    public void EnemyDie()
    {
        Destroy(gameObject);
    }
}
