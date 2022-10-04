using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackjudge : MonoBehaviour
{
    [SerializeField]
    PlayerController player = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))//攻撃したモンスターのタグを覚えておいて、初めて攻撃した場合のみ0.5加算にする
        {
            player.GetSkillPoint(player.Getpoint);
            //Enemyにダメージを与える処理は関数にしてanimationイベントで行う。
        }
    }
}
