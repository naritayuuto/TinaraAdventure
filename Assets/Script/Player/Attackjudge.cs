using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Attackjudge : MonoBehaviour//武器に付ける
{
    [SerializeField, Tooltip("ダメージ表示用のUI")]
    GameObject damageUi = null;
    [Tooltip("damageUiのText")]
    TextMeshProUGUI damageText = null;
    [Tooltip("playerのAttackDamageの値を入れる変数")]
    int _playerAttack = 0;
    [Tooltip("当たり判定")]
    Collider _collider = default;
    private void Start()
    {
        if (!damageUi)
        {
            Debug.LogError("damageUIがありません");
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
            Instantiate(damageUi, hitPos, Quaternion.identity);//ダメージ表示
            enemyHp.Damage(_playerAttack);
            GameManager.Instance.SkillManager.AddSkillPoint(enemyHp.Die);
            _collider.enabled = false;
        }
    }
}
