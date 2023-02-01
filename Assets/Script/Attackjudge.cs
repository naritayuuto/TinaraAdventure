using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Attackjudge : MonoBehaviour
{
    [SerializeField,Tooltip("ダメージ表示用のUI")]
    GameObject damageUi = null;
    [Tooltip("damageUiのText")]
    TextMeshProUGUI damageText = null;
    [Tooltip("攻撃が当たったEnemy")]
    EnemyController enemy = null;
    private void Start()
    {
        if(!damageUi)
        {
            Debug.LogError("damageUIがありません");
        }
        damageText = damageUi.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Attack");
            Vector3 hitPos = other.ClosestPointOnBounds(transform.position);
            damageText.text = GameManager.Instance._player._playerAttackParam.AttackDamage.ToString();
            Instantiate(damageUi, hitPos, Quaternion.identity);//ダメージ表示
            enemy = other.gameObject.GetComponent<EnemyController>();
            enemy.Damage(GameManager.Instance._player._playerAttackParam.AttackDamage);
            GameManager.Instance._skillManager.AddSkillPoint(enemy.Die);
            if(enemy.Die)
            {
                enemy = null;
            }
        }
    }
}
