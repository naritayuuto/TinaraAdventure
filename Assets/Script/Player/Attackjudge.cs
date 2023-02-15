using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Attackjudge : MonoBehaviour//武器に付ける
{
    [SerializeField, Tooltip("ダメージ表示用のUI")]
    GameObject damageUi = null;
    [Tooltip("damageUiのText")]
    TextMeshProUGUI damageText = null;
    [Tooltip("playerのAttackDamageの値を入れる変数")]
    int _playerAttack = 0;
    [Tooltip("定期的に当たり判定を消す処理を行うためのタイマー")]
    float _timer = 0;
    [Tooltip("当たり判定内に標的が入っていない場合に当たり判定を消す秒数")]
    float _colliderActiveTime = 0.5f;
    [Tooltip("当たり判定")]
    Collider _collider = default;
    public Vector3 _iTransform;
    private void Start()
    {
        if (!damageUi)
        {
            Debug.LogError("damageUIがありません");
        }
        damageText = damageUi.GetComponentInChildren<TextMeshProUGUI>();
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
    }
    private void Update()
    {
        if (_collider.enabled == true)
        {
            _timer += Time.deltaTime;
            if (_timer >= _colliderActiveTime)
            {
                _collider.enabled = false;
                _timer = 0;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyHp>(out EnemyHp enemyHp))
        {
            _playerAttack = (int)Mathf.Round(GameManager.Instance.Player._playerAttackParam.AttackDamage);
            damageText.text = _playerAttack.ToString();
            Instantiate(damageUi, transform.position, Quaternion.identity);//ダメージ表示
            enemyHp.Damage(_playerAttack);
            GameManager.Instance.SkillManager.AddSkillPoint(enemyHp.Die);
            transform.position = _iTransform;
            _collider.enabled = false;
        }
    }
    public void TransformMove(Vector3 pos)
    {
        _iTransform = transform.position;
        transform.position = pos;
    }

    public void ColliderActive()
    {
        _collider.enabled = true;
    }
}
