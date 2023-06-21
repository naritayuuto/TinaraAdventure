using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDamage : MonoBehaviour
{
    [SerializeField]
    SpiderAttackParam _sAParam;
    [SerializeField]
    ParticleSystem _poison;
    float _timer = 0f;
    float _timelimit = 10f;
    [Tooltip("毒ダメージは、攻撃力の10分の1")]
    int _poisonDamage = 10;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
                _timer += Time.deltaTime;
               if (_timer >= _timelimit)
                {
                    GameManager.Instance.PlayerHp.Damage(Random.Range(_sAParam.MinAttackDamage,_sAParam.MaxAttackDamage) / _poisonDamage);
                }
        }
    }
    public void PoisonAttack()
    {
        _poison.Play();
    }
}
