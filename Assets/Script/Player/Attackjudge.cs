using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Attackjudge : MonoBehaviour//武器に付ける
{
    [SerializeField, Tooltip("ダメージ表示用のUI")]
    GameObject damageUi = null;
    [SerializeField, Header("player"), Tooltip("プレイヤー")]
    PlayerController _player = null;
    [Tooltip("damageUiのText")]
    TextMeshProUGUI damageText = null;
    [Tooltip("攻撃が当たったEnemy")]
    EnemyHp _enemyHp = null;
    [Tooltip("playerのAttackDamageの値を入れる変数")]
    int _playerAttack = 0;
    [Tooltip("定期的に当たり判定を消す処理を行うためのタイマー")]
    float _timer = 0;
    [Tooltip("当たり判定内にモンスターが入っていない場合に当たり判定を消す秒数")]
    float _colliderActiveTime = 0.5f;
    public Vector3 _Itransform;
    private void Start()
    {
        if (!damageUi)
        {
            Debug.LogError("damageUIがありません");
        }
        damageText = damageUi.GetComponentInChildren<TextMeshProUGUI>();
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (gameObject.activeSelf)
        {
            _timer += Time.deltaTime;
            if (_timer >= _colliderActiveTime)
            {
                gameObject.SetActive(false);
                _timer = 0;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Attack");
            //Vector3 hitPos = other.ClosestPointOnBounds(transform.position);
            _playerAttack = (int)Mathf.Round(GameManager.Instance.Player._playerAttackParam.AttackDamage);
            damageText.text = _playerAttack.ToString();
            Instantiate(damageUi, transform.position, Quaternion.identity);//ダメージ表示
            _enemyHp = other.gameObject.GetComponent<EnemyHp>();
            _enemyHp.Damage(_playerAttack);
            GameManager.Instance.SkillManager.AddSkillPoint(_enemyHp.Die);
            if (_enemyHp.Die)
            {
                _enemyHp = null;
            }
            transform.position = _Itransform;
            gameObject.SetActive(false);
        }
    }
    public void TransformMove(Vector3 pos)
    {
        _Itransform = transform.position;
        transform.position = pos;
    }
}
