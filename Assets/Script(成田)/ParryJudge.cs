using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryJudge : MonoBehaviour
{
    bool parryJudge;
    PlayerController player = null;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            parryJudge = other.GetComponent<EnemyController>().Parry;
            player.ParryJudge(parryJudge);
        }
    }
}
