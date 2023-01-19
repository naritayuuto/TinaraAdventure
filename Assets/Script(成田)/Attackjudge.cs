using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Attackjudge : MonoBehaviour
{
    [SerializeField,Tooltip("�_���[�W�\���p��UI")]
    GameObject damageUi = null;
    [SerializeField,Tooltip("damageUi��Text")]
    TextMeshProUGUI damageText = null;
    PlayerController player = null;
    [Tooltip("�U������������Enemy")]
    EnemyController enemy = null;
    private void Start()
    {
        if(!damageUi)
        {
            Debug.LogError("damageUI������܂���");
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        damageText = damageUi.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Attack");
            Vector3 hitPos = other.ClosestPointOnBounds(transform.position);
            damageText.text = player.AttackDamage.ToString();
            Instantiate(damageUi, hitPos, Quaternion.identity);//�_���[�W�\��
            enemy = other.gameObject.GetComponent<EnemyController>();
            enemy.Damage(player.AttackDamage);
            GameManager.Instance._skillManager.AddSkillPoint(enemy.Die);
            if(enemy.Die)
            {
                enemy = null;
            }
        }
    }
}
