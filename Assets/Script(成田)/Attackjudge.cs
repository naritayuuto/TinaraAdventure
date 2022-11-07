using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Attackjudge : MonoBehaviour
{
    [SerializeField]
    GameObject damageUi = null;
    PlayerController player = null;
    TextMeshProUGUI damageText = null;
    Skilltree skilltree = null;
    EnemyController enemy;
    SkillManager sManager = null; 
    private void Start()
    {
        if(!damageUi)
        {
            Debug.LogError("damageUIがありません");
        }
        sManager = GameObject.FindGameObjectWithTag("SkillManager").GetComponent<SkillManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        damageText = damageUi.GetComponentInChildren<TextMeshProUGUI>();
        //skilltree = GameObject.FindGameObjectWithTag("Skilltree").GetComponent<Skilltree>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Attack");
            Vector3 hitPos = other.ClosestPointOnBounds(transform.position);
            damageText.text = player.AttackDamage.ToString();
            Instantiate(damageUi, hitPos, Quaternion.identity);//ダメージ表示
            //enemy = other.gameObject.GetComponent<EnemyController1>();
            //enemy.Damage(player.AttackDamage);//ダメージを与える
            sManager.SkillPoint += 0.5f;//スキルpoint加算    
        }
    }
}
