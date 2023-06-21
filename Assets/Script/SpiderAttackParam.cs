using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAttackParam : MonoBehaviour
{
    [SerializeField]
    GameObject _poisonObject = null;
    PoisonDamage _poisonDamage = null;
    [SerializeField, Header("攻撃力の最低値"), Tooltip("攻撃力の最低値")]
    int _minAttackDamage = 800;
    [SerializeField, Header("攻撃力の最大値"), Tooltip("攻撃力の最大値")]
    int _maxAttackDamage = 1200;
    [Tooltip("randomを使うために使用")]
    const int _two = 2;
    float _timer = 0f;
    float _limit = 5f;
    bool _exit = true;
    bool _attack = false;
    bool _poison = false;
    Animator _anim = null;
    [Tooltip("プレイヤーが一定の距離に近づいたらTrue")]
    public bool _playerDis = false;
    public int MinAttackDamage { get => _minAttackDamage; set => _minAttackDamage = value; }
    public int MaxAttackDamage { get => _maxAttackDamage; set => _maxAttackDamage = value; }

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _poisonDamage = _poisonObject.GetComponent<PoisonDamage>();
    }
    void Update()
    {
        if (_playerDis)
        {
            _timer += Time.deltaTime;
            int num = 0;
            if (_timer >= _limit)
            {
                num = Random.Range(0, _two);
                _anim.SetInteger("Number", num);
                if (num != 0)
                {
                    _attack = true;
                }
                else
                {
                    _poisonObject.SetActive(true);
                    _poisonDamage.PoisonAttack();
                    _attack = false;
                }
                _timer = 0;
            }
        }
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_attack)
            {
                GameManager.Instance.PlayerHp.Damage(Random.Range(MinAttackDamage,MaxAttackDamage));
                _attack = false;
            }
        }
    }
    public void Attack()
    {
        _attack = true;
    }
}
