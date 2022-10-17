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
            Debug.LogError("ƒvƒŒƒCƒ„[‚ğŒ©‚Â‚¯‚ç‚ê‚Ü‚¹‚ñ");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            parryJudge = other.GetComponent<EnemyController1>().Parry;
            player.ParryJudge(parryJudge);
        }
    }
}
