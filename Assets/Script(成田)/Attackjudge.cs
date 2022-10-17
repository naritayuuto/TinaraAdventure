using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackjudge : MonoBehaviour
{
    [SerializeField]
    PlayerController player = null;
    Skilltree skilltree = null;
    int enemyHp = 0;

    private void Start()
    {
        if (player)
        {
            Debug.LogError("プレイヤーを見つけられません");
        }
        skilltree = GameObject.FindGameObjectWithTag("Skilltree").GetComponent<Skilltree>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            enemyHp = gameObject.GetComponent<EnemyController>().EnemyHp;
            skilltree.Skillpoint += 0.5f;
            enemyHp -= player.AttackDamage; 
            //Enemyに与えるダメージの値変化は関数にしてanimationイベントで行う。
        }
    }
}
