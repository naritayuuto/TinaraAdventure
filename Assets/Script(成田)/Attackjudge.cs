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
    EnemyController1 enemy;
    private void Start()
    {
        if(!damageUi)
        {
            Debug.LogError("damageUIがありません");
        }
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
            //enemy = other.gameObject.GetComponent<EnemyController1>();
            //enemy.Damage(player.AttackDamage);//ダメージを与える
            //skilltree.Skillpoint += 0.5f;//スキルpoint加算
            damageText.text = player.AttackDamage.ToString();
            Instantiate(damageUi,hitPos,Quaternion.identity);//ダメージ表示
        }
    }
}
