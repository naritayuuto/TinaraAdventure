using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryJudge : MonoBehaviour
{
    bool parryJudge;
    [SerializeField]
    PlayerController player = null;
    private void Start()
    {
        if(player)
        {
            Debug.LogError("�v���C���[���������܂���");
        }
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
