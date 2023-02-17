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
    [Tooltip("player��AttackDamage�̒l������ϐ�")]
    int _playerAttack = 0;
    [Tooltip("�����蔻��")]
    Collider _collider = default;
    private void Start()
    {
        if (!damageUi)
        {
            Debug.LogError("damageUI������܂���");
        }
        damageText = damageUi.GetComponentInChildren<TextMeshProUGUI>();
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyHp>(out EnemyHp enemyHp))
        {
            Vector3 hitPos = other.ClosestPointOnBounds(transform.position);
            _playerAttack = (int)Mathf.Round(GameManager.Instance.Player._playerAttackParam.AttackDamage);
            damageText.text = _playerAttack.ToString();
            Instantiate(damageUi, hitPos, Quaternion.identity);//�_���[�W�\��
            enemyHp.Damage(_playerAttack);
            GameManager.Instance.SkillManager.AddSkillPoint(enemyHp.Die);
            _collider.enabled = false;
        }
    }
}
