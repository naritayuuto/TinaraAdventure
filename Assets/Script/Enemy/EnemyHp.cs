using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            //�A�j���[�V�����𗬂��A�����蔻��ƂȂ��Ă���R���C�_�[�̃��X�g������Ă����A�����蔻��������B
            Debug.Log("�G������");
        }
    }
}
