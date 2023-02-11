using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Attackjudge : MonoBehaviour//����ɕt����
{
    [SerializeField, Tooltip("�_���[�W�\���p��UI")]
    GameObject damageUi = null;
    [Tooltip("damageUi��Text")]
    TextMeshProUGUI damageText = null;
    [Tooltip("�U������������Enemy")]
    EnemyHp _enemyHp = null;
    [Tooltip("player��AttackDamage�̒l������ϐ�")]
    int _playerAttack = 0;
    //public Vector3 _uiTransform;
    private void Start()
    {
        if (!damageUi)
        {
            Debug.LogError("damageUI������܂���");
        }
        damageText = damageUi.GetComponentInChildren<TextMeshProUGUI>();
        //gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //if (_uiTransform != default)
            //{
            Debug.Log("Attack");
            Vector3 hitPos = other.ClosestPointOnBounds(transform.position);
            _playerAttack = (int)Mathf.Round(GameManager.Instance._player._playerAttackParam.AttackDamage);
            damageText.text = _playerAttack.ToString();
            Instantiate(damageUi, hitPos, Quaternion.identity);//�_���[�W�\��
            _enemyHp = other.gameObject.GetComponent<EnemyHp>();
            _enemyHp.Damage(_playerAttack);
            GameManager.Instance._skillManager.AddSkillPoint(_enemyHp.Die);
            //}
            if (_enemyHp.Die)
            {
                _enemyHp = null;
            }
            //gameObject.SetActive(false);
        }
    }
}
